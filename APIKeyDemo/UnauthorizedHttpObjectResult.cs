namespace APIKeyDemo
{
    public sealed class UnauthorizedHttpObjectResult:IResult,IStatusCodeHttpResult
    {
        private readonly object _body;

        public UnauthorizedHttpObjectResult(object body)
        {
            _body = body;
        }
        public int StatusCode => StatusCodes.Status401Unauthorized;
        int? IStatusCodeHttpResult.StatusCode => StatusCode;
        public async Task ExecuteAsync(HttpContext httpContext)
        {
            ArgumentNullException.ThrowIfNull(httpContext);

            httpContext.Response.StatusCode = StatusCode;
            if(_body is string s)
            {
                await httpContext.Response.WriteAsync(s);
            }

            await httpContext.Response.WriteAsJsonAsync(_body);
        }
    }

}
