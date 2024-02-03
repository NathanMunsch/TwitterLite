using System.ComponentModel.DataAnnotations;

namespace TwitterLite.Server.Dtos
{
    public class RegisterDto
    {
        [Required]
        [StringLength(32, MinimumLength = 3)]
        [RegularExpression(@"^[a-zA-Z0-9]+$")]
        public string Username { get; set; }


        /*
         * Require that at least one digit appear anywhere in the string
         * Require that at least one lowercase letter appear anywhere in the string
         * Require that at least one uppercase letter appear anywhere in the string
         * Require that at least one special character appear anywhere in the string
         * The password must be at least 12 characters long, but no more than 64
        */
        [Required]
        [RegularExpression(@"^(?=.*[0-9])(?=.*[a-z])(?=.*[A-Z])(?=.*[*.!@$%^&(){}[\]:;<>,.?/~_+\-=|\\]).{12,64}$")]
        public string Password { get; set; }
    }
}
