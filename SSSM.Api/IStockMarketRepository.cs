using SSSM.Model;
using System;
using System.Collections.Generic;

namespace SSSM.Api
{
    public interface IStockMarketRepository
    {
        CommonStock Get(string stockSymbol);
        IList<CommonStock> GetAllStocks();
        Trade RecordTrade(string stockSymbol, DateTime timeStamp, int quantityOfShares, TradeIndicator tradeIndicator, decimal price);
        IList<Trade> GetLast15MinTrades(string stockSymbol, DateTime now);
        IList<Trade> GetAllTrades();
        void ClearOnMemoryData();
    }
}
