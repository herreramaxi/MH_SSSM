using System;

namespace SSSM.Model
{
    public class CommonStock
    {
        public string StockSymbol { get; set; }
        public virtual StockType Type => StockType.Common;
        public decimal LastDividend { get; set; }
        public decimal ParValue { get; set; }
        public decimal FixedDividend { get; set; }

        public CommonStock(string stockSymbol, decimal lastDividend, decimal parValue)
        {
            this.StockSymbol = stockSymbol;
            this.LastDividend = lastDividend;
            this.ParValue = parValue;
        }

        public virtual decimal DividendYield(decimal price)
        {
            if (price == 0) throw new ArgumentException("Price cannot be zero");
            return this.LastDividend / price;
        }

        public decimal PERatio(decimal price)
        {
            return price / this.DividendYield(price);
        }
    }
}
