using Microsoft.AspNetCore.Diagnostics;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Serilog;
using SiloTowerApi.Helper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;

namespace SiloTowerApi.Controllers
{
    [ApiExplorerSettings(IgnoreApi = true)]
    public class ErrorController : Controller
    {
        private readonly ILogger _logger = LoggerHelper.Logger;

        [Route("/error")]
        [ProducesResponseType(StatusCodes.Status502BadGateway)]
        public IActionResult Error()
        {
            var context = HttpContext.Features.Get<IExceptionHandlerFeature>();

            _logger.Fatal($"ErrorController fatal error: {context.Error.Message} \n {context.Error.StackTrace}");
            
            return Problem(
                statusCode: (int)HttpStatusCode.BadGateway,
                detail: context.Error.StackTrace,
                title: context.Error.Message);
        }
    }
}
