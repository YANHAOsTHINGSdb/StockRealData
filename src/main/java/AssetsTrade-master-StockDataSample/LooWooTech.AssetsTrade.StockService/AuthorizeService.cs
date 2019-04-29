using LooWooTech.AssetsTrade.Managers;
using LooWooTech.AssetsTrade.Models;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace LooWooTech.AssetsTrade.StockService
{
    internal class AuthorizeService : ServiceBase
    {
        protected override int GetInterval()
        {
            return AppSettings.IsWorkingTime ? AppSettings.QueryAuthroizeIntervalSecond : 120;
        }

        protected override void Dowork()
        {
            var list = Core.AuthorizeManager.GetTodayAuthorize(Account);
            LogWriter.Default("[" + Account.MainCodeName + "]\t查询委托" + list.Count + "条");
            foreach (var item in list)
            {
                Core.TradeManager.UpdateAuthorize(item);
            }
        }
    }
}
