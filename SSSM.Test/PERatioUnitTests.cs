using NUnit.Framework;
using SSSM.Api;
using SSSM.Repositories;
using SSSM.Services;
using System;

namespace SSSM.Test
{
    [TestFixture]
    public class PERatioUnitTests
    {
        private IStockMarketService _service;

        [SetUp]
        public void SetUp()
        {
            var repository = new StockMarketRepository();
            _service = new StockMarketService(repository);
        }

        [Test]
        public void When_Price_Is_Less_Or_Equal_To_Zero_An_Exception_Is_Thrown()
        {
            Assert.Throws<ArgumentException>(() => _service.GetPERatioFor("POP", 0M));
            Assert.Throws<ArgumentException>(() => _service.GetPERatioFor("POP", -1M));
        }

        [Test]
        public void When_Price_Is_Valid_And_LastDividend_Greater_Then_The_PERatio_Is_The_Expected()
        {
            var price = 123M;
            Assert.AreEqual(_service.GetPERatioFor("POP", price), price / 8);
            Assert.AreEqual(_service.GetPERatioFor("ALE", price), price / 23);
            Assert.AreEqual(_service.GetPERatioFor("GIN", price), price / 8);
            Assert.AreEqual(_service.GetPERatioFor("JOE", price), price / 13);
        }

        [Test]
        public void When_Price_Is_Valid_And_LastDividend_Is_Zero_Then_The_PERatio_Is_Zero()
        {
            var price = 456.23M;
            Assert.AreEqual(_service.GetPERatioFor("TEA", price), 0);
        }
    }
}