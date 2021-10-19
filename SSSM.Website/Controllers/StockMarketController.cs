using Microsoft.AspNetCore.Mvc;
using SSSM.Api;
using System.Collections;

namespace SSSM.Website.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class StockMarketController : ControllerBase
    {
        private readonly IStockMarketService _stockMarketService;

        public StockMarketController(IStockMarketService stockMarketService)
        {
            _stockMarketService = stockMarketService;
        }
        public IEnumerable GetSampleData()
        {
            return _stockMarketService.GetStocks();
        }
    }
}
