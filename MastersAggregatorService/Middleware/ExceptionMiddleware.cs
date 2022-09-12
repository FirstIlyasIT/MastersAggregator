using System.Net;
using System.Text.Json;
using Dapper;
using MastersAggregatorService.Errors;
using Npgsql;

namespace MastersAggregatorService.Middleware;

public class ExceptionMiddleware
{
    private readonly RequestDelegate _next;
    private readonly ILogger<ExceptionMiddleware> _logger;
    private readonly IHostEnvironment _env;
    private readonly IConfiguration _configuration;


    public ExceptionMiddleware(RequestDelegate next, ILogger<ExceptionMiddleware> logger, IHostEnvironment env, IConfiguration configuration)
    {
        _next = next;
        _logger = logger;
        _env = env;
        _configuration = configuration;
    }

    public async Task InvokeAsync(HttpContext context)
    {
        try
        {
            await _next(context);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, ex.Message);
            
            context.Response.ContentType = "application/json";

            context.Response.StatusCode = (int)HttpStatusCode.InternalServerError;

            var response = _env.IsDevelopment()
                ? new ApiException(context.Response.StatusCode, context.User.ToString(), ex.Message, ex.StackTrace?.ToString())
                : new ApiException(context.Response.StatusCode, context.User.ToString(), "Internal Server Error");

            var options = new JsonSerializerOptions { PropertyNamingPolicy = JsonNamingPolicy.CamelCase };

            var json = JsonSerializer.Serialize(response, options);

            await SaveExceptionToDbAsync(new ApiException(context.Response.StatusCode, context.User.ToString(),
                ex.Message, ex.StackTrace?.ToString()));

            await context.Response.WriteAsync(json);
            

            throw;
        }
    }

    public async Task SaveExceptionToDbAsync(ApiException ex)
    {
        const string sqlQuery = 
            @"INSERT INTO master_shema.errors (status_code, user_name, message, details)" +
            $@"VALUES (@{nameof(ex.StatusCode)}), (@{nameof(ex.UserName)}), (@{nameof(ex.Message)}), (@{nameof(ex.Details)})";

        await using var connection = new NpgsqlConnection(_configuration.GetValue<string>("ConnectionString"));
        connection.Open();

        await connection.ExecuteAsync(sqlQuery);
    }
}