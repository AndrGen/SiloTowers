using Common.Helper;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace SiloTowersUnitTest
{

    [TestClass]
    public class Md5Test
    {

        [TestMethod]
        public void GenerateToken()
        {
            string str = "Это пароль";
            var res = Md5Helper.CreateMD5(str);
            Assert.AreEqual("C7CBB97BB3A7B44CD37922FB2D03D566", res, "Md5 хеш не совпал");
        }

       
    }
}
