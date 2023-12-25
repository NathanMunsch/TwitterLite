using Azure.Core;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using System.IdentityModel.Tokens.Jwt;
using System.Threading.Tasks;
using TwitterLite.Server.Data;
using TwitterLite.Server.Helpers;
using TwitterLite.Server.Models;

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
            JwtSecurityToken jwtSecurityToken;
            var jwtToken = httpContext.Request.Cookies["jwtToken"];
            int userId;

            try
            {
                jwtSecurityToken = jwtService.IsValid(jwtToken);
                userId = int.Parse(jwtSecurityToken.Issuer);
            }
            catch
            {
                httpContext.Response.StatusCode = StatusCodes.Status400BadRequest;
                return;
            }

            // Check if the user in the JWT token exists
            User user = userRepository.GetById(userId);
            if (user == null)
            {
                httpContext.Response.StatusCode = StatusCodes.Status401Unauthorized;
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
