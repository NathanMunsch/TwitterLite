using System.Collections.ObjectModel;

namespace TwitterLite.Server.Models
{
    public class Tweet
    {
        public int Id { get; set; }
        public string Content { get; set; }
        public int AuthorId { get; set; }
        public User Author { get; set; }
        public List<User> LikedBy { get; set; }
    }
}
