using DigitalWallet.Data;
using DigitalWallet.Responses;
using Microsoft.EntityFrameworkCore;

namespace DigitalWallet.Services
{
    public class WalletService : IWalletService
    {
        private readonly Database _context;

        public WalletService(Database context)
        {
            _context = context; 
        }

        public async Task<ServiceResult> GetBalance(int userId)
        {
            var user = await _context.Users.Include(u => u.Wallet).FirstOrDefaultAsync(u => u.Id == userId);

            if (user == null)
            {
                return new ServiceResult
                {
                    IsSuccess = false,
                    Message = "Kullanıcı bulunamadı!"
                };
            }

            if (user.Wallet == null)
            {
                return new ServiceResult
                {
                    IsSuccess = false,
                    Message = "Bu kullanıcıya ait bir cüzdan bulunamadı!"
                };
            }

            return new ServiceResult
            {
                IsSuccess = true,
                Message = $"Sayın {user.Name} {user.Surname}, güncel bakiyeniz: {user.Wallet.Money} TL"
            };
        }

        public async Task<ServiceResult> Deposit(int userId, decimal amount)
        {
            if (amount <= 0)
            {
                return new ServiceResult
                {
                    IsSuccess = false,
                    Message = "Geçersiz işlem! Yatırılacak tutar 0'dan büyük olmalıdır."
                };
            }

            var user = await _context.Users.Include(u => u.Wallet).FirstOrDefaultAsync(u => u.Id == userId);

            if (user == null || user.Wallet == null)
            {
                return new ServiceResult
                {
                    IsSuccess = false,
                    Message = "Kullanıcı veya cüzdan bulunamadı!"
                };
            }

            user.Wallet.Money += amount;

            await _context.SaveChangesAsync();

            return new ServiceResult
            {
                IsSuccess = true,
                Message = $"İşlem başarılı! Hesabınıza {amount} TL yatırıldı. Yeni bakiyeniz: {user.Wallet.Money} TL"
            };
        }
    }
}
