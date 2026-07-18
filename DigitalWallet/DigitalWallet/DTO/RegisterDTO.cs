using System.ComponentModel.DataAnnotations;

namespace DigitalWallet.DTO
{
    public class RegisterDTO
    {
        [Required]
        [MaxLength(50)]
        public string Name { get; set; } = string.Empty;

        [Required]
        [MaxLength(50)]
        public string Surname { get; set; } = string.Empty;

        [Required]
        [StringLength(11, MinimumLength = 11)]
        public string TC { get; set; } = string.Empty;

        [Required]
        public string Password { get; set; } = string.Empty;
    }
}
