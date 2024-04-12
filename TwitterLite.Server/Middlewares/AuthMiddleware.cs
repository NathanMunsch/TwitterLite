using Azure.Core;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using System;
using System.IdentityModel.Tokens.Jwt;
using System.Threading.Tasks;
using TwitterLite.Server.Data;
using TwitterLite.Server.Helpers;
using TwitterLite.Server.Models;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory;
using static System.Net.Mime.MediaTypeNames;

namespace TwitterLite.Server.Middlewares
{
    // You may need to install the Microsoft.AspNetCore.Http.Abstractions package into your project
    public class AuthMiddleware
    {
        private readonly RequestDelegate _next;
        private readonly string[] _routes =
        {
            "/admin",
            "/auth/user",
            "/auth/logout",
            "/tweet",
            "/user/all",
            "/user/update",
            "/user/delete",
            "/user/get",
            "/user/has-liked"
        };

        public AuthMiddleware(RequestDelegate next)
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
            return;
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
