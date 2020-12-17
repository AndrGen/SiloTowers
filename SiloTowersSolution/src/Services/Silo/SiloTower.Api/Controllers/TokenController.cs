using Common.Helper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace SiloTower.Api.Controllers
{
    [Route("connect")]
    public class TokenController : Controller
    {
        private readonly ILogger<TokenController> _logger;
        private readonly IConfiguration _config;

        public TokenController(IConfiguration configuration, ILogger<TokenController>? logger)
        {
            _config = configuration;
            _logger = logger;
        }

        [AllowAnonymous]
        [HttpPost]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public IActionResult GenerateToken([FromQuery] string publicPass)
        {
            _logger.LogDebug("GenerateToken start");
            //MyPassw0rd
            if (Md5Helper.CreateMD5(publicPass) != _config["PublicPass"])
                return BadRequest("Неверный пароль");
            
            var claims = new[]
            {
              new Claim(JwtRegisteredClaimNames.Sub, "user"),
              new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
            };

            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_config["Tokens:Key"]));
            var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

            var token = new JwtSecurityToken(_config["Tokens:Issuer"],
              _config["Tokens:Issuer"],
              claims,
              expires: DateTime.Now.AddMinutes(30),
              signingCredentials: creds);

            return Ok(new { token = new JwtSecurityTokenHandler().WriteToken(token) });
        }

    }
}
    
