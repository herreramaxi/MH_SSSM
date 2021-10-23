using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
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
        private readonly ILogger<StockMarketController> _logger;

        public StockMarketController(IStockMarketService stockMarketService, ILogger<StockMarketController> logger)
        {
            _stockMarketService = stockMarketService;
            _logger = logger;
        }

        [HttpGet]
        public IEnumerable<CommonStock> GetSampleData()
        {
            _logger.LogInformation("GetSampleData");
            return _stockMarketService.GetStocks();
        }

        [HttpGet]
        [Route("GetStockCalculations")]
        public StockCalculations GetStockCalculations(string stockSymbol)
        {
            _logger.LogInformation($"GetStockCalculations: {stockSymbol}");
            return _stockMarketService.GetStockCalculations(stockSymbol);
        }

        [HttpGet]
        [Route("GetTrades")]
        public IEnumerable<Trade> GetTrades()
        {
            _logger.LogInformation("GetTrades");
            return _stockMarketService.GetTrades();
        }

        [HttpPost]
        [Route("Trade")]
        public Trade Trade([FromBody] TradeUI trade)
        {
            //There should be mappings between business entities and UI, usually using Automaper
            _logger.LogInformation(trade?.ToString());
            return _stockMarketService.RecordTrade(trade.StockSymbol, trade.TimeStamp, trade.QuantityOfShares, trade.TradeIndicator, trade.Price);
        }

        [HttpPost]
        [Route("ClearOnMemoryData")]
        public ActionResult ClearOnMemoryData()
        {
            _stockMarketService.ClearOnMemoryData();

            return Ok("On memory data was succesfully cleaned");
        }
    }
}
