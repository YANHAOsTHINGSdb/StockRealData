using LooWooTech.AssetsTrade.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LooWooTech.AssetsTrade.TradeApi
{
    public interface IMarketService
    {
        ApiResult Connect(ApiHost host);

        ApiResult GetMarketInfo(string[] stockCodes);

        void Disconnet();
    }
}
