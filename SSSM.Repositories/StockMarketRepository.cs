using SSSM.Api;
using SSSM.Model;
using System;
using System.Collections.Generic;
using System.Linq;

namespace SSSM.Repositories
{
    public class StockMarketRepository : IStockMarketRepository
    {
        private static Dictionary<string, CommonStock> _stockData;
        private readonly List<Trade> _trades = new List<Trade>();  
        
        public StockMarketRepository()
        {
            _stockData = new Dictionary<string, CommonStock>
            {
                 {  "TEA",  new CommonStock("TEA", 0, 100 ) },
                 {  "POP",  new CommonStock ( "POP", 8, 100 )},
                 {  "ALE",  new CommonStock ("ALE", 23, 60 )},
                 {  "GIN",  new PreferredStock("GIN", 8, 0.02M, 100 )},
                 {  "JOE",  new CommonStock ("JOE", 13, 250)}
            };
        }

        public CommonStock Get(string stockSymbol)
        {
            return _stockData.ContainsKey(stockSymbol) ? _stockData[stockSymbol] : null;
        }

        public IList<CommonStock> GetAllStocks()
        {
            return _stockData.Values.ToList();
        }

        public IList<Trade> GetAllTrades()
        {
            return _trades;
        }

        public IList<Trade> GetLast15MinTrades(string stockSymbol, DateTime now)
        {
            var nowAsUtc = now.ToUniversalTime();
            return _trades.Where(x => (string.IsNullOrEmpty(stockSymbol) || x.StockSymbol.Equals(stockSymbol, StringComparison.InvariantCultureIgnoreCase)) &&
                                      nowAsUtc > x.TimeStamp &&
                                      x.TimeStamp.AddMinutes(15) >= nowAsUtc).ToList();
        }

        public Trade RecordTrade(string stockSymbol, DateTime timeStamp, int quantityOfShares, TradeIndicator tradeIndicator, decimal price)
        {
            if (!_stockData.ContainsKey(stockSymbol)) throw new Exception($"Stock symbol not found: {stockSymbol}");

            _stockData[stockSymbol].LastPrice = price;
            var trade = new Trade(stockSymbol, timeStamp.ToUniversalTime(), quantityOfShares, tradeIndicator, price);

            //There is not ORM in here, so I am generating the id manually which is necessary for UI
            trade.Id = _trades.Any() ? _trades.Max(x => x.Id) + 1 : 1;

            _trades.Add(trade);

            return trade;
        }

        public void ClearOnMemoryData()
        {
            _stockData.Values.ToList().ForEach(x => x.LastPrice = 0);
            _trades.Clear();
        }
    }
}
