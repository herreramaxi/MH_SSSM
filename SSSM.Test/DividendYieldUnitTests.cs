using NUnit.Framework;
using SSSM.Model;
using SSSM.Repositories;
using SSSM.Services;
using System;

namespace SSSM.Test
{
    [TestFixture]
    public class Tests
    {
        private StockMarketService _service;

        [OneTimeSetUp]
        public void OnceSetup()
        {
            var repository = new StockMarketRepository();
            _service = new StockMarketService(repository);
        }

        [Test]
        public void Test_DividendYield_For_Sample_Data()
        {
            var price = 123.0M;

            Assert.AreEqual(_service.GetDividendYieldFor("TEA", price), 0M);
            Assert.AreEqual(_service.GetDividendYieldFor("POP", price), 8 / price);
            Assert.AreEqual(_service.GetDividendYieldFor("ALE", price), 23 / price);
            Assert.AreEqual(_service.GetDividendYieldFor("GIN", price), 0.02M * 100 / price);
            Assert.AreEqual(_service.GetDividendYieldFor("JOE", price), 13 / price);
        }

        [Test]
        public void When_LastDividend_Is_Zero_For_CommonStock_Then_DividendYield_Is_Zero()
        {
            var price = 123M;

            Assert.AreEqual(_service.GetDividendYieldFor("TEA", price), 0);
        }

        [Test]
        public void When_Price_Is_Zero_DividendYield_Throws_ArgumentException()
        {
            Assert.Throws<ArgumentException>(() => _service.GetDividendYieldFor("POP", 0M));
            Assert.Throws<ArgumentException>(() => _service.GetDividendYieldFor("GIN", 0M));
        }


        [Test]
        public void When_Numerator_And_Price_Are_Different_Than_Zero__For_PreferredStock_Then_The_Price_Is_The_Expected()
        {
            var price = 123.0M;
            Assert.AreEqual(_service.GetDividendYieldFor("GIN", price), 0.02M * 100 / price);
        }

        [Test]
        public void When_FixedDividend_Is_Zero_Then_DividendYield_For_Preferred_Stock_Is_Zero()
        {
            var price = 123M;
            var stockFixedDividendZero = new PreferredStock("ABC", 1, 0, 10);
            Assert.AreEqual(stockFixedDividendZero.DividendYield(price), 0);
        }

        [Test]
        public void When_ParValue_Is_Zero_Then_DividendYield_For_Preferred_Stock_Is_Zero()
        {
            var price = 123M;
            var stockParValueZero = new PreferredStock("ABC", 1, 10, 0);
            Assert.AreEqual(stockParValueZero.DividendYield(price), 0);
        }
    }
}