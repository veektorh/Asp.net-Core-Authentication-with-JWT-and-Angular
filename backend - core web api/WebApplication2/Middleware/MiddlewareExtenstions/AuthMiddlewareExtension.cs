using Microsoft.AspNetCore.Builder;

namespace WebApplication2.Middleware.MiddlewareExtenstions
{
    public static class AuthMiddlewareExtension
    {
        public static IApplicationBuilder UseAuthMiddleware(this IApplicationBuilder builder)
        {
            return builder.UseMiddleware<AuthMiddleware>();
        }
    }
}
