namespace SSSM.Model
{
    public class PreferredStock : CommonStock
    {      
        public override StockType StockType => StockType.Preferred;
        public PreferredStock(string stockSymbol, decimal lastDividend, decimal fixedDividend, decimal parValue): base(stockSymbol, lastDividend,parValue)
        {
            this.FixedDividend = fixedDividend;
        }   
        
        public override decimal DividendYield(decimal price)
        {          
            return price> 0 ? this.FixedDividend * this.ParValue / price: 0; ;
        }
    }
}