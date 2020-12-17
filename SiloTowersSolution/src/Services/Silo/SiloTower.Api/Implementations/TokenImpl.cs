using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using SiloTower.Interfaces.Auth;
using System;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace SiloTower.Api.Implementations
{
    public class TokenImpl : IToken
    {
        private readonly IConfiguration _config;
        public TokenImpl(IConfiguration configuration)
        {
            _config = configuration;
        }
        public string GenerateToken()
        {
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

            return new JwtSecurityTokenHandler().WriteToken(token);
        }
    }
}
