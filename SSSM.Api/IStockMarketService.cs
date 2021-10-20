using SSSM.Model;
using System;
using System.Collections.Generic;

namespace SSSM.Api
{
    public interface IStockMarketService
    {
        IList<CommonStock> GetStocks();
        CommonStock GetStock(string stockSymbol);        
        StockCalculations GetStockCalculations(string stockSymbol);
        Trade RecordTrade(string stockSymbol, DateTime timeStamp, int quantityOfShares, TradeIndicator tradeIndicator, decimal price);
    }
}
