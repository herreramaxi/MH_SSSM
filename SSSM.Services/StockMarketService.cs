using SSSM.Api;
using SSSM.Model;
using System;
using System.Collections.Generic;
using System.Linq;

namespace SSSM.Services
{
    public class StockMarketService : IStockMarketService
    {
        private readonly IStockMarketRepository _repository;

        public StockMarketService(IStockMarketRepository repository)
        {
            _repository = repository;
        }

        public IList<CommonStock> GetStocks()
        {
            return _repository.GetAllStocks();
        }

        public decimal GetDividendYieldFor(string stockSymbol, decimal price)
        {
            ValidatePrice(price);
            var stock = GetStock(stockSymbol);

            return stock.DividendYield(price);
        }

        public decimal GetPERatioFor(string stockSymbol, decimal price)
        {
            ValidatePrice(price);
            var stock = GetStock(stockSymbol);

            return stock.PERatio(price);
        }

        public Trade RecordTrade(string stockSymbol, DateTime timeStamp, int quantityOfShares, TradeIndicator tradeIndicator, decimal price)
        {
            ValidatePrice(price);

            return _repository.RecordTrade(stockSymbol, timeStamp, quantityOfShares, tradeIndicator, price);
        }

        public decimal GetVolumeWeightedStockPrice(string stockSymbol, DateTime? now = null)
        {
            var tradesLast15Min = _repository.GetLatest15MinTrades(stockSymbol, now ?? DateTime.Now);

            var numerator = tradesLast15Min.Sum(x => x.Price * x.QuantityOfShares);
            var denominator = tradesLast15Min.Sum(y => y.QuantityOfShares);

            return denominator > 0 ? numerator / denominator : 0;
        }

        public decimal GBCEAllShareIndex()
        {
            IList<Trade> trades = _repository.GetAllTrades();

            var pricesMultiplied = trades.Aggregate(1M, (accum, x) => accum * x.Price);
            return (decimal)Math.Pow((double)pricesMultiplied, 1d / trades.Count);
        }

        private static void ValidatePrice(decimal price)
        {
            if (price <= 0) throw new ArgumentException("Price cannot be less or equal to zero");
        }

        private CommonStock GetStock(string stockSymbol)
        {
            var stock = _repository.Get(stockSymbol);
            if (stock == null) throw new Exception($"Stock symbol not found: {stockSymbol}");
            return stock;
        }
    }
}
