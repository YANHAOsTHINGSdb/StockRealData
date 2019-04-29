using LooWooTech.AssetsTrade.Models;
using LooWooTech.AssetsTrade.TradeApi;
using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LooWooTech.AssetsTrade.Managers
{
    public class ServiceManager : ManagerBase
    {
        private static readonly TradeApi.TradeServiceInvoker TradeServiceInvoker = new TradeApi.TradeServiceInvoker();
        private static readonly TradeApi.MarketServiceInvoker MarketServiceInvoker = new TradeApi.MarketServiceInvoker();

        public ApiResult Buy(MainAccount account, string stockCode, int number, double price)
        {
            return TradeServiceInvoker.InvokeMethod(account, "Buy", new object[] { stockCode, number, price });
        }

        public ApiResult Sell(MainAccount account, string stockCode, int number, double price)
        {
            return TradeServiceInvoker.InvokeMethod(account, "Sell", new object[] { stockCode, number, price });
        }

        public ApiResult Cancel(MainAccount account, string stockCode, string authorizeIndex)
        {
            return TradeServiceInvoker.InvokeMethod(account, "Cancel", new object[] { stockCode, authorizeIndex });
        }

        public ApiResult QueryStocks(MainAccount account)
        {
            return TradeServiceInvoker.InvokeMethod(account, "QueryStocks", null);
        }

        public ApiResult QueryAuthroizes(MainAccount account)
        {
            return TradeServiceInvoker.InvokeMethod(account, "QueryAuthroizes", null);
        }

        public ApiResult QueryTrades(MainAccount account)
        {
            return TradeServiceInvoker.InvokeMethod(account, "QueryTrades", null);
        }

        public ApiResult QueryMoney(MainAccount account)
        {
            return TradeServiceInvoker.InvokeMethod(account, "QueryMoney", null);
        }

        public ApiResult QueryHistoryTrade(MainAccount account, DateTime startTime, DateTime endTime)
        {
            return TradeServiceInvoker.InvokeMethod(account, "QueryHistoryTrade", new object[] { startTime, endTime });
        }

        public ApiResult QueryHistoryMoney(MainAccount account, DateTime startTime, DateTime endTime)
        {
            return TradeServiceInvoker.InvokeMethod(account, "QueryHistoryMoney", new object[] { startTime, endTime });
        }

        public ApiResult QueryMarket(string[] stockCodes)
        {
            return MarketServiceInvoker.InvokeMethod("GetMarketInfo", new object[] { stockCodes });
        }
    }
}
