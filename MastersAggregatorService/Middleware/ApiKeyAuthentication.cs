using Dapper;
using MastersAggregatorService.Controllers;
using MastersAggregatorService.Models;
using MastersAggregatorService.Repositories;
using Npgsql;

namespace MastersAggregatorService.Middleware
{
    public class ApiKeyAuthentication : BaseRepository<Master>
    {
        /// <summary>
        /// проверяем ApiKey с context.Request.Headers
        /// </summary>
        private readonly RequestDelegate _next;
        private readonly IConfiguration _configuration;
        public ApiKeyAuthentication(RequestDelegate next, IConfiguration configuration) : base(configuration)
        {
            _next = next;
            _configuration = configuration;
        }
        public async Task InvokeAsync(HttpContext context)
        {
            //ApiKey - в Swagger назначили как ключ в заголовке куда запишеться Api ключ в Request.Headers
            if (!context.Request.Headers.TryGetValue("ApiKey", out var extractedApiKey))//получеам с Header элемент ApiKey, если нет то ошибка
            {
                context.Response.StatusCode = 403;
                await context.Response.WriteAsync("Api Key was not provided. (Using ApiKeyAuthentication) ");
                return;
            }

             
            //проверяем совпадает ли наш ключ с БД с ключем с сайта 
            if (ValidateTokens(extractedApiKey.First().ToString()).GetAwaiter().GetResult())
            {
                context.Response.StatusCode = 403;
                await context.Response.WriteAsync("Unauthorized client. (Using ApiKeyAuthentication)");
                return;
            }
            await _next(context);
        }

        //!!!!!!!!!!ОШИБКА НЕ ПОЛУЧАЕТЬСЯ ПОДКЛЮЧИТЬСЯ К БД!!!!!!!!!!!!!!!
        public async Task<bool> ValidateTokens(string strTokenApi)
        {
            //получаем список ключей из Token
            string sqlQuery = @"SELECT api_token AS ApiToken " +
                                     @"FROM master_shema.access";

            await using var connection = new NpgsqlConnection(_configuration.GetConnectionString("ConnectionString"));
            connection.Open();
            var tokens = await connection.QueryAsync<string>(sqlQuery);

            foreach (var token in tokens)
            {
                if (token == strTokenApi)
                    return true;
            }

            return false;
        }
    } 
}
 
