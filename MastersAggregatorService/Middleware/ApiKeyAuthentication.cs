
using MastersAggregatorService.Repositories;


namespace MastersAggregatorService.Middleware
{ 
    public class ApiKeyAuthentication  
    {
        private readonly TokenRepository _repository;
         
        private readonly RequestDelegate _next;
    
        public ApiKeyAuthentication(RequestDelegate next)
        { 
            _next = next;
        }
        public async Task InvokeAsync(HttpContext context, TokenRepository repo)
        {
            //ApiKey - в Swagger назначили как ключ в заголовке куда запишеться Api ключ в Request.Headers
            if (!context.Request.Headers.TryGetValue("ApiKey", out var extractedApiKey))//получеам с Header элемент ApiKey, если нет то ошибка
            {
                context.Response.StatusCode = 403;
                await context.Response.WriteAsync("Api Key was not provided. (Using ApiKeyAuthentication) ");
                return;
            }
              
            /// <summary>
            /// проверяем ApiKey с context.Request.Headers
            /// </summary> 
            string strApiToken = extractedApiKey.First().ToString();
            //проверяем совпадает ли наш ключ с БД с ключем с сайта 
            if (!repo.ValidateTokenAsync(strApiToken, context).GetAwaiter().GetResult())
            {
                context.Response.StatusCode = 403;
                await context.Response.WriteAsync("Unauthorized client. (Using ApiKeyAuthentication)");
                return;
            }

            await _next(context);
        } 
        
    } 
}
 
