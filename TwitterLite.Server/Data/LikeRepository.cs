using TwitterLite.Server.Models;

namespace TwitterLite.Server.Data
{
    public class LikeRepository
    {
        private readonly DataContext _dbContext;

        public LikeRepository(DataContext dbContext)
        {
            _dbContext = dbContext;
        }

        public void Create(User user, Tweet tweet)
        {
            if (_dbContext.Likes.Any(l => l.UserId == user.Id && l.TweetId == tweet.Id))
            {
                return;
            }

            _dbContext.Add(new Like { UserId = user.Id, TweetId = tweet.Id });
            _dbContext.SaveChanges();
        }

        public void Delete(User user, Tweet tweet)
        {
            if (!_dbContext.Likes.Any(l => l.UserId == user.Id && l.TweetId == tweet.Id))
            {
                return;
            }

            _dbContext.Remove(new Like { UserId = user.Id, TweetId = tweet.Id });
            _dbContext.SaveChanges();
        }
    }
}
