using DigitalWallet.Data;
using DigitalWallet.DTO;
using DigitalWallet.Helpers;
using DigitalWallet.Models;
using DigitalWallet.Responses;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using System.Runtime.CompilerServices;

namespace DigitalWallet.Services
{
    public class AuthService : IAuthService
    {

        private readonly Database _context;
        private readonly ITokenService _tokenService;
        private readonly ILogger<AuthService> _logger;

        public AuthService(Database context, ITokenService tokenService, ILogger<AuthService> logger)
        {
            _context = context;
            _tokenService = tokenService;
            _logger = logger;
        }

        public async Task<ServiceResult> Register(RegisterDTO registerDto)
        {
            bool isUserExist = await _context.Users.AnyAsync(x => x.TC == registerDto.TC);

            if (isUserExist)
            {
                _logger.LogWarning("BAŞARISIZ KAYIT: Sistemde zaten var olan {TC} TC numarası ile kayıt olunmaya çalışıldı.", registerDto.TC.MaskTC());

                return new ServiceResult
                {
                    IsSuccess = false,
                    Message = "Bu TC kimlik numarası sisteme zaten kayıtlı!"
                };
            }

            var newUser = new User
            {
                Name = registerDto.Name,
                Surname = registerDto.Surname,
                TC = registerDto.TC,
                PasswordHash = BCrypt.Net.BCrypt.HashPassword(registerDto.Password),
                Wallet = new Wallet { Money = 0m }
            };

            await _context.Users.AddAsync(newUser);
            await _context.SaveChangesAsync();

            _logger.LogInformation("YENİ KAYIT: {UserId} ID'li ve {TC} TC numaralı kullanıcı sisteme başarıyla kayıt oldu.", newUser.Id, newUser.TC.MaskTC());

            return new ServiceResult
            {
                IsSuccess = true,
                Message = "Kayıt işlemi başarıyla tamamlandı!"
            };

        }

        public async Task<ServiceResult> Login(LoginDTO loginDto)
        {

            var user = await _context.Users.FirstOrDefaultAsync(x => x.TC == loginDto.TC);

            if (user == null)
            {
                _logger.LogWarning("BAŞARISIZ GİRİŞ: Sistemde kayıtlı olmayan {TC} TC numarası ile giriş yapılmaya çalışıldı.", loginDto.TC.MaskTC());

                return new ServiceResult
                {
                    IsSuccess = false,
                    Message = "Hatalı TC Kimlik Numarası veya Şifre!"
                };
            }

            bool isPasswordCorrect = BCrypt.Net.BCrypt.Verify(loginDto.Password, user.PasswordHash);

            if (!isPasswordCorrect)
            {
                _logger.LogWarning("BAŞARISIZ GİRİŞ: {TC} TC numaralı kullanıcı için HATALI ŞİFRE girildi.", loginDto.TC.MaskTC());

                return new ServiceResult
                {
                    IsSuccess = false,
                    Message = "Hatalı TC Kimlik Numarası veya Şifre!"
                };
            }

            var token = _tokenService.GenerateToken(user);

            _logger.LogInformation("BAŞARILI GİRİŞ: {UserId} ID'li ({TC}) kullanıcı sisteme başarıyla giriş yaptı.", user.Id, user.TC.MaskTC());

            return new ServiceResult
            {
                IsSuccess = true,
                Message = $"Giriş başarılı! Hoş geldin {user.Name} {user.Surname} -- Token: {token}"
            };

        }
    }
}
