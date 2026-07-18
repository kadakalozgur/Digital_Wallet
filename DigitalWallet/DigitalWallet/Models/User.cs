using System.ComponentModel.DataAnnotations;

namespace DigitalWallet.Models
{
    public class User
    {
        public int Id { get; set; }

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
        public string PasswordHash { get; set; } = string.Empty;

        public Wallet Wallet { get; set; }
    }
}
