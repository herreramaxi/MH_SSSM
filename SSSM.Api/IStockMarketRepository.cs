using SSSM.Model;
using System;
using System.Collections.Generic;

namespace SSSM.Api
{
    public interface IStockMarketRepository
    {
        CommonStock Get(string stockSymbol);
        IList<CommonStock> GetAll();
        Trade RecordTrade(string stockSymbol, DateTime timeStamp, int quantityOfShares, TradeIndicator tradeIndicator, decimal price);
        IList<Trade> GetLatest15MinTrades(string stockSymbol, DateTime now);
        IList<Trade> GetAllTrades();
    }
}
