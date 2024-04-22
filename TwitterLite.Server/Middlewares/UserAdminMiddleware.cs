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
    public class UserAdminMiddleware
    {
        private readonly RequestDelegate _next;
        private readonly string[] _routes = { 
            "/admin"
        };

        public UserAdminMiddleware(RequestDelegate next)
        {
            _next = next;
        }

        public async Task Invoke(HttpContext httpContext, JwtService jwtService, UserRepository userRepository)
        {
            if (!RouteRequiresAuthentication(httpContext))
            {
                await _next(httpContext);
                return;
            }

            var jwtToken = httpContext.Request.Cookies["jwtToken"];
            JwtSecurityToken jwtSecurityToken = jwtService.IsValid(jwtToken);

            User user = userRepository.GetById(int.Parse(jwtSecurityToken.Issuer));

            if (user.IsAdmin) 
            { 
                await _next(httpContext);
                return;
            }
            else
            {
                httpContext.Response.StatusCode = StatusCodes.Status401Unauthorized;
                return;
            }
        }

        // function to return true or false if the route is in the list of routes that require authentication
        private bool RouteRequiresAuthentication(HttpContext httpContext)
        {
            foreach (string route in _routes)
            {
                if (httpContext.Request.Path.StartsWithSegments(route))
                {
                    return true;
                }
            }
            return false;
        }
    }
}