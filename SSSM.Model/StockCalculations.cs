namespace SSSM.Model
{
    public class StockCalculations
    {
        public string StockSymbol { get; set; }
        public decimal DividendYield { get; set; }
        public decimal PERatio { get; set; }
        public decimal VolumeWeightedStockPrice { get; set; }
        public decimal LastPrice { get; set; }
        public decimal GBCEAllShareIndex { get; set; }
        public decimal LastDividend { get; set; }
        public decimal FixedDividend { get; set; }
        public decimal ParValue { get; set; }
        public string StockTypeDesc { get; set; }

        public StockCalculations(CommonStock stock)
        {                  
            this.FixedDividend = stock.FixedDividend;
            this.StockTypeDesc = stock.StockTypeDesc;
            this.LastDividend = stock.LastDividend;     
            this.StockSymbol = stock.StockSymbol; 
            this.LastPrice = stock.LastPrice;
            this.ParValue = stock.ParValue;  
            this.DividendYield = stock.DividendYield(stock.LastPrice);
            this.PERatio = stock.PERatio(stock.LastPrice);
        }     
    }
}