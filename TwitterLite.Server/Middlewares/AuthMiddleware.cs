using Azure.Core;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using System.Threading.Tasks;
using TwitterLite.Server.Data;
using TwitterLite.Server.Helpers;

namespace TwitterLite.Server.Middlewares
{
    // You may need to install the Microsoft.AspNetCore.Http.Abstractions package into your project
    public class AuthMiddleware
    {
        private readonly RequestDelegate _next;

        public AuthMiddleware(RequestDelegate next)
        {
            _next = next;
        }

        public async Task Invoke(HttpContext httpContext, JwtService jwtService, UserRepository userRepository)
        {
            // Check if JWT token cookie exists
            var jwtToken = httpContext.Request.Cookies["jwtToken"];
            if (jwtToken == null)
            {
                httpContext.Response.StatusCode = StatusCodes.Status400BadRequest;
                return;
            }

            // Check if the JWT token is expired
            if (jwtService.IsTokenExpired(jwtToken))
            {
                httpContext.Response.StatusCode = StatusCodes.Status401Unauthorized;
                return;
            }

            // Check if the user in the JWT token exists
            var userId = jwtService.GetUserIdFromToken(jwtToken);
            var user = userRepository.GetById(userId);
            if (user == null)
            {
                httpContext.Response.StatusCode = StatusCodes.Status400BadRequest;
                return;
            }

            await _next(httpContext);
        }
    }

    // Extension method used to add the middleware to the HTTP request pipeline.
    public static class AuthenticationMiddlewareExtensions
    {
        public static IApplicationBuilder UseAuthenticationMiddleware(this IApplicationBuilder builder)
        {
            return builder.UseMiddleware<AuthMiddleware>();
        }
    }
}
