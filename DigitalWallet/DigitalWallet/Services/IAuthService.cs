using DigitalWallet.DTO;

namespace DigitalWallet.Services
{
    public interface IAuthService
    {
        Task<string> Register(RegisterDTO registerDto);
        Task<string> Login(LoginDTO loginDto);
    }
}
