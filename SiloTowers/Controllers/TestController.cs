using Microsoft.AspNetCore.Mvc;
using Serilog;
using SiloTowers.Helper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SiloTowers.Controllers
{
    [Route("")]
    public class TestController : Controller
    {
        private readonly ILogger _logger = LoggerHelper.Logger;
        
        [HttpGet]
        public string Index()
        {
            _logger.Debug("Тест");
            return "Тест";
        }
    }
}
