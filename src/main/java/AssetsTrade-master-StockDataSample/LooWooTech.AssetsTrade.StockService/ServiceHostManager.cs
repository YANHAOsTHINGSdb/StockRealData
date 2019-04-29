using LooWooTech.AssetsTrade.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace LooWooTech.AssetsTrade.StockService
{
    public class ServiceHostManager
    {
        private Dictionary<MainAccount, ServiceHost> _services = new Dictionary<MainAccount, ServiceHost>();

        private Thread _update;
        public void Start()
        {
            if (_update != null) return;
            _update = new Thread(() =>
            {
                while (true)
                {
                    UpdateMainAccount();
                    Thread.Sleep(1000 * 60);
                }
            });
            _update.Start();
        }

        public void Stop()
        {
            _update.Abort();
            _update = null;
        }

        private List<MainAccount> GetAccounts()
        {
            return Managers.ManagerCore.Instance.AccountManager.GetMainAccounts();
        }


        private void UpdateMainAccount()
        {
            var list = GetAccounts();
            foreach (var account in list)
            {
                var data = _services.FirstOrDefault(kv => kv.Key.MainID == account.MainID);
                if (data.Key != null)
                {
                    var key = data.Key;
                    var service = data.Value;
                    if (key.UpdateTime != account.UpdateTime)
                    {
                        data.Value.UpdateMainAccount(account);
                        _services.Remove(key);
                        _services.Add(account, service);
                    }
                }
                else
                {
                    var service = new ServiceHost(account);
                    service.Start();

                    _services.Add(account, service);
                }

            }
        }
    }
}
