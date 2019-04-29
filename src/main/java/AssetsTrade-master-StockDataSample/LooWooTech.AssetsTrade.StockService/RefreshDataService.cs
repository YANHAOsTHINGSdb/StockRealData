using LooWooTech.AssetsTrade.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LooWooTech.AssetsTrade.StockService
{
    internal class RefreshDataService : ServiceBase
    {
        private bool _hasRefreshed;

        protected override void Dowork()
        {
            if (_hasRefreshed)
            {
                if (DateTime.Now.Minute != AppSettings.RefreshTime.Minute)
                {
                    _hasRefreshed = false;
                }
                return;
            }
            if (DateTime.Now.Hour == AppSettings.RefreshTime.Hour && DateTime.Now.Minute == AppSettings.RefreshTime.Minute)
            {
                LogWriter.Info("[" + Account.MainCodeName + "]\t开始结算用户资产");
                Core.AccountManager.UpdateChildAccountMoneyAndStocks(Account);
                LogWriter.Success("结算完毕");
                _hasRefreshed = true;
            }
        }

        protected override int GetInterval()
        {
            return AppSettings.IsWorkingTime ? 1200 : 30;
        }
    }
}
