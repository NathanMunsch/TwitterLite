using Microsoft.EntityFrameworkCore;
using TwitterLite.Server.Models;

namespace TwitterLite.Server.Data
{
    public class TweetRepository
    {
        private readonly DataContext _dbContext;

        public TweetRepository(DataContext dbContext)
        {
            _dbContext = dbContext;
        }

        public void Create(Tweet tweet)
        {
            _dbContext.Tweets.Add(tweet);
            _dbContext.SaveChanges();
        }

        public void Delete(Tweet tweet)
        {
            _dbContext.Tweets.Remove(tweet);
            _dbContext.SaveChanges();
        }

        public void DeleteAllTweetsFromUser(User user)
        {
            foreach (var tweet in _dbContext.Tweets)
            {
                if (tweet.AuthorId == user.Id)
                {
                    _dbContext.Tweets.Remove(tweet);
                }
            }

            _dbContext.SaveChanges();
        }   

        public Tweet GetById(int id) 
        {
            return _dbContext.Tweets.Include(t => t.LikedBy).FirstOrDefault(t => t.Id == id);
        }

        public List<Tweet> GetRecentTweets()
        {
            return _dbContext.Tweets.Include(t => t.LikedBy).OrderByDescending(t => t.CreatedAt).ToList();
        }

        public List<Tweet> GetOldTweets()
        {
            return _dbContext.Tweets.Include(t => t.LikedBy).OrderBy(t => t.CreatedAt).ToList();
        }

        public List<Tweet> GetMostLikedTweets()
        {
            return _dbContext.Tweets.Include(t => t.LikedBy).OrderByDescending(t => t.LikedBy.Count).ToList();
        }
    }
}
