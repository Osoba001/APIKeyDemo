using Microsoft.AspNetCore.Mvc;

namespace APIKeyDemo
{
    public class ApiKeyAttribute:ServiceFilterAttribute
    {
        public ApiKeyAttribute():base(typeof(ApiKeyAuthFilter))
        {

        }
    }
}
