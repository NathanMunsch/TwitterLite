using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using TwitterLite.Server.Data;
using TwitterLite.Server.Dtos;
using TwitterLite.Server.Helpers;
using TwitterLite.Server.Models;

namespace TwitterLite.Server.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class TweetController : Controller
    {
        private readonly TweetRepository tweetRepository;
        private readonly UserRepository userRepository;
        private readonly LikeRepository likeRepository;
        private readonly JwtService jwtService;

        public TweetController(TweetRepository tweetRepository, UserRepository userRepository, LikeRepository likeRepository, JwtService jwtService)
        {
            this.tweetRepository = tweetRepository;
            this.userRepository = userRepository;
            this.likeRepository = likeRepository;
            this.jwtService = jwtService;
        }

        [HttpGet("newest")]
        public IActionResult MostRecent()
        {
            var tweets = tweetRepository.GetRecentTweets();
            return Ok(new { tweets });
        }

        [HttpGet("oldest")]
        public IActionResult Oldest()
        {
            var tweets = tweetRepository.GetOldTweets();
            return Ok(new { tweets });
        }

        [HttpGet("most-liked")]
        public IActionResult MostLiked()
        {
            var tweets = tweetRepository.GetMostLikedTweets();
            return Ok(new { tweets });
        }

        [HttpPost("create")]
        public IActionResult Create(TweetDto tweetDto)
        {
            var jwtToken = Request.Cookies["jwtToken"];
            var jwtSecurityToken = jwtService.IsValid(jwtToken);
            User user = userRepository.GetById(int.Parse(jwtSecurityToken.Issuer));

            tweetRepository.Create(new Tweet { Author = user, Content = tweetDto.Content});

            return Ok();
        }

        [HttpDelete("delete/{id}")]
        public IActionResult Delete(int id)
        {
            var tweet = tweetRepository.GetById(id);
            if (tweet == null) return NotFound();

            // Check if the user in the JWT token is the author of the tweet
            var jwtToken = Request.Cookies["jwtToken"];
            var jwtSecurityToken = jwtService.IsValid(jwtToken);
            User user = userRepository.GetById(int.Parse(jwtSecurityToken.Issuer));
            if (!userRepository.IsTheAuthor(user, tweet)) return Unauthorized();

            tweetRepository.Delete(tweet);

            return Ok();
        }

        [HttpGet("like/{id}")]
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

        [HttpGet("unlike/{id}")]
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
