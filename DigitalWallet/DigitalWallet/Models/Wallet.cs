using Microsoft.EntityFrameworkCore;

namespace DigitalWallet.Models
{
    public class Wallet
    {
        public int Id { get; set; }
        public int UserId { get; set; }

        [Precision(18, 2)]
        public decimal Money {  get; set; }
        public User User { get; set; }
    }
}
