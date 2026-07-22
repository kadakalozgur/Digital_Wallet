using Microsoft.EntityFrameworkCore;

namespace DigitalWallet.Models
{
    public class Process
    {
        public int Id { get; set; }

        public int UserId { get; set; }

        [Precision(18, 2)]
        public decimal Amount { get; set; }

        public ProcessType Type { get; set; }

        public DateTime Date { get; set; } = DateTime.UtcNow;

        public string Description { get; set; }
    }
}
