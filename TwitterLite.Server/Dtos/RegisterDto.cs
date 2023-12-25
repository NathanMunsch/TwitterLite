using System.ComponentModel.DataAnnotations;

namespace TwitterLite.Server.Dtos
{
    public class RegisterDto
    {
        [Required]
        [StringLength(50, MinimumLength = 3)]
        [RegularExpression(@"^[a-zA-Z0-9]+$")]
        public string Username { get; set; }

        [Required]
        [MaxLength(500)]
        [RegularExpression(@"^(?=.*[a-z])(?=.*[A-Z])(?=.*\d)(?=.*[@$!%*?&#_])[A-Za-z\d@$!%*?&#_]{12,}$")]
        public string Password { get; set; }
    }
}
