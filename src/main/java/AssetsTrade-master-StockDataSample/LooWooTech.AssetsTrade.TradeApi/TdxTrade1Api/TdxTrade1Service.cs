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
    public class TdxTrade1Service : ITradeService
    {
        private readonly int ResultCapacity = 0x1000 * 0x100;
        private readonly int ErrorCapacity = 0x100;

        private readonly static ConcurrentDictionary<string, int> LoginAccounts = new ConcurrentDictionary<string, int>();

        public MainAccount Account { get; set; }

        public ApiHost Host { get; set; }

        private int GetClientId()
        {
            int clientId = 0;
            if (LoginAccounts.TryGetValue(Account.MainID, out clientId))
            {
                return clientId;
            }
            throw new Exception("账户未登录");
        }

        private StringBuilder RemoveFirstLine(StringBuilder sb)
        {
            if (sb.Length == 0) return sb;
            var index = -1;
            for (var i = 0; i < sb.Length; i++)
            {
                if (sb[i] == '\n')
                {
                    index = i;
                    break;
                }
            }
            sb.Remove(0, index + 1);
            return sb;
        }

        public ApiResult Login()
        {
            var success = new ApiResult { Result = true };

            if (LoginAccounts.ContainsKey(Account.MainID))
            {
                return success;
            }

            TdxTrade1Api.OpenTdx();
            var error = new StringBuilder(ErrorCapacity);

            var clientId = TdxTrade1Api.Logon(Host.IPAddress, (short)Host.Port,
                Account.TradeApiVersion, Account.YingYeBuDM,
                Account.MainID, Account.MainID, Account.TradePassword, Account.MessagePassword, error);
            if (clientId > -1)
            {
                LoginAccounts.GetOrAdd(Account.MainID, clientId);
            }
            return new ApiResult
            {
                Result = clientId == -1,
                Error = error.ToString()
            };
        }

        public void Logout()
        {
            int clientId = 0;
            LoginAccounts.TryRemove(Account.MainID, out clientId);
        }

        private string GetGDDM(string stockCode)
        {
            var stockCodePrifex = stockCode.Substring(0, 2);
            if ("50,51,60".Contains(stockCodePrifex))
            {
                return Account.SH_GDDM;
            }
            if ("30,00,15".Contains(stockCodePrifex))
            {
                return Account.SZ_GDDM;
            }

            return null;
        }

        public ApiResult Buy(string stockCode, int number, double price)
        {
            var data = new StringBuilder(ResultCapacity);
            var error = new StringBuilder(ErrorCapacity);
            TdxTrade1Api.SendOrder(GetClientId(), 0, 0, GetGDDM(stockCode), stockCode, (float)price, number, data, error);
            return new ApiResult
            {
                Result = error.Length == 0,
                Data = data.ToString(),
                Error = error.ToString()
            };
        }

        public ApiResult Sell(string stockCode, int number, double price)
        {
            var data = new StringBuilder(ResultCapacity);
            var error = new StringBuilder(ErrorCapacity);
            TdxTrade1Api.SendOrder(GetClientId(), 1, 0, GetGDDM(stockCode), stockCode, (float)price, number, data, error);

            return new ApiResult
            {
                Result = error.Length == 0,
                Data = data.ToString(),
                Error = error.ToString()
            };
        }

        public ApiResult Cancel(string stockCode, string authorizeIndex)
        {
            var data = new StringBuilder(ResultCapacity);
            var error = new StringBuilder(ErrorCapacity);
            TdxTrade1Api.CancelOrder(GetClientId(), authorizeIndex, data, error);
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
            TdxTrade1Api.QueryData(GetClientId(), 2, data, error);
            RemoveFirstLine(data);
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
            TdxTrade1Api.QueryHistoryData(GetClientId(), 3, startTime.ToString("yyyyMMdd"), endTime.ToString("yyyyMMdd"), data, error);
            RemoveFirstLine(data);
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
            TdxTrade1Api.QueryHistoryData(GetClientId(), 1, startTime.ToString("yyyyMMdd"), endTime.ToString("yyyyMMdd"), data, error);
            RemoveFirstLine(data);
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
            TdxTrade1Api.QueryData(GetClientId(), 0, data, error);
            RemoveFirstLine(data);
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
            TdxTrade1Api.QueryData(GetClientId(), 1, data, error);
            RemoveFirstLine(data);
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
            TdxTrade1Api.QueryData(GetClientId(), 3, data, error);
            RemoveFirstLine(data);
            return new ApiResult
            {
                Result = error.Length == 0,
                Data = data.ToString(),
                Error = error.ToString()
            };
        }
    }
}
