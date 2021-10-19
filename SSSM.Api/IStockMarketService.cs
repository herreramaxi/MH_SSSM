using SSSM.Model;
using System.Collections.Generic;

namespace SSSM.Api
{
    public interface IStockMarketService
    {
        IList<CommonStock> GetStocks();
    }
}
