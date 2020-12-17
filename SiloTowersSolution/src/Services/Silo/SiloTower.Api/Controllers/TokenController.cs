using Common.Helper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using SiloTower.Interfaces.Auth;
using System;

namespace SiloTower.Api.Controllers
{
    [Route("connect")]
    public class TokenController : Controller
    {
        private readonly ILogger<TokenController> _logger;
        private readonly IConfiguration _config;
        private readonly IToken _token;

        public TokenController(IConfiguration configuration, ILogger<TokenController> logger, IToken token)
        {
            _config = configuration;
            _logger = logger;
            _token = token ?? throw new ArgumentNullException(nameof(token));
        }

        [AllowAnonymous]
        [HttpPost]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status201Created)]
        public IActionResult GenerateToken([FromBody] string publicPass)
        {
            _logger.LogDebug("GenerateToken start");
            //MyPassw0rd
            if (Md5Helper.CreateMD5(publicPass) != _config["PublicPass"])
                return BadRequest("Неверный пароль");

            return CreatedAtAction(nameof(GenerateToken), _token.GenerateToken());
        }
    }
}

