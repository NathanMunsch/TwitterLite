using Microsoft.AspNetCore.Mvc;
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

        [HttpPost("register")]
        public IActionResult Register(RegisterDto registerDto)
        {
            User user = new User
            {
                Username = registerDto.Username,
                Password = BCrypt.Net.BCrypt.HashPassword(registerDto.Password),
            };

            if (userRepository.GetByUsername(user.Username) != null) return BadRequest(new { message = "Username already exists" });

            User userCreated = userRepository.Create(user);

            return Ok(new { message = "User created", userCreated });
        }

        [HttpPost("login")]
        public IActionResult Login(LoginDto loginDto)
        {
            User user = userRepository.GetByUsername(loginDto.Username);
            if (user == null) return BadRequest(new { message = "Invalid credentials" });

            if (!BCrypt.Net.BCrypt.Verify(loginDto.Password, user.Password)) return Unauthorized();

            var jwtToken = jwtService.Generate(user.Id);
            Response.Cookies.Append("jwtToken", jwtToken, new CookieOptions
            {
                HttpOnly = true
            });

            return Ok();
        }

        [HttpGet("getAuthenticatedUser")]
        public IActionResult GetAuthenticatedUser()
        {
            var jwtToken = Request.Cookies["jwtToken"];

            JwtSecurityToken jwtSecurityToken;
            try
            {
                jwtSecurityToken = jwtService.IsValid(jwtToken);
            }
            catch 
            {
                return BadRequest();
            }
            
            User user = userRepository.GetById(int.Parse(jwtSecurityToken.Issuer));

            return Ok(new { user });
        }

        [HttpPost("logout")]
        public IActionResult Logout()
        {
            Response.Cookies.Delete("jwtToken");

            return Ok();
        }
    }
}
