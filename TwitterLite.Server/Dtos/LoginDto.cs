using System.ComponentModel.DataAnnotations;

namespace TwitterLite.Server.Dtos
{
    public class LoginDto
    {
        [Required]
        [StringLength(50, MinimumLength = 3)]
        public string Username { get; set; }

        [Required]
        [StringLength(500, MinimumLength = 12)]
        public string Password { get; set; }
    }
}
