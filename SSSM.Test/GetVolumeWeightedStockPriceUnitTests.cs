using NUnit.Framework;
using SSSM.Api;
using SSSM.Model;
using SSSM.Repositories;
using SSSM.Services;
using System;
using System.Collections.Generic;

namespace SSSM.Test
{
    [TestFixture]
    public class GetVolumeWeightedStockPriceUnitTests
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
        public void When_There_Are_Trades_Then_GetVolumeWeightedStockPrice_Is_The_Expected()
        {
            var now = new DateTime(2021, 10, 18, 10, 15, 0);
            var t1 = _service.RecordTrade("TEA", new DateTime(2021, 10, 18, 9, 0, 0), 10, TradeIndicator.BUY, 100);

            var t2 = _service.RecordTrade("TEA", new DateTime(2021, 10, 18, 10, 0, 0), 1, TradeIndicator.BUY, 50);
            var t3 = _service.RecordTrade("TEA", new DateTime(2021, 10, 18, 10, 2, 0), 2, TradeIndicator.SELL, 100);
            var t4 = _service.RecordTrade("TEA", new DateTime(2021, 10, 18, 10, 5, 0), 3, TradeIndicator.BUY, 150);
            var t5 = _service.RecordTrade("TEA", new DateTime(2021, 10, 18, 10, 10, 0), 4, TradeIndicator.SELL, 200);

            var t6 = _service.RecordTrade("TEA", new DateTime(2021, 10, 18, 11, 0, 0), 10, TradeIndicator.BUY, 100);

            var latest15MinTrades = _repository.GetLast15MinTrades("TEA", now);

            CollectionAssert.AreEqual(new List<Trade>() { t2, t3, t4, t5 }, latest15MinTrades);

            var volumeWeightedStockPrice = _service.GetVolumeWeightedStockPrice("TEA", now);

            Assert.AreEqual(volumeWeightedStockPrice, (decimal)(1 * 50 + 2 * 100 + 3 * 150 + 4 * 200) / (1 + 2 + 3 + 4));
        }

        [Test]
        public void When_There_Are_Not_Trades_Then_GetVolumeWeightedStockPrice_Is_Equal_To_Zero()
        {
            var now = new DateTime(2021, 10, 18, 10, 15, 0);
             _service.RecordTrade("TEA", new DateTime(2021, 10, 18, 9, 0, 0), 10, TradeIndicator.BUY, 100);
             _service.RecordTrade("TEA", new DateTime(2021, 10, 18, 9, 30, 0), 10, TradeIndicator.BUY, 100);
             _service.RecordTrade("TEA", new DateTime(2021, 10, 18, 9, 50, 0), 10, TradeIndicator.BUY, 100);
             _service.RecordTrade("TEA", new DateTime(2021, 10, 18, 9, 59, 0), 10, TradeIndicator.BUY, 100);

            var latest15MinTrades = _repository.GetLast15MinTrades("TEA", now);
            CollectionAssert.IsEmpty(latest15MinTrades);

            Assert.AreEqual(0, _service.GetVolumeWeightedStockPrice("TEA", new DateTime(2021, 10, 18, 10, 15, 0)));
        }

        [Test]
        public void When_There_Are_Not_Trades_Within_15_Minutes_Then_GetVolumeWeightedStockPrice_Is_Equal_To_Zero()
        {
            Assert.AreEqual(0, _service.GetVolumeWeightedStockPrice("TEA", new DateTime(2021, 10, 18, 10, 15, 0)));
        }    
    }
}