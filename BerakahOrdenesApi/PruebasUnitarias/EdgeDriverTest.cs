using Microsoft.VisualStudio.TestTools.UnitTesting;
using OpenQA.Selenium;
using OpenQA.Selenium.Edge;

namespace PruebasUnitarias
{
    [TestClass]
    public class EdgeDriverTest
    {

        [TestMethod]
        public void InicioSesion()
        {
            // Replace with your own test logic
            _driver.Url = "https://www.bing.com";
            Assert.AreEqual("Bing", _driver.Title);
        }

    }
}
