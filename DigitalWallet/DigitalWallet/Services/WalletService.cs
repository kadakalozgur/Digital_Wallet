using DigitalWallet.Data;
using DigitalWallet.DTO;
using DigitalWallet.Models;
using DigitalWallet.Responses;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace DigitalWallet.Services
{
    public class WalletService : IWalletService
    {
        private readonly Database _context;
        private readonly ILogger<WalletService> _logger;

        public WalletService(Database context, ILogger<WalletService> logger)
        {
            _context = context; 
            _logger = logger;   
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

            var process = new Process
            {
                UserId = userId,
                Amount = amount,
                Type = ProcessType.Deposit,
                Description = $"{amount} TL hesaba yatırıldı."
            };
            _context.Transactions.Add(process);

            await _context.SaveChangesAsync();

            _logger.LogInformation("PARA YATIRMA: {UserId} ID'li kullanıcı hesabına {Amount} TL yatırdı. Yeni Bakiye: {NewBalance}", userId, amount, user.Wallet.Money);

            return new ServiceResult
            {
                IsSuccess = true,
                Message = $"İşlem başarılı! Hesabınıza {amount} TL yatırıldı. Yeni bakiyeniz: {user.Wallet.Money} TL"
            };
        }

        public async Task<ServiceResult> Withdraw(int userId, decimal amount)
        {
            if (amount <= 0)
            {
                return new ServiceResult
                {
                    IsSuccess = false,
                    Message = "Geçersiz işlem! Çekilecek tutar 0'dan büyük olmalıdır."
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

            if (user.Wallet.Money < amount)
            {
                _logger.LogWarning("BAŞARISIZ İŞLEM (Yetersiz Bakiye): {UserId} ID'li kullanıcı {Amount} TL çekmek istedi. Mevcut Bakiye: {Balance}", userId, amount, user.Wallet.Money);

                return new ServiceResult
                {
                    IsSuccess = false,
                    Message = $"Yetersiz bakiye! Mevcut bakiyeniz ({user.Wallet.Money} TL), çekmek istediğiniz tutarı ({amount} TL) karşılamıyor."
                };
            }

            user.Wallet.Money -= amount;

            var process = new Process
            {
                UserId = userId,
                Amount = amount,
                Type = ProcessType.Withdraw,
                Description = $"{amount} TL hesaptan çekildi."
            };
            _context.Transactions.Add(process);

            await _context.SaveChangesAsync();

            _logger.LogInformation("PARA ÇEKME: {UserId} ID'li kullanıcı hesabından {Amount} TL çekti. Kalan Bakiye: {NewBalance}", userId, amount, user.Wallet.Money);

            return new ServiceResult
            {
                IsSuccess = true,
                Message = $"İşlem başarılı! Hesabınızdan {amount} TL çekildi. Kalan bakiyeniz: {user.Wallet.Money} TL"
            };
        }

        public async Task<ServiceResult> Transfer(int senderId, string receiverTC, decimal amount)
        {
            if (amount <= 0)
            {
                return new ServiceResult
                { 
                    IsSuccess = false, 
                    Message = "Transfer tutarı 0'dan büyük olmalıdır." 
                };
            }        

            var sender = await _context.Users.Include(u => u.Wallet).FirstOrDefaultAsync(u => u.Id == senderId);
            var receiver = await _context.Users.Include(u => u.Wallet).FirstOrDefaultAsync(u => u.TC == receiverTC);

            if (sender == null || sender.Wallet == null)
            {
                return new ServiceResult
                {
                    IsSuccess = false,
                    Message = "Gönderici hesap bulunamadı!"
                };
            }

            if (receiver == null || receiver.Wallet == null)
            {
                return new ServiceResult
                {
                    IsSuccess = false,
                    Message = "Alıcı hesap bulunamadı! Lütfen geçerli bir kullanıcı ID'si girin."
                };
            }

            if (sender.Id == receiver.Id)
            {
                return new ServiceResult
                {
                    IsSuccess = false,
                    Message = "Kendi kendinize para gönderemezsiniz!"
                };
            }           

            if (sender.Wallet.Money < amount)
            {
                _logger.LogWarning("BAŞARISIZ TRANSFER (Yetersiz Bakiye): {SenderId} ID'li kullanıcı, {ReceiverId} ID'li kullanıcıya {Amount} TL göndermek istedi. Mevcut Bakiye: {Balance}", senderId, receiver.Id, amount, sender.Wallet.Money);

                return new ServiceResult 
                { 
                    IsSuccess = false, 
                    Message = $"Yetersiz bakiye! Mevcut bakiyeniz ({sender.Wallet.Money} TL), göndermek istediğiniz tutarı karşılamıyor." 
                };
            }

            sender.Wallet.Money -= amount;
            receiver.Wallet.Money += amount;

            var senderProcess = new Process
            {
                UserId = senderId,
                Amount = amount,
                Type = ProcessType.Transfer,
                Description = $"{receiver.Name} {receiver.Surname} adlı kişiye {amount} TL transfer yapıldı."
            };
            _context.Transactions.Add(senderProcess);

            var receiverProcess = new Process
            {
                UserId = receiver.Id,
                Amount = amount,
                Type = ProcessType.Transfer,
                Description = $"Hesabınıza {amount} TL transfer geldi."
            };
            _context.Transactions.Add(receiverProcess);

            await _context.SaveChangesAsync();

            _logger.LogInformation("TRANSFER BAŞARILI: {SenderId} ID'li kullanıcı, {ReceiverId} ID'li kullanıcıya {Amount} TL gönderdi. Gönderen Kalan Bakiye: {NewBalance}", senderId, receiver.Id, amount, sender.Wallet.Money);

            return new ServiceResult
            {
                IsSuccess = true,
                Message = $"Transfer başarılı! {receiver.Name} {receiver.Surname} adlı kullanıcıya {amount} TL gönderdiniz. Kalan bakiyeniz: {sender.Wallet.Money} TL"
            };
        }

        public async Task<ServiceResult> GetHistory(int userId,FilterDTO filter)
        {
            var query = _context.Transactions
                    .Where(t => t.UserId == userId)
                    .OrderByDescending(t => t.Date);

            var history = await query
                    .Skip((filter.PageNumber - 1) * filter.PageSize)
                    .Take(filter.PageSize)
                    .ToListAsync();

            if (!history.Any())
            {
                return new ServiceResult
                {
                    IsSuccess = false,
                    Message = "Belirtilen sayfada hiçbir işlem geçmişi bulunamadı."
                };
            }

            return new ServiceResult
            {
                IsSuccess = true,
                Message = $"Sayfa {filter.PageNumber} başarıyla getirildi.",
                Data = history
            };
        }
    }
}
