using System.Text.Json.Serialization;

namespace TwitterLite.Server.Models
{
    public class User
    {
        public int Id { get; set; }
        public string Username { get; set; }
        [JsonIgnore] public string Password { get; set; }
        public bool IsAdmin { get; set; }
        public List<Tweet> Tweets { get; set; }
        public List<Tweet> Likes { get; set; }
    }
}
