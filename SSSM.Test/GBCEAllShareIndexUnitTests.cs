using NUnit.Framework;
using SSSM.Api;
using SSSM.Model;
using SSSM.Repositories;
using SSSM.Services;
using System;

namespace SSSM.Test
{
    [TestFixture]
    public class GBCEAllShareIndexUnitTests
    {
        private IStockMarketRepository _repository;
        private IStockMarketService _service;

        [SetUp]
        public void SetUp()
        {
            _repository = new StockMarketRepository();
            _service = new StockMarketService(_repository);
        }

        [Test]
        public void When_There_Are_Trades_Then_GBCEAllShareIndex_Is_The_Expected()
        {
            var now = new DateTime(2021, 10, 18, 10, 15, 0);
            _service.RecordTrade("TEA", new DateTime(2021, 10, 18, 10, 0, 0), 1, TradeIndicator.BUY, 50);
            _service.RecordTrade("TEA", new DateTime(2021, 10, 18, 10, 2, 0), 2, TradeIndicator.SELL, 100);
            _service.RecordTrade("TEA", new DateTime(2021, 10, 18, 10, 5, 0), 3, TradeIndicator.BUY, 150);
            _service.RecordTrade("TEA", new DateTime(2021, 10, 18, 10, 10, 0), 4, TradeIndicator.SELL, 200);

            _service.RecordTrade("POP", new DateTime(2021, 10, 18, 10, 0, 0), 5, TradeIndicator.BUY, 200);
            _service.RecordTrade("POP", new DateTime(2021, 10, 18, 10, 2, 0), 6, TradeIndicator.SELL, 100);
            _service.RecordTrade("POP", new DateTime(2021, 10, 18, 10, 5, 0), 7, TradeIndicator.BUY, 50);
            _service.RecordTrade("POP", new DateTime(2021, 10, 18, 10, 10, 0), 8, TradeIndicator.SELL, 250);

            _service.RecordTrade("ALE", new DateTime(2021, 10, 18, 10, 0, 0), 9, TradeIndicator.BUY, 200);
            _service.RecordTrade("ALE", new DateTime(2021, 10, 18, 10, 2, 0), 10, TradeIndicator.SELL, 100);
            _service.RecordTrade("ALE", new DateTime(2021, 10, 18, 10, 5, 0), 11, TradeIndicator.BUY, 50);
            _service.RecordTrade("ALE", new DateTime(2021, 10, 18, 10, 10, 0), 12, TradeIndicator.SELL, 250);

            var gBCEAllShareIndex = _service.GBCEAllShareIndex(now);

            var teaPrice = (decimal)(50 * 1 + 100 * 2 + 150 * 3 + 200 * 4) / (1 + 2 + 3 + 4);
            var popPrice = (decimal)(200 * 5 + 100 * 6 + 50 * 7 + 250 * 8) / (5 + 6 + 7 + 8);
            var alePrice = (decimal)(200 * 9 + 100 * 10 + 50 * 11 + 250 * 12) / (9 + 10 + 11 + 12);

            Assert.AreEqual((decimal)Math.Pow((double)(teaPrice * popPrice * alePrice), 1d / 3), gBCEAllShareIndex);
        }

        [Test]
        public void When_There_Are_Not_Trades_Then_GBCEAllShareIndex_Is_Equal_To_Zero()
        {
            Assert.AreEqual(0, _service.GBCEAllShareIndex());
        }
    }
}
