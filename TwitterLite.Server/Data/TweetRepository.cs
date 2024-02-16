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
        }

        public void Delete(Tweet tweet)
        {
            _dbContext.Tweets.Remove(tweet);
        }

        public Tweet GetById(int id) 
        {
            return _dbContext.Tweets.FirstOrDefault(t => t.Id == id);
        }

        public List<Tweet> GetTweetsFromUser(User user)
        {
            return _dbContext.Tweets.Where(t => t.AuthorId == user.Id ).ToList();
        }
    }
}
