using Microsoft.AspNetCore.Mvc;

namespace APIKeyDemo
{
    public class ApiKeyEndpointFilter : IEndpointFilter
    {
        private readonly IConfiguration _config;

        public ApiKeyEndpointFilter(IConfiguration config)
        {
            _config = config;
        }
        public async ValueTask<object?> InvokeAsync(
            EndpointFilterInvocationContext context, 
            EndpointFilterDelegate next)
        {
            if (!context.HttpContext.Request.Headers.TryGetValue(ApiKeyHeaderName, out var extractedApiKey))
            {
               return TypedResults.Unauthorized();
            }
            var apikey = _config.GetValue<string>(ApiKeySectionName);
            if (!apikey.Equals(extractedApiKey))
            {
                return TypedResults.Unauthorized();
            }
            return await next(context);
        }
    }
}
