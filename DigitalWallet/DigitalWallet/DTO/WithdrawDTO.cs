using System.ComponentModel.DataAnnotations;

namespace DigitalWallet.DTO
{
    public class WithdrawDTO
    {
        [Required]
        [Range(0.01, double.MaxValue)]
        public decimal Amount { get; set; }
    }
}
