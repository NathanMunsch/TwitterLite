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
            return tweet.Author.Id == user.Id;
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
            return _dbContext.Users.ToList();
        }   

        public User GetById(int id) 
        { 
            return _dbContext.Users.FirstOrDefault(u => u.Id == id);
        }

        public User GetByUsername(string username)
        {
            return _dbContext.Users.FirstOrDefault(u => u.Username == username);
        }

        public void Delete(User user)
        {
            _tweetRepository.DeleteAllTweetsFromUser(user);
            _dbContext.Users.Remove(user);
            _dbContext.SaveChanges();
        }
    }
}
