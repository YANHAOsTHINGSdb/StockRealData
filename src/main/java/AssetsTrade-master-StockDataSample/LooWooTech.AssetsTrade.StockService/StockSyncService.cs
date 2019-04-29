using LooWooTech.AssetsTrade.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LooWooTech.AssetsTrade.StockService
{
    internal class StockSyncService : ServiceBase
    {
        protected override void Dowork()
        {
            if (AppSettings.IsWorkingTime)
            {
                LogWriter.Default("[" + Account.MainCodeName + "]\t同步股票市价");
                Core.StockManager.SyncStocks(Account);
            }
        }

        protected override int GetInterval()
        {
            var result = AppSettings.SyncInterval == 0 ? 5 : AppSettings.SyncInterval;

            return AppSettings.IsWorkingTime ? result : 60;
        }
    }
}
