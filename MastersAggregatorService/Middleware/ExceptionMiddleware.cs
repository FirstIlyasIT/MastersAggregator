using System.Net;
using System.Text.Json;
using MastersAggregatorService.Errors;
using MastersAggregatorService.Repositories;

namespace MastersAggregatorService.Middleware;


public class ExceptionMiddleware
{
    private readonly RequestDelegate _next;
    private readonly ILogger<ExceptionMiddleware> _logger;
    private readonly ExceptionRepository _repository;


    public ExceptionMiddleware(RequestDelegate next, ILogger<ExceptionMiddleware> logger, ExceptionRepository repository)
    {
        _next = next;
        _logger = logger;
        _repository = repository;
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

            var response = new ApiException(context.Response.StatusCode, context.User.ToString(), ex.Message,
                    ex.StackTrace?.ToString());

            var options = new JsonSerializerOptions { PropertyNamingPolicy = JsonNamingPolicy.CamelCase };

            var json = JsonSerializer.Serialize(response, options);

            await _repository.SaveAsync(response);

            await context.Response.WriteAsync(json);
            
            throw;
        }
    }
}