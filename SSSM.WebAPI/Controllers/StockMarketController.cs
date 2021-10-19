using Microsoft.AspNetCore.Mvc;
using SSSM.Api;
using System.Collections;

namespace SSSM.WebAPI.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class StockMarketController: ControllerBase
    {
        private readonly IStockMarketService _stockMarketService;

        public StockMarketController(IStockMarketService stockMarketService)
        {
            _stockMarketService = stockMarketService;
        }
        public IEnumerable GetSampleData() {
            return _stockMarketService.GetStocks();
        }
    }
}
