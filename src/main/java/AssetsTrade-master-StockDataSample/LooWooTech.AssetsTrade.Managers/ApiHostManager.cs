using LooWooTech.AssetsTrade.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace LooWooTech.AssetsTrade.Managers
{
    public class ApiHostManager : ManagerBase
    {
        private static List<ApiHost> _data;
        private static object _lockObj = new object();

        public ApiHostManager()
        {
            _data = GetList();
            UpdateData();
        }

        public List<ApiHost> GetList()
        {
            using (var db = GetDbContext())
            {
                return db.ApiHosts.ToList();
            }
        }

        private static DateTime _lastUpdateTime = DateTime.MinValue;
        public void UpdateData()
        {
            if (_data.Count > 0 && (DateTime.Now - _lastUpdateTime).TotalMinutes < 1) return;

            lock (_lockObj)
            {
                if ((DateTime.Now - _lastUpdateTime).TotalMinutes < 1) return;
                new Thread(() =>
                {
                    _data = GetList();
                    foreach (var ip in _data)
                    {
                        var ping = new System.Net.NetworkInformation.Ping();
                        var reply = ping.Send(ip.IPAddress, 1000 * 10);
                        if (reply.Status == System.Net.NetworkInformation.IPStatus.Success)
                        {
                            ip.Ping = reply.RoundtripTime;
                        }
                    }
                    _data = _data.OrderBy(e => e.Ping).ToList();

                    _lastUpdateTime = DateTime.Now;
                }).Start();
            }
        }

        public ApiHost GetFastHost(Type serviceType)
        {
            UpdateData();
            var serviceName = serviceType.Name;
            return _data.Where(e => e.Service == serviceName).OrderBy(e => e.Ping).FirstOrDefault();
        }

    }
}
