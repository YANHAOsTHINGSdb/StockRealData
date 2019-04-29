using LooWooTech.AssetsTrade.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LooWooTech.AssetsTrade.StockService
{
    internal class CloseAccountService : ServiceBase
    {
        private bool _hasCloseAccount;

        protected override void Dowork()
        {
            if (_hasCloseAccount)
            {
                if (DateTime.Now.Minute != AppSettings.CloseAccountTime.Minute)
                {
                    _hasCloseAccount = false;
                }
                return;
            }
            if (DateTime.Now.Hour == AppSettings.CloseAccountTime.Hour && DateTime.Now.Minute == AppSettings.CloseAccountTime.Minute)
            {
                LogWriter.Info("[" + Account.MainCodeName + "]\t开始结算所有委托");
                //结算所有委托
                Core.AuthorizeManager.CloseAllAuthorize();
                LogWriter.Success("结算完毕");
                _hasCloseAccount = true;
            }
        }

        protected override int GetInterval()
        {
            return 30;
        }
    }
}
