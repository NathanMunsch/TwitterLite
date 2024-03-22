using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;

namespace TwitterLite.Server.Models
{
    public class Tweet
    {
        public int Id { get; set; }
        public string Content { get; set; }
        public int AuthorId { get; set; }
        public virtual User Author { get; set; }
        public DateTime CreatedAt { get; set; } = DateTime.Now;
        [JsonIgnore] public virtual List<User> LikedBy { get; set; } = new List<User>();
        public int NumberOfLikes { get => LikedBy.Count; }
    }
}
