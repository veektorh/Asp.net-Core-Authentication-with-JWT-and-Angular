using Microsoft.AspNetCore.Http;
using System.Threading.Tasks;

namespace WebApplication2.Middleware
{
    public class AuthMiddleware
    {
        private readonly RequestDelegate _next;

        public AuthMiddleware(RequestDelegate next)
        {
            _next = next;
        }

        public async Task Invoke(HttpContext context)
        {
            var headers = context.Request.Headers;
            if (headers.ContainsKey("X-Authorized"))
            {
                if (headers["X-Authorized"] == "false")
                {
                    context.Response.StatusCode = StatusCodes.Status401Unauthorized;
                    return;
                }
            }

            await _next.Invoke(context);
        }
    }
}
