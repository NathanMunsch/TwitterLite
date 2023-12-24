using TwitterLite.Server.Models;

namespace TwitterLite.Server.Data
{
    public class UserRepository
    {
        private readonly UserContext userContext;

        public UserRepository(UserContext userContext)
        {
            this.userContext = userContext;
        }
        public User Create(User user)
        {
            userContext.Users.Add(user);
            userContext.SaveChanges();

            var userCreated = userContext.Users.FirstOrDefault(xUser => xUser.Username == user.Username);

            if (userCreated == null) return null;
            if (user.Username != userCreated.Username) return null;

            return userCreated;
        }

        public User GetByUsername(string username)
        {
            var user = userContext.Users.FirstOrDefault(user => user.Username == username);

            if (user == null) return null;
            if (user.Username != username) return null;

            return user;
        }

        public User GetById(int id)
        {
            var user = userContext.Users.FirstOrDefault(user => user.Id == id);

            if (user == null) return null;
            if (user.Id != id) return null;

            return user;
        }
    }
}
