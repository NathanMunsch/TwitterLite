using Microsoft.EntityFrameworkCore;
using TwitterLite.Server.Models;

namespace TwitterLite.Server.Data
{
    public class TweetRepository
    {
        private readonly DataContext _dbContext;
        private readonly LikeRepository _likeRepository;

        public TweetRepository(DataContext dbContext, LikeRepository likeRepository)
        {
            _dbContext = dbContext;
            _likeRepository = likeRepository;
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
            Tweet tweet = _dbContext.Tweets.FirstOrDefault(t => t.Id == id);
            tweet.NumberOfLikes = _likeRepository.GetLikesCount(tweet);
            return tweet;
        }

        public List<Tweet> GetRecentTweets()
        {
            List<Tweet> tweets = _dbContext.Tweets.OrderByDescending(t => t.CreatedAt).ToList();
            foreach (var tweet in tweets)
            {
                tweet.NumberOfLikes = _likeRepository.GetLikesCount(tweet);
            }
            return tweets;
        }

        public List<Tweet> GetOldTweets()
        {
            List<Tweet> tweets = _dbContext.Tweets.OrderBy(t => t.CreatedAt).ToList();
            foreach (var tweet in tweets)
            {
                tweet.NumberOfLikes = _likeRepository.GetLikesCount(tweet);
            }
            return tweets;
        }

        public List<Tweet> GetMostLikedTweets()
        {
            List<Tweet> tweets = _dbContext.Tweets.ToList();
            foreach (var tweet in tweets)
            {
                tweet.NumberOfLikes = _likeRepository.GetLikesCount(tweet);
            }
            return tweets.OrderByDescending(t => t.NumberOfLikes).ToList();
        }
    }
}
