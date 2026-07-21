using System.ComponentModel.DataAnnotations;

namespace DigitalWallet.DTO
{
    public class TransferDTO
    {
        [StringLength(11)]
        public string ReceiverTC { get; set; }

        [Required]
        [Range(0.01, double.MaxValue)]
        public decimal Amount { get; set; }
    }
}
