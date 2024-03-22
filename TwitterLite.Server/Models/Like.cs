using System.Text.Json.Serialization;

namespace TwitterLite.Server.Models
{
    public class Like
    {
        public int UserId { get; set; }
        public int TweetId { get; set; }
    }
}
