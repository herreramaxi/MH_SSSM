using System;

namespace SSSM.Model
{
    public class PreferredStock : CommonStock
    {      
        public override StockType Type => StockType.Preferred;
        public PreferredStock(string stockSymbol, decimal lastDividend, decimal fixedDividend, decimal parValue): base(stockSymbol, lastDividend,parValue)
        {
            this.FixedDividend = fixedDividend;
        }   
        
        public override decimal DividendYield(decimal price)
        {
            if (price == 0) throw new ArgumentException("Price cannot be zero");
          
            return this.FixedDividend * this.ParValue / price;
        }
    }
}
