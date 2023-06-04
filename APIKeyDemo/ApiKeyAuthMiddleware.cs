global using static APIKeyDemo.AuthConstants;
namespace APIKeyDemo
{
    public class ApiKeyAuthMiddleware : IMiddleware
    {
        private readonly IConfiguration _config;

        public ApiKeyAuthMiddleware(IConfiguration config)
        {
            _config = config;
        }
        public async Task InvokeAsync(HttpContext context, RequestDelegate next)
        {
            if (!context.Request.Headers.TryGetValue(ApiKeyHeaderName, out var extractedApiKey))
            {
                //ontext.Response.ContentType = "ap";
                context.Response.StatusCode = 401;
                await context.Response.WriteAsync("API Key missing");
                return;
            }
            var apikey = _config.GetValue<string>(ApiKeySectionName);
            if (!apikey.Equals(extractedApiKey))
            {
                context.Response.StatusCode = 401;
                await context.Response.WriteAsync("API Key Invalid");
                return;
            }
            await next(context);
        }
    }
}
