using System.Text.Json.Serialization;

namespace TwitterLite.Server.Models
{
    public class User
    {
        public int Id { get; set; }
        public string Username { get; set; }
        [JsonIgnore] public string Password { get; set; }
    }
}
