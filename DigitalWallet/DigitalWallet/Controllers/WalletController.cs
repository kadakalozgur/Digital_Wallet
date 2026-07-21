using DigitalWallet.DTO;
using DigitalWallet.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace DigitalWallet.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
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
    }
}
