using Microsoft.AspNetCore.Diagnostics;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Net;
using Microsoft.Extensions.Logging;

namespace SiloTower.Api.Controllers
{
    [ApiExplorerSettings(IgnoreApi = true)]
    public class ErrorController : Controller
    {
        private readonly ILogger<ErrorController> _logger;

        public ErrorController(ILogger<ErrorController>? logger)
        {
            _logger = logger;
        }

        [Route("/error")]
        [ProducesResponseType(StatusCodes.Status502BadGateway)]
        public IActionResult Error()
        {
            var context = HttpContext.Features.Get<IExceptionHandlerFeature>();

            _logger.LogCritical($"ErrorController fatal error: {context.Error.Message} \n {context.Error.StackTrace}");
            
            return Problem(
                statusCode: (int)HttpStatusCode.BadGateway,
                detail: context.Error.Message + " || " + context.Error.StackTrace,
                title: context.Error.Message);
        }
    }
}
