using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using TwitterLite.Server.Data;

namespace TwitterLite.Server.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class AdminController : Controller
    {
        private readonly UserRepository userRepository;
        private readonly TweetRepository tweetRepository;

        public AdminController(UserRepository userRepository, TweetRepository tweetRepository)
        {
            this.userRepository = userRepository;
            this.tweetRepository = tweetRepository;
        }

        [HttpDelete("delete-user/{id}")]
        public IActionResult DeleteUser(int id)
        {
            var user = userRepository.GetById(id);

            userRepository.Delete(user);

            return Ok();
        }

        [HttpDelete("delete-tweet/{id}")]
        public IActionResult DeleteTweet(int id)
        {
            var tweet = tweetRepository.GetById(id);
            if (tweet == null) return NotFound();

            tweetRepository.Delete(tweet);

            return Ok();
        }
    }
}
