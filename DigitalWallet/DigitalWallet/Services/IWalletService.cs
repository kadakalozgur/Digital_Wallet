using DigitalWallet.Responses;

namespace DigitalWallet.Services
{
    public interface IWalletService
    {
        Task<ServiceResult> GetBalance(int userId);
        Task<ServiceResult> Deposit(int userId, decimal amount);
        Task<ServiceResult> Withdraw(int userId, decimal amount);
        Task<ServiceResult> Transfer(int senderId, string receiverTC, decimal amount);
    }
}
