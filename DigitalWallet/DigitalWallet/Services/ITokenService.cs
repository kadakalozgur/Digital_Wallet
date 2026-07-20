using DigitalWallet.Models;

namespace DigitalWallet.Services
{
    public interface ITokenService
    {
        string GenerateToken(User user);
    }
}
