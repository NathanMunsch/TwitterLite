﻿using Microsoft.AspNetCore.Mvc;
using System.IdentityModel.Tokens.Jwt;
using TwitterLite.Server.Data;
using TwitterLite.Server.Dtos;
using TwitterLite.Server.Helpers;
using TwitterLite.Server.Models;

namespace TwitterLite.Server.Controllers
{
    [Route("[Controller]")]
    [ApiController]
    public class AuthController : Controller
    {
        private readonly UserRepository userRepository;
        private readonly JwtService jwtService;

        public AuthController(UserRepository userRepository, JwtService jwtService)
        {
            this.userRepository = userRepository;
            this.jwtService = jwtService;
        }

        [HttpPost("login")]
        public IActionResult Login(LoginDto loginDto)
        {
            User user = userRepository.GetByUsername(loginDto.Username);
            if (user == null) return BadRequest();

            if (!BCrypt.Net.BCrypt.Verify(loginDto.Password, user.Password)) return Unauthorized();

            var jwtToken = jwtService.Generate(user.Id);
            Response.Cookies.Append("jwtToken", jwtToken, new CookieOptions
            {
                HttpOnly = true,
                Expires = DateTime.Today.AddDays(1)
            });

            return Ok();
        }

        [HttpGet("user")]
        public new IActionResult User()
        {
            var jwtToken = Request.Cookies["jwtToken"];
            JwtSecurityToken jwtSecurityToken = jwtService.IsValid(jwtToken);
            
            User user = userRepository.GetById(int.Parse(jwtSecurityToken.Issuer));

            return Ok(new { user });
        }

        [HttpGet("logout")]
        public IActionResult Logout()
        {
            Response.Cookies.Delete("jwtToken");

            return Ok();
        }
    }
}
