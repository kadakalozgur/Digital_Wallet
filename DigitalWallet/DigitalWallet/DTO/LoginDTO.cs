using System.ComponentModel.DataAnnotations;

namespace DigitalWallet.DTO
{
    public class LoginDTO
    {
        [Required(ErrorMessage = "TC Kimlik numarası zorunludur.")]
        [StringLength(11, MinimumLength = 11, ErrorMessage = "TC Kimlik numarası tam 11 haneli olmalıdır.")]
        [RegularExpression("^[0-9]*$", ErrorMessage = "TC Kimlik numarası sadece rakamlardan oluşabilir.")]
        public string TC { get; set; } = string.Empty;

        [Required(ErrorMessage = "Şifre alanı zorunludur.")]
        public string Password { get; set; } = string.Empty;
    }
}
