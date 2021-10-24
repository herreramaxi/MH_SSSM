using SSSM.Api;
using SSSM.Model;
using System;
using System.Collections.Generic;

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

        public CommonStock GetStock(string stockSymbol)
        {
            var stock = _repository.Get(stockSymbol);
            if (stock == null) throw new Exception($"Stock symbol not found: {stockSymbol}");
            return stock;
        }

        public StockCalculations GetStockCalculations(string stockSymbol)
        {
            var stock = GetStock(stockSymbol);

            var stockCalculations = new StockCalculations(stock);
            stockCalculations.VolumeWeightedStockPrice = this.GetVolumeWeightedStockPrice(stockSymbol);
            stockCalculations.GBCEAllShareIndex = this.GBCEAllShareIndex();

            return stockCalculations;
        }

        public IList<Trade> GetTrades()
        {
            return _repository.GetAllTrades();
        }

        public Trade RecordTrade(string stockSymbol, DateTime timeStamp, int quantityOfShares, TradeIndicator tradeIndicator, decimal price)
        {
            ValidatePrice(price);

            return _repository.RecordTrade(stockSymbol, timeStamp, quantityOfShares, tradeIndicator, price);
        }

        public decimal GetDividendYield(string stockSymbol, decimal price)
        {
            ValidatePrice(price);

            return GetStock(stockSymbol).DividendYield(price);
        }

        public decimal GetPERatioFor(string stockSymbol, decimal price)
        {
            ValidatePrice(price);

            return GetStock(stockSymbol).PERatio(price);
        }

        public decimal GetVolumeWeightedStockPrice(string stockSymbol, DateTime? now = null)
        {
            var tradesLast15Min = _repository.GetLast15MinTrades(stockSymbol, now ?? DateTime.Now);
            var priceXQuantity = 0M;
            var quantity = 0M;

            foreach (var trade in tradesLast15Min)
            {
                priceXQuantity += trade.Price * trade.QuantityOfShares;
                quantity += trade.QuantityOfShares;
            }

            return quantity > 0 ? priceXQuantity / quantity : 0;
        }

        public decimal GBCEAllShareIndex(DateTime? now = null)
        {
            var trades = _repository.GetLast15MinTrades(null, now ?? DateTime.Now);
            var priceXQuantityByStock = new Dictionary<string, decimal>();
            var quantityByStock = new Dictionary<string, decimal>();

            foreach (var trade in trades)
            {
                if (!priceXQuantityByStock.ContainsKey(trade.StockSymbol))
                {
                    priceXQuantityByStock[trade.StockSymbol] = 0;
                    quantityByStock[trade.StockSymbol] = 0;
                }

                priceXQuantityByStock[trade.StockSymbol] += trade.Price * trade.QuantityOfShares;
                quantityByStock[trade.StockSymbol] += trade.QuantityOfShares;
            }

            var price = 1M;
            foreach (var stockSymbol in priceXQuantityByStock.Keys)
            {
                price *= priceXQuantityByStock[stockSymbol] / quantityByStock[stockSymbol];
            }

            return priceXQuantityByStock.Count > 0 ? (decimal)Math.Pow((double)price, 1d / priceXQuantityByStock.Count) : 0;
        }

        public void ClearOnMemoryData()
        {
            _repository.ClearOnMemoryData();
        }

        private static void ValidatePrice(decimal price)
        {
            if (price <= 0) throw new ArgumentException("Price cannot be less or equal to zero");
        }
    }
}