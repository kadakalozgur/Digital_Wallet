using DigitalWallet.DTO;
using DigitalWallet.Services;
using IdempotentAPI.Filters;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.RateLimiting;
using System.Security.Claims;

namespace DigitalWallet.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    [Produces("application/json")]
    [EnableRateLimiting("WalletLimiter")]
    public class WalletController : ControllerBase
    {
        private readonly IWalletService _walletService;

        public WalletController(IWalletService walletService)
        {
            _walletService = walletService;
        }

        [HttpGet("balance")]
        public async Task<IActionResult> GetBalance()
        {
            var userIdString = User.FindFirstValue(ClaimTypes.NameIdentifier);

            if (string.IsNullOrEmpty(userIdString))
            {
                return Unauthorized("Kimlik doğrulama hatası! Geçersiz Token.");
            }

            int userId = int.Parse(userIdString);

            var result = await _walletService.GetBalance(userId);

            if (!result.IsSuccess)
            {
                return NotFound(result);
            }

            return Ok(result);

        }

        [HttpPost("deposit")]
        [Idempotent(ExpireHours = 1)]
        public async Task<IActionResult> Deposit([FromBody] DepositDTO dto)
        {
            var userIdString = User.FindFirstValue(ClaimTypes.NameIdentifier);

            if (string.IsNullOrEmpty(userIdString))
            {
                return Unauthorized("Kimlik doğrulama hatası! Geçersiz Token.");
            }

            int userId = int.Parse(userIdString);

            var result = await _walletService.Deposit(userId, dto.Amount);

            if (!result.IsSuccess)
            {
                return NotFound(result);
            }

            return Ok(result);

        }

        [HttpPost("withdraw")]
        [Idempotent(ExpireHours = 1)]
        public async Task<IActionResult> Withdraw([FromBody] WithdrawDTO dto)
        {
            var userIdString = User.FindFirstValue(ClaimTypes.NameIdentifier);

            if (string.IsNullOrEmpty(userIdString))
            {
                return Unauthorized("Kimlik doğrulama hatası! Geçersiz Token.");
            }

            int userId = int.Parse(userIdString);

            var result = await _walletService.Withdraw(userId, dto.Amount);

            if (!result.IsSuccess)
            {
                return BadRequest(result);
            }

            return Ok(result);

        }

        [HttpPost("transfer")]
        [Idempotent(ExpireHours = 1)]
        public async Task<IActionResult> Transfer([FromBody] TransferDTO dto)
        {
            var userIdString = User.FindFirstValue(ClaimTypes.NameIdentifier);

            if (string.IsNullOrEmpty(userIdString))
            {
                return Unauthorized("Kimlik doğrulama hatası! Geçersiz Token.");
            }

            int senderId = int.Parse(userIdString);

            var result = await _walletService.Transfer(senderId, dto.ReceiverTC, dto.Amount);

            if (!result.IsSuccess)
            {
                return BadRequest(result); 
            }

            return Ok(result);

        }

        [HttpGet("history")]
        public async Task<IActionResult> GetHistory([FromQuery] FilterDTO filter)
        {
            var userIdString = User.FindFirstValue(ClaimTypes.NameIdentifier);

            if (string.IsNullOrEmpty(userIdString))
            {
                return Unauthorized("Geçersiz Token.");
            }

            int userId = int.Parse(userIdString);
            var result = await _walletService.GetHistory(userId,filter);

            if (!result.IsSuccess)
            {
                return NotFound(result);
            }

            return Ok(result); 

        }
    }
}
