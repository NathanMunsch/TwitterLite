using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using TwitterLite.Server.Data;
using TwitterLite.Server.Helpers;
using TwitterLite.Server.Models;

namespace TwitterLite.Server.Controllers
{
    [Route("/tweet/[controller]")]
    [ApiController]
    public class LikeController : Controller
    {
        public readonly LikeRepository likeRepository;
        public readonly TweetRepository tweetRepository;
        public readonly UserRepository userRepository;
        public readonly JwtService jwtService;

        public LikeController(LikeRepository likeRepository, TweetRepository tweetRepository, UserRepository userRepository, JwtService jwtService)
        {
            this.likeRepository = likeRepository;
            this.tweetRepository = tweetRepository;
            this.userRepository = userRepository;
            this.jwtService = jwtService;
        }

        [HttpGet("create/{id}")]
        public IActionResult Like(int id)
        {
            var tweet = tweetRepository.GetById(id);
            if (tweet == null) return NotFound();

            var jwtToken = Request.Cookies["jwtToken"];
            var jwtSecurityToken = jwtService.IsValid(jwtToken);
            User user = userRepository.GetById(int.Parse(jwtSecurityToken.Issuer));

            likeRepository.Create(user, tweet);

            return Ok();
        }

        [HttpDelete("delete/{id}")]
        public IActionResult Unlike(int id)
        {
            var tweet = tweetRepository.GetById(id);
            if (tweet == null) return NotFound();

            var jwtToken = Request.Cookies["jwtToken"];
            var jwtSecurityToken = jwtService.IsValid(jwtToken);
            User user = userRepository.GetById(int.Parse(jwtSecurityToken.Issuer));

            likeRepository.Delete(user, tweet);

            return Ok();
        }
    }
}
