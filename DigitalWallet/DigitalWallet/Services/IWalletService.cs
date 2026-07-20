using DigitalWallet.Responses;

namespace DigitalWallet.Services
{
    public interface IWalletService
    {
        Task<ServiceResult> GetBalance(int userId);
    }
}
