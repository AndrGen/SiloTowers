using Common.Helper;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using SiloTower.Api.Implementations;
using SiloTower.Interfaces.Auth;

namespace SiloTowersUnitTest
{

    [TestClass]
    public class TokenTest
    {
        [TestMethod]
        public void GenerateToken()
        {
            var config = AppFileConfiguration.GetConfiguration(@"appsettings.json");
            IToken tokenImpl = new TokenImpl(config);
            string token = tokenImpl.GenerateToken();
            Assert.IsNotNull(token, "Токен не задан");
        }
    }
}
