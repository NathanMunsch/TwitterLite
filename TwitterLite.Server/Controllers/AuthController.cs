using Microsoft.AspNetCore.Mvc;
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
            var user = new User
            {
                Username = registerDto.Username,
                Password = BCrypt.Net.BCrypt.HashPassword(registerDto.Password),
            };

            if (user.Username.Length < 3) return BadRequest(new { message = "Username must contains at least 3 characters" });
            if (userRepository.GetByUsername(user.Username) != null) return BadRequest(new { message = "Username already exists" });

            var userCreated = userRepository.Create(user);

            return Ok(new { message = "User created", userCreated });
        }

        [HttpPost("login")]
        public IActionResult Login(LoginDto loginDto)
        {
            var user = userRepository.GetByUsername(loginDto.Username);
            if (user == null) return BadRequest(new { message = "Invalid credentials" });

            if (!BCrypt.Net.BCrypt.Verify(loginDto.Password, user.Password)) return BadRequest(new { message = "Invalid credentials" });

            var jwtToken = jwtService.Generate(user.Id);
            Response.Cookies.Append("jwtToken", jwtToken, new CookieOptions
            {
                HttpOnly = true
            });

            return Ok(new { message = "Authentication successful" });
        }

        [HttpGet("getAuthenticatedUser")]
        public IActionResult GetAuthenticatedUser()
        {
            try
            {
                var jwtToken = Request.Cookies["jwtToken"];
                if (jwtToken == null) return BadRequest();

                if (jwtService.IsTokenExpired(jwtToken)) return Unauthorized();

                var userId = jwtService.GetUserIdFromToken(jwtToken);
                var user = userRepository.GetById(userId);
                if (user == null) return BadRequest();

                return Ok(new { user });
            }
            catch (Exception)
            {
                return BadRequest();
            }
        }

        [HttpPost("logout")]
        public IActionResult Logout()
        {
            Response.Cookies.Delete("jwtToken");

            return Ok(new { message = "Logged out" });
        }
    }
}
