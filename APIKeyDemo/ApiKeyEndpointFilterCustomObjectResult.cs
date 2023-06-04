namespace APIKeyDemo
{
    public class ApiKeyEndpointFilterCustomObjectResult: IEndpointFilter
    {
        private readonly IConfiguration _config;

        public ApiKeyEndpointFilterCustomObjectResult(IConfiguration config)
        {
            _config = config;
        }
        public async ValueTask<object?> InvokeAsync(
            EndpointFilterInvocationContext context,
            EndpointFilterDelegate next)
        {
            if (!context.HttpContext.Request.Headers.TryGetValue(ApiKeyHeaderName, out var extractedApiKey))
            {
                return new UnauthorizedHttpObjectResult("Api Key is Missing.");
            }
            var apikey = _config.GetValue<string>(ApiKeySectionName);
            if (!apikey.Equals(extractedApiKey))
            {
                return new UnauthorizedHttpObjectResult("Api Key is Missing.");
            }
            return await next(context);
        }
    }

}
