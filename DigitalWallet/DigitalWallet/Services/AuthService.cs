using DigitalWallet.Data;
using DigitalWallet.DTO;
using DigitalWallet.Models;
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

        public async Task<string> Register(RegisterDTO registerDto)
        {
            bool isUserExist = await _context.Users.AnyAsync(x => x.TC == registerDto.TC);

            if (isUserExist)
            {
                return "Bu TC kimlik numarası sisteme zaten kayıtlı!";
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

            return "Kayıt işlemi başarıyla tamamlandı!";

        }

        public async Task<string> Login(LoginDTO loginDto)
        {

            var user = await _context.Users.FirstOrDefaultAsync(x => x.TC == loginDto.TC);

            if (user == null)
            {
                return "Hatalı TC Kimlik Numarası veya Şifre!";
            }

            bool isPasswordCorrect = BCrypt.Net.BCrypt.Verify(loginDto.Password, user.PasswordHash);

            if (!isPasswordCorrect)
            {
                return "Hatalı TC Kimlik Numarası veya Şifre!";
            }

            return $"Giriş başarılı! Hoş geldin {user.Name} {user.Surname}";

        }
    }
}
