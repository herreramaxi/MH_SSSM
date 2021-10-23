using System;

namespace SSSM.Model
{
    public class CommonStock
    {
        public string StockSymbol { get; }
        public virtual StockType StockType => StockType.Common;
        public string StockTypeDesc => Enum.GetName(typeof(StockType), StockType);
        public decimal LastDividend { get; }
        public decimal ParValue { get; }
        public decimal FixedDividend { get; protected set; }
        public decimal LastPrice { get; set; }

        public CommonStock(string stockSymbol, decimal lastDividend, decimal parValue)
        {
            this.StockSymbol = stockSymbol;
            this.LastDividend = lastDividend;
            this.ParValue = parValue;
        }
       
        public virtual decimal DividendYield(decimal price)
        {
            return price > 0 ? this.LastDividend / price : 0;
        }
        
        public decimal PERatio(decimal price)
        {
            return price > 0 && this.LastDividend > 0 ? price / this.LastDividend : 0;
        }
    }
}