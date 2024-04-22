using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;

namespace TwitterLite.Server.Models
{
    public class Tweet
    {
        public int Id { get; set; }
        public string Content { get; set; }
        public int AuthorId { get; set; }
        [JsonIgnore] public User Author { get; set; }
        public DateTime CreatedAt { get; set; } = DateTime.Now;
        [JsonIgnore] public List<User> LikedBy { get; set; } = new List<User>();
        [NotMapped] public int NumberOfLikes { get; set; }
    }
}
