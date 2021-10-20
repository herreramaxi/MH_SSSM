using System;

namespace SSSM.Model.UI
{
    public class TradeUI
    {
        public string StockSymbol { get; set; }
        public DateTime TimeStamp { get;  set; } = DateTime.Now;
        public int QuantityOfShares { get; set; }
        public TradeIndicator TradeIndicator { get; set; }
        public decimal Price { get; set; }

        public TradeUI()
        {
        }
    }
}
