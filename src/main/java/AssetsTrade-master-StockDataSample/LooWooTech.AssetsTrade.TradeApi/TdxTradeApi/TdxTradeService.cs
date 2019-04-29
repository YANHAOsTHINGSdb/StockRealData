using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using LooWooTech.AssetsTrade.Models;
using System.Runtime.InteropServices;
using System.Collections.Concurrent;

namespace LooWooTech.AssetsTrade.TradeApi
{
    public class TdxTradeService : ITradeService
    {
        private readonly int ResultCapacity = 0x1000 * 0x100;
        private readonly int ErrorCapacity = 0x100;
        private readonly static HashSet<string> LoginAccounts = new HashSet<string>();

        public MainAccount Account { get; set; }

        public ApiHost Host { get; set; }

        public ApiResult Login()
        {
            var success = new ApiResult { Result = true };

            if (LoginAccounts.Any(str => Account.MainID == str))
            {
                return success;
            }

            TdxTradeApi.SetServer(Host.IPAddress, Host.Port);
            TdxTradeApi.SetAccount(Account.MainID, Account.TradePassword, Account.MessagePassword);

            if (TdxTradeApi.Login())
            {
                LoginAccounts.Add(Account.MainID);
                return success;
            }
            else
            {
                var serverInfo = TdxTradeApi.GetReturnInfo();
                return new ApiResult
                {
                    Result = false,
                    Error = serverInfo
                };
            }
        }

        public void Logout()
        {
            LoginAccounts.Remove(Account.MainID);
        }

        public ApiResult Buy(string stockCode, int number, double price)
        {
            var data = new StringBuilder(ResultCapacity);
            var error = new StringBuilder(ErrorCapacity);
            var result = TdxTradeApi.ToBuy(stockCode, number, (float)price, data, error) == 1;

            return new ApiResult
            {
                Result = result,
                Data = data.ToString(),
                Error = error.ToString()
            };
        }

        public ApiResult Sell(string stockCode, int number, double price)
        {
            var data = new StringBuilder(ResultCapacity);
            var error = new StringBuilder(ErrorCapacity);
            var result = TdxTradeApi.ToSell(stockCode, number, (float)price, data, error) == 1;

            return new ApiResult
            {
                Result = result,
                Data = data.ToString(),
                Error = error.ToString()
            };
        }

        public ApiResult Cancel(string stockCode, string authorizeIndex)
        {
            var data = new StringBuilder(ResultCapacity);
            var error = new StringBuilder(ErrorCapacity);
            TdxTradeApi.CancelOrder(stockCode, authorizeIndex, data, error);
            return new ApiResult
            {
                Result = error.Length == 0,
                Data = data.ToString(),
                Error = error.ToString()
            };
        }

        public ApiResult QueryAuthroizes()
        {
            var data = new StringBuilder(ResultCapacity);
            var error = new StringBuilder(ErrorCapacity);
            TdxTradeApi.QueryData((int)QueryFlag.Authorize, data, error);
            return new ApiResult
            {
                Result = error.Length == 0,
                Data = data.ToString(),
                Error = error.ToString()
            };
        }

        public ApiResult QueryHistoryMoney(DateTime startTime, DateTime endTime)
        {
            var data = new StringBuilder(ResultCapacity);
            var error = new StringBuilder(ErrorCapacity);
            TdxTradeApi.QueryHistoryMoney(startTime.ToString("yyyyMMdd"), endTime.ToString("yyyyMMdd"), data, error);
            return new ApiResult
            {
                Result = error.Length == 0,
                Data = data.ToString(),
                Error = error.ToString()
            };
        }

        public ApiResult QueryHistoryTrade(DateTime startTime, DateTime endTime)
        {
            var data = new StringBuilder(ResultCapacity);
            var error = new StringBuilder(ErrorCapacity);
            TdxTradeApi.QueryHistoryData(startTime.ToString("yyyyMMdd"), endTime.ToString("yyyyMMdd"), data, error);
            return new ApiResult
            {
                Result = error.Length == 0,
                Data = data.ToString(),
                Error = error.ToString()
            };
        }

        public ApiResult QueryMoney()
        {
            var data = new StringBuilder(ResultCapacity);
            var error = new StringBuilder(ErrorCapacity);
            TdxTradeApi.QueryData((int)QueryFlag.Money, data, error);
            return new ApiResult
            {
                Result = error.Length == 0,
                Data = data.ToString(),
                Error = error.ToString()
            };
        }

        public ApiResult QueryStocks()
        {
            var data = new StringBuilder(ResultCapacity);
            var error = new StringBuilder(ErrorCapacity);
            TdxTradeApi.QueryData((int)QueryFlag.Stock, data, error);
            return new ApiResult
            {
                Result = error.Length == 0,
                Data = data.ToString(),
                Error = error.ToString()
            };
        }

        public ApiResult QueryTrades()
        {
            var data = new StringBuilder(ResultCapacity);
            var error = new StringBuilder(ErrorCapacity);
            TdxTradeApi.QueryData((int)QueryFlag.Trade, data, error);
            return new ApiResult
            {
                Result = error.Length == 0,
                Data = data.ToString(),
                Error = error.ToString()
            };
        }

        enum QueryFlag
        {
            Stock = 0,
            Authorize = 1,
            Trade = 2,
            Money = 3
        }
    }
}
