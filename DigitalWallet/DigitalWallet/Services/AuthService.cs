using DigitalWallet.Data;
using DigitalWallet.DTO;
using DigitalWallet.Models;
using DigitalWallet.Responses;
using Microsoft.EntityFrameworkCore;
using System.Runtime.CompilerServices;

namespace DigitalWallet.Services
{
    public class AuthService : IAuthService
    {

        private readonly Database _context;

        public AuthService(Database context)
        {
            _context = context;
        }

        public async Task<ServiceResult> Register(RegisterDTO registerDto)
        {
            bool isUserExist = await _context.Users.AnyAsync(x => x.TC == registerDto.TC);

            if (isUserExist)
            {
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
                PasswordHash = BCrypt.Net.BCrypt.HashPassword(registerDto.Password)
            };

            await _context.Users.AddAsync(newUser);
            await _context.SaveChangesAsync();

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
                return new ServiceResult
                {
                    IsSuccess = false,
                    Message = "Hatalı TC Kimlik Numarası veya Şifre!"
                };
            }

            bool isPasswordCorrect = BCrypt.Net.BCrypt.Verify(loginDto.Password, user.PasswordHash);

            if (!isPasswordCorrect)
            {
                return new ServiceResult
                {
                    IsSuccess = false,
                    Message = "Hatalı TC Kimlik Numarası veya Şifre!"
                };
            }

            return new ServiceResult
            {
                IsSuccess = true,
                Message = $"Giriş başarılı! Hoş geldin {user.Name} {user.Surname}"
            };

        }
    }
}
