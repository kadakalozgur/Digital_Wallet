using System.ComponentModel.DataAnnotations;

namespace DigitalWallet.DTO
{
    public class FilterDTO
    {
        [Range(1, int.MaxValue)]
        public int PageNumber { get; set; } = 1;
        public int PageSize { get; } = 10;
    }
}
