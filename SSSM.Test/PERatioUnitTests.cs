using NUnit.Framework;
using SSSM.Repositories;
using SSSM.Services;
using System;

namespace SSSM.Test
{
    [TestFixture]
    public class PERatioUnitTests
    {
        private StockMarketService _service;

        [OneTimeSetUp]
        public void OnceSetup()
        {
            var repository = new StockMarketRepository();
            _service = new StockMarketService(repository);
        }

        [Test]
        public void Calculate_PERatio_For_SampleData()
        {
            var price = 123M;
            Assert.Throws<DivideByZeroException>(() => _service.GetPERatioFor("TEA", price));
            Assert.AreEqual(_service.GetPERatioFor("POP", price), price / (8 / price));
            Assert.AreEqual(_service.GetPERatioFor("ALE", price), price / (23 / price));
            Assert.AreEqual(_service.GetPERatioFor("GIN", price), price / (0.02M * 100 / price));
            Assert.AreEqual(_service.GetPERatioFor("JOE", price), price / (13 / price));
        }
    }
}
