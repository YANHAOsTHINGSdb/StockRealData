using LooWooTech.AssetsTrade.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LooWooTech.AssetsTrade.StockService
{
    public class ServiceHost
    {
        private List<ServiceBase> _services = new List<ServiceBase>();
        private MainAccount _account;
        public ServiceHost(MainAccount account)
        {
            _account = account;
            _services.Add(new AuthorizeService());
            _services.Add(new CloseAccountService());
            _services.Add(new RefreshDataService());
            _services.Add(new StockSyncService());
        }

        public void Start()
        {
            UpdateMainAccount(_account);
            foreach(var service in _services)
            {
                service.Start();
            }
        }

        public void UpdateMainAccount(MainAccount account)
        {
            _account = account;
            foreach (var service in _services)
            {
                service.Account = account;
            }
        }

        public void Stop()
        {
            foreach (var service in _services)
            {
                service.Stop();
            }
        }
    }
}
