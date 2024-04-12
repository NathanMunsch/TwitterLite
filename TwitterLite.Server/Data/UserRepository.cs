using TwitterLite.Server.Models;

namespace TwitterLite.Server.Data
{
    public class UserRepository
    {
        private readonly DataContext _dbContext;
        private readonly TweetRepository _tweetRepository;

        public UserRepository(DataContext dbContext, TweetRepository tweetRepository)
        {
            _dbContext = dbContext;
            _tweetRepository = tweetRepository;
        }

        public bool IsUsernameTaken(string username)
        {
            return _dbContext.Users.Any(u => u.Username == username);
        }

        public bool IsTheAuthor(User user, Tweet tweet)
        {
            return tweet.AuthorId == user.Id;
        }

        public void Create(User user)
        {
            _dbContext.Users.Add(user);
            _dbContext.SaveChanges();
        }

        public void Update(User user)
        {
            _dbContext.Users.Update(user);
            _dbContext.SaveChanges();
        }

        public List<User> GetAll()
        {

            List<User> users = _dbContext.Users.ToList();
            foreach (var user in users)
            {
                user.NumberOfTweets = _tweetRepository.GetTweetsCount(user);
            }

            return users;
        }   

        public User GetById(int id)
        {
            User user = _dbContext.Users.FirstOrDefault(u => u.Id == id);
            if (user != null)
            {
                user.NumberOfTweets = _tweetRepository.GetTweetsCount(user);
            }
            
            return user;
        }

        public User GetByUsername(string username)
        {
            User user = _dbContext.Users.FirstOrDefault(u => u.Username == username);
            if (user != null)
            {
                user.NumberOfTweets = _tweetRepository.GetTweetsCount(user);
            }
            
            return user;
        }

        public void Delete(User user)
        {
            _tweetRepository.DeleteAllTweetsFromUser(user);
            _dbContext.Users.Remove(user);
            _dbContext.SaveChanges();
        }
    }
}
