using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using SiloTower.Domain.Silo;
using SiloTower.Interfaces.Silo;
using System;
using System.Collections.Generic;
using System.Net;
using System.Threading.Tasks;

namespace SiloTower.Api.Controllers
{
    [Route("api")]
    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
    public class SiloTowerController : Controller
    {
        private readonly ILogger<SiloTowerController> _logger;
        private readonly ISiloTowerValues _siloTowerValues;

        public SiloTowerController(ISiloTowerValues siloTowerValues, ILogger<SiloTowerController>? logger)
        {
            _logger = logger;
            _siloTowerValues = siloTowerValues ?? throw new ArgumentNullException(nameof(siloTowerValues));
        }

        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status502BadGateway)]
        public async Task<ActionResult<IDictionary<int, SiloIndicators>>> GetSiloIndicators()
        {
            try
            {
                _logger.LogDebug("GetSiloIndicators start");
                var res = await _siloTowerValues.GetSiloIndicators();
                if (res.Count > 0)
                    return Ok(res);
                return NotFound("Значений не найдено");
            }
            catch (Exception ex) when
            (ex is ArgumentNullException ||
            ex is ArgumentOutOfRangeException ||
             ex is InvalidOperationException)
            {
                _logger.LogError("GetSiloIndicators ", ex, ex.Message);
                return Problem(
                    statusCode: (int)HttpStatusCode.InternalServerError,
                    detail: ex.StackTrace,
                    title: ex.Message);
            }
        }

        [HttpPost("SaveIndicator")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status502BadGateway)]
        public async Task<ActionResult<bool>> SaveSiloIndicators([FromBody] SaveSiloIndicatorRequest saveSiloIndicatorRequest)
        {
            try
            {
                if (saveSiloIndicatorRequest is null) throw new ArgumentNullException(nameof(saveSiloIndicatorRequest));
                if (saveSiloIndicatorRequest.TowerId < 1) throw new ArgumentOutOfRangeException(nameof(saveSiloIndicatorRequest.TowerId));
                if (saveSiloIndicatorRequest.LevelValue < 0) throw new ArgumentOutOfRangeException(nameof(saveSiloIndicatorRequest.LevelValue));
                if (saveSiloIndicatorRequest.WeightValue < 0) throw new ArgumentOutOfRangeException(nameof(saveSiloIndicatorRequest.WeightValue));

                _logger.LogDebug("SaveSiloIndicators start");

                if (await _siloTowerValues.SaveSiloIndicators(saveSiloIndicatorRequest))
                    return NoContent();
                return BadRequest("Не выполнено сохранение значений");
            }
            catch (Exception ex) when
            (ex is ArgumentNullException ||
            ex is ArgumentOutOfRangeException ||
             ex is InvalidOperationException)
            {
                _logger.LogError("SaveSiloIndicators ", ex, ex.Message);
                return Problem(
                    statusCode: (int)HttpStatusCode.InternalServerError,
                    detail: ex.StackTrace,
                    title: ex.Message);
            }
        }
    }
}
