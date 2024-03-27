using System.Text.Json.Serialization;

namespace TwitterLite.Server.Models
{
    public class User
    {
        public int Id { get; set; }
        public string Username { get; set; }
        [JsonIgnore] public string Password { get; set; }
        public bool IsAdmin { get; set; }
        [JsonIgnore] public List<Tweet> Tweets { get; set; } = new List<Tweet>();
        [JsonIgnore] public List<Tweet> Likes { get; set; } = new List<Tweet>();
        public DateTime RegisteredAt { get; set; } = DateTime.Now;
    }
}
