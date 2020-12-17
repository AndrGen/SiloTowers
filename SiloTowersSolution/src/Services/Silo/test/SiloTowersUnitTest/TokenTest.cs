using Microsoft.Extensions.Configuration;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using SiloTower.Api.Implementations;
using SiloTower.Interfaces.Auth;

namespace SiloTowersUnitTest
{

    [TestClass]
    public class TokenTest
    {
        private Mock<IConfiguration> _config = new Mock<IConfiguration>();

        public TokenTest()
        {
            _config.Setup(x => x["Tokens:Issuer"]).Returns("This is my custom Issuer");
            _config.Setup(x => x["Tokens:Key"]).Returns("This is my custom Secret key for authnetication");
        }

        [TestMethod]
        public void GenerateToken()
        {
            IToken tokenImpl = new TokenImpl(_config.Object);
            string token = tokenImpl.GenerateToken();
            Assert.IsNotNull(token, "Токен не задан");
        }
    }
}
