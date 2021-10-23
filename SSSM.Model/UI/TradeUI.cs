using System;
using System.ComponentModel.DataAnnotations;

namespace SSSM.Model.UI
{
    public class TradeUI
    {
        [Required]
        public string StockSymbol { get; set; }
        public DateTime TimeStamp { get; set; } = DateTime.UtcNow;

        [Range(1, 1000000)]
        public int QuantityOfShares { get; set; }
        public TradeIndicator TradeIndicator { get; set; }

        [Range(1.0, 1000000, ErrorMessage = "The field {0} must be greater than {1}.")]
        public decimal Price { get; set; }

        public TradeUI()
        {
        }

        public override string ToString()
        {
            return $"Trade => stockSymbol: {StockSymbol}, timestamp: {TimeStamp}, tradeIndicator: {TradeIndicator}, quantityOfShares: {QuantityOfShares}, price: {Price}";
        }
    }
}
