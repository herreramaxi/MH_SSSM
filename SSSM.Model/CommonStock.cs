namespace SSSM.Model
{
    public class CommonStock
    {
        public string StockSymbol { get; }
        public virtual StockType Type => StockType.Common;
        public decimal LastDividend { get; }
        public decimal ParValue { get; }
        public decimal FixedDividend { get; protected set; }
        public decimal LatestPrice { get; private set; }

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
            var dividend = this.DividendYield(price);

            return price > 0 && dividend> 0 ? price / dividend: 0;
        }

        public void SetLatestPrice(decimal price)
        {
            this.LatestPrice = price;
        }
    }
}
