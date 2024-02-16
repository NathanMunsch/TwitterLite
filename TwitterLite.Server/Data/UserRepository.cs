using TwitterLite.Server.Models;

namespace TwitterLite.Server.Data
{
    public class UserRepository
    {
        private readonly DataContext _dbContext;

        public UserRepository(DataContext dbContext)
        {
            _dbContext = dbContext;
        }

        public void Create(User user)
        {
            _dbContext.Users.Add(user);
            _dbContext.SaveChanges();
        }

        public void Update(User user)
        {
            _dbContext.Users.Update(user);
        }

        public User GetById(int id) 
        { 
            return _dbContext.Users.FirstOrDefault(u => u.Id == id);
        }

        public User GetByUsername(string username)
        {
            return _dbContext.Users.FirstOrDefault(u => u.Username == username);
        }
    }
}
