using Microsoft.AspNetCore.Mvc;
using SSSM.Api;
using SSSM.Model;
using SSSM.Model.UI;
using System.Collections.Generic;

namespace SSSM.WebAPI.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class StockMarketController : ControllerBase
    {
        private readonly IStockMarketService _stockMarketService;

        public StockMarketController(IStockMarketService stockMarketService)
        {
            _stockMarketService = stockMarketService;
        }

        [HttpGet]
        public IEnumerable<CommonStock> GetSampleData()
        {
            return _stockMarketService.GetStocks();
        }

        [HttpGet]
        [Route("GetStockCalculations")]
        public StockCalculations GetStockCalculations(string stockSymbol)
        {
            return _stockMarketService.GetStockCalculations(stockSymbol);
        }
                
        [HttpPost]
        [Route("Trade")]
        public Trade Trade([FromBody] TradeUI trade)
        {
           return  _stockMarketService.RecordTrade(trade.StockSymbol, trade.TimeStamp, trade.QuantityOfShares, trade.TradeIndicator, trade.Price);
        }
    }
}
