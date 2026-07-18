using DigitalWallet.DTO;
using DigitalWallet.Responses;

namespace DigitalWallet.Services
{
    public interface IAuthService
    {
        Task<ServiceResult> Register(RegisterDTO registerDto);
        Task<ServiceResult> Login(LoginDTO loginDto);
    }
}
