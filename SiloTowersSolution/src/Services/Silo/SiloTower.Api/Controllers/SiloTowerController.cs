using Common.Helper;
using Microsoft.AspNetCore.Mvc;
using Serilog;
using SiloTower.Domain.Silo;
using SiloTower.Interfaces.Silo;
using System;
using System.Collections.Generic;
using System.Linq;
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
        public async Task<ActionResult<IList<SiloIndicators>>> GetSiloIndicators()
        {
            return Ok(await _siloTowerValues.GetSiloIndicators());
        }
    }
}
