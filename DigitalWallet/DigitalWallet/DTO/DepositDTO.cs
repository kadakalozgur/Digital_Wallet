using System.ComponentModel.DataAnnotations;

namespace DigitalWallet.DTO
{
    public class DepositDTO
    {
        [Required]
        [Range(0.01, double.MaxValue)]
        public decimal Amount { get; set; }
    }
}
