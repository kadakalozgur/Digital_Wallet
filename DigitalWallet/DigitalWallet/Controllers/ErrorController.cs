using Microsoft.AspNetCore.Diagnostics;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace DigitalWallet.Controllers
{
    [ApiExplorerSettings(IgnoreApi = true)]
    public class ErrorController : ControllerBase
    {
        [Route("/error")]
        public IActionResult HandleError()
        {
            var exceptionHandlerFeature = HttpContext.Features.Get<IExceptionHandlerFeature>();
            var exception = exceptionHandlerFeature?.Error;

            return Problem(
                detail: "Sistemde beklenmeyen bir hata oluştu. Lütfen daha sonra tekrar deneyin.",
                statusCode: 500,
                title: "Sunucu Hatası"
            );
        }
    }
}
