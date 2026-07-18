using System.ComponentModel.DataAnnotations;

namespace DigitalWallet.DTO
{
    public class LoginDTO
    {
        [Required]
        [StringLength(11, MinimumLength = 11)]
        public string TC { get; set; } = string.Empty;

        [Required]
        public string Password { get; set; } = string.Empty;
    }
}
