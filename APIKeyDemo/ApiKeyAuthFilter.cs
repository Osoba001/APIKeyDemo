using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;

namespace APIKeyDemo
{
    public class ApiKeyAuthFilter : IAuthorizationFilter
    {
        private readonly IConfiguration _config;

        public ApiKeyAuthFilter(IConfiguration config)
        {
            _config = config;
        }
        public void OnAuthorization(AuthorizationFilterContext context)
        {
            if (!context.HttpContext.Request.Headers.TryGetValue(ApiKeyHeaderName, out var extractedApiKey))
            {
                //ontext.Response.ContentType = "ap";
                context.Result = new UnauthorizedObjectResult("Api Key is Missing.");
                return;
            }
            var apikey = _config.GetValue<string>(ApiKeySectionName);
            if (!apikey.Equals(extractedApiKey))
            {
                context.Result = new UnauthorizedObjectResult("Invalid Api Key.");
                return;
            }
        }
    }
}
