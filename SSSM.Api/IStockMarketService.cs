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
        IList<Trade> GetTrades();         
        Trade RecordTrade(string stockSymbol, DateTime timeStamp, int quantityOfShares, TradeIndicator tradeIndicator, decimal price);
        decimal GetDividendYield(string stockSymbol, decimal price);
        decimal GetPERatioFor(string stockSymbol, decimal price);
        decimal GetVolumeWeightedStockPrice(string stockSymbol, DateTime? now = null);
        decimal GBCEAllShareIndex(DateTime? now = null);
        void ClearOnMemoryData();
    }
}
