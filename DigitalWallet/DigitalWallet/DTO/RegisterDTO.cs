using System.ComponentModel.DataAnnotations;

namespace DigitalWallet.DTO
{
    public class RegisterDTO
    {
        [Required(ErrorMessage = "Ad alanı zorunludur.")]
        [MaxLength(50, ErrorMessage = "Ad en fazla 50 karakter olabilir.")]
        public string Name { get; set; } = string.Empty;

        [Required(ErrorMessage = "Soyad alanı zorunludur.")]
        [MaxLength(50, ErrorMessage = "Soyad en fazla 50 karakter olabilir.")]
        public string Surname { get; set; } = string.Empty;

        [Required(ErrorMessage = "TC Kimlik numarası zorunludur.")]
        [StringLength(11, MinimumLength = 11, ErrorMessage = "TC Kimlik numarası tam 11 haneli olmalıdır.")]
        [RegularExpression("^[0-9]*$", ErrorMessage = "TC Kimlik numarası sadece rakamlardan oluşabilir.")]
        public string TC { get; set; } = string.Empty;

        [Required(ErrorMessage = "Şifre alanı zorunludur.")]
        [MinLength(6, ErrorMessage = "Şifre en az 6 karakter olmalıdır.")]
        public string Password { get; set; } = string.Empty;
    }
}
