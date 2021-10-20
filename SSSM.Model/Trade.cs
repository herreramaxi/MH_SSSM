using System;

namespace SSSM.Model
{
    public class Trade
    {
        public string StockSymbol { get; }
        public DateTime TimeStamp { get;  } 
        public int QuantityOfShares { get; }
        public TradeIndicator TradeIndicator { get; }
        public decimal Price { get; }

        public Trade(string stockSymbol, DateTime timeStamp, int quantityOfShares, TradeIndicator tradeIndicator, decimal price)
        {
            StockSymbol = stockSymbol;
            TimeStamp = timeStamp;
            QuantityOfShares = quantityOfShares;
            TradeIndicator = tradeIndicator;
            Price = price;
        }
    }
}
