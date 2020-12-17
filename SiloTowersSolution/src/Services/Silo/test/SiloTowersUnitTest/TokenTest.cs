using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using SiloTower.Api.Controllers;
using SiloTower.Api.Implementations;
using SiloTower.Interfaces.Auth;
using System.Net;

namespace SiloTowersUnitTest
{

    [TestClass]
    public class TokenTest
    {
        private Mock<IConfiguration> _config = new Mock<IConfiguration>();
        private Mock<ILogger<TokenController>> _logger = new Mock<ILogger<TokenController>>();
        private IToken _tokenImpl;

        public TokenTest()
        {
            _config.Setup(x => x["Tokens:Issuer"]).Returns("This is my custom Issuer");
            _config.Setup(x => x["Tokens:Key"]).Returns("This is my custom Secret key for authnetication");
            _config.Setup(x => x["PublicPass"]).Returns("5BC19AF86C75C8BC1AF45B3EF4CD8717");
            _tokenImpl = new TokenImpl(_config.Object);
        }

        [TestMethod]
        public void GenerateToken()
        {
            string token = _tokenImpl.GenerateToken();
            Assert.IsNotNull(token, "Токен не задан");
        }

        [TestMethod]
        public void TokenControllerSuccess()
        {
            string publicPass = "MyPassw0rd";
            TokenController tokenController = new TokenController(_config.Object, _logger.Object, _tokenImpl);
            CreatedAtActionResult res = (CreatedAtActionResult)tokenController.GenerateToken(publicPass);
            Assert.AreEqual((int)HttpStatusCode.Created, res.StatusCode, "Получение токена не выполнено");
            Assert.IsNotNull(res.Value, "Токен не задан");
        }

        [TestMethod]
        public void TokenControllerNotSuccess()
        {
            string publicPassWrong = "wrong";
            TokenController tokenController = new TokenController(_config.Object, _logger.Object, _tokenImpl);
            BadRequestObjectResult res = (BadRequestObjectResult)tokenController.GenerateToken(publicPassWrong);
            Assert.AreEqual((int)HttpStatusCode.BadRequest, res.StatusCode, "Пароль не проверен");
            Assert.AreEqual("Неверный пароль", res.Value);
        }
    }
}
