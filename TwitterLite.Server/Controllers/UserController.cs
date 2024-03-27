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
    public class UserController : Controller
    {
        private readonly UserRepository userRepository;
        private readonly JwtService jwtService;

        public UserController(UserRepository userRepository, JwtService jwtService)
        {
            this.userRepository = userRepository;
            this.jwtService = jwtService;
        }

        [HttpGet("all")]
        public IActionResult All()
        {
            var users = userRepository.GetAll();
            return Ok(new { users });
        }

        [HttpGet("get/{id}")]
        public IActionResult Get(int id)
        {
            User user = userRepository.GetById(id);
            if (user == null) return NotFound();

            return Ok(new { user });
        }

        [HttpPost("create")]
        public IActionResult Create(RegisterDto registerDto)
        {
            User user = new User
            {
                Username = registerDto.Username,
                Password = BCrypt.Net.BCrypt.HashPassword(registerDto.Password),
            };

            if (userRepository.IsUsernameTaken(user.Username)) return BadRequest(new { errorMessage = "Username already exists" });

            userRepository.Create(user);

            return Ok();
        }

        [HttpPut("update")]
        public IActionResult Update(UpdateUserAccountDto updateUserAccountDto)
        {
            var jwtToken = Request.Cookies["jwtToken"];
            JwtSecurityToken jwtSecurityToken = jwtService.IsValid(jwtToken);

            // Update username
            User user = userRepository.GetById(int.Parse(jwtSecurityToken.Issuer));
            if (updateUserAccountDto.newUsername != null)
            {
                if (userRepository.IsUsernameTaken(updateUserAccountDto.newUsername)) return BadRequest(new { errorMessage = "Username already exists" });
                user.Username = updateUserAccountDto.newUsername;
            }

            // Update password
            if (updateUserAccountDto.newPassword != null)
            {
                if (!BCrypt.Net.BCrypt.Verify(updateUserAccountDto.oldPassword, user.Password)) return Unauthorized();
                user.Password = BCrypt.Net.BCrypt.HashPassword(updateUserAccountDto.newPassword);
            }

            userRepository.Update(user);
            return Ok();
        }

        [HttpDelete("delete")]
        public IActionResult Delete()
        {
            var jwtToken = Request.Cookies["jwtToken"];
            JwtSecurityToken jwtSecurityToken = jwtService.IsValid(jwtToken);

            User user = userRepository.GetById(int.Parse(jwtSecurityToken.Issuer));
            userRepository.Delete(user);

            return Ok();
        }
    }
}
