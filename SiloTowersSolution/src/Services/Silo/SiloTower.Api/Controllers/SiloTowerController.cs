using Common.Helper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Serilog;
using SiloTower.Domain.Silo;
using SiloTower.Interfaces.Silo;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;

namespace SiloTower.Api.Controllers
{
    [Route("api")]
    public class SiloTowerController : Controller
    {
        private readonly ILogger _logger = LoggerHelper.Logger;
        private readonly ISiloTowerValues _siloTowerValues;

        public SiloTowerController(ISiloTowerValues siloTowerValues)
        {
            _siloTowerValues = siloTowerValues ?? throw new ArgumentNullException(nameof(siloTowerValues));
        }

        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<ActionResult<IList<SiloIndicators>>> GetSiloIndicators()
        {
            try
            {
                _logger.Debug("GetSiloIndicators start");
                return Ok(await _siloTowerValues.GetSiloIndicators());
            }
            catch (InvalidOperationException ioe)
            {
                _logger.Error("GetSiloIndicators ", ioe, ioe.Message);
                return Problem(
                    statusCode: (int)HttpStatusCode.InternalServerError,
                    detail: ioe.StackTrace,
                    title: ioe.Message);
            }
        }
    }
}
