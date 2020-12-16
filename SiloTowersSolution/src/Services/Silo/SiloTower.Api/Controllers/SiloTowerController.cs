using Common.Helper;
using Microsoft.AspNetCore.Authorization;
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
    [Authorize]
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
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status502BadGateway)]
        public async Task<ActionResult<IList<SiloIndicators>>> GetSiloIndicators()
        {
            try
            {
                _logger.Debug("GetSiloIndicators start");
                var res = await _siloTowerValues.GetSiloIndicators();
                if (res.Count > 0)
                    return Ok(res);
                return NotFound("Значений не найдено");
            }
            catch (Exception ex) when
            (ex is ArgumentNullException ||
             ex is InvalidOperationException)
            {
                _logger.Error("GetSiloIndicators ", ex, ex.Message);
                return Problem(
                    statusCode: (int)HttpStatusCode.InternalServerError,
                    detail: ex.StackTrace,
                    title: ex.Message);
            }
        }

        [HttpPost("SaveIndicator")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status502BadGateway)]
        public async Task<ActionResult<bool>> SaveSiloIndicators([FromBody] SaveSiloIndicatorRequest saveSiloIndicatorRequest)
        {
            try
            {
                _logger.Debug("SaveSiloIndicators start");

                if (await _siloTowerValues.SaveSiloIndicators(saveSiloIndicatorRequest))
                    return NoContent();
                return BadRequest("Не выполнено сохранение значений");
            }
            catch (Exception ex) when
            (ex is ArgumentNullException ||
             ex is InvalidOperationException)
            {
                _logger.Error("SaveSiloIndicators ", ex, ex.Message);
                return Problem(
                    statusCode: (int)HttpStatusCode.InternalServerError,
                    detail: ex.StackTrace,
                    title: ex.Message);
            }
        }
    }
}
