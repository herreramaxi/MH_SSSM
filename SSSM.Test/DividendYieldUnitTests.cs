using NUnit.Framework;
using SSSM.Api;
using SSSM.Model;
using SSSM.Repositories;
using SSSM.Services;
using System;

namespace SSSM.Test
{
    [TestFixture]
    public class DividendYieldUnitTests
    {
        private IStockMarketService _service;

        [SetUp]
        public void SetUp()
        {
            var repository = new StockMarketRepository();
            _service = new StockMarketService(repository);
        }

        [Test]
        public void When_Last_Dividend_Is_Greater_Than_Zero_Then_DividendYield_Is_The_Expected_For_Common_Stock()
        {
            var price = 123.0M;

            Assert.AreEqual(_service.GetDividendYield("POP", price), 8 / price);
            Assert.AreEqual(_service.GetDividendYield("ALE", price), 23 / price);           
            Assert.AreEqual(_service.GetDividendYield("JOE", price), 13 / price);          
        }

        [Test]
        public void When_LastDividend_Is_Zero_Then_DividendYield_Is_Zero_For_CommonStock()
        {
            var price = 123M;

            Assert.AreEqual(_service.GetDividendYield("TEA", price), 0);
        }

        [Test]
        public void When_Price_Is_Zero_GetDividendYield_Throws_ArgumentException_For_CommonStock()
        {
            Assert.Throws<ArgumentException>(() => _service.GetDividendYield("POP", 0M));
        }

        [Test]
        public void When_Price_Is_Zero_GetDividendYield_Throws_ArgumentException_For_PreferredStock()
        {
            Assert.Throws<ArgumentException>(() => _service.GetDividendYield("GIN", 0M));
        }

        [Test]
        public void When_Numerator_And_Price_Are_Greater_Than_Zero_Then_DividendYield_Is_The_Expected_For_PreferredStock()
        {
            var price = 123M;
            Assert.AreEqual(_service.GetDividendYield("GIN", price), 0.02M * 100 / price);
        }

        [Test]
        public void When_FixedDividend_OrParValue_Are_Zero_Then_DividendYield_For_Preferred_Stock_Is_Zero()
        {
            var price = 123M;
            var stockFixedDividendZero = new PreferredStock("ABC", 1, 0, 10);
            Assert.AreEqual(stockFixedDividendZero.DividendYield(price), 0);

            var stockParValueZero = new PreferredStock("ABC", 1, 10, 0);
            Assert.AreEqual(stockParValueZero.DividendYield(price), 0);
        }       
    }
}