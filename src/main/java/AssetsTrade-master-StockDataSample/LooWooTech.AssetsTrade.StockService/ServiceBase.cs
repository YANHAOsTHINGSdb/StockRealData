using LooWooTech.AssetsTrade.Common;
using LooWooTech.AssetsTrade.Managers;
using LooWooTech.AssetsTrade.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace LooWooTech.AssetsTrade.StockService
{
    abstract class ServiceBase
    {
        public MainAccount Account { get; set; }

        protected static readonly ManagerCore Core = ManagerCore.Instance;

        protected CancellationTokenSource cts = new CancellationTokenSource();

        protected abstract int GetInterval();

        protected abstract void Dowork();

        public virtual void Start()
        {
            Task.Factory.StartNew(() =>
            {
                while (true)
                {
                    if (cts.IsCancellationRequested)
                    {
                        break;
                    }
                    try
                    {
                        Dowork();
                    }
                    catch (Exception ex)
                    {
                        LogWriter.Error(ex.Message);
                        LogHelper.WriteLog(ex);
                    }
                    //如果不是工作时间，线程间隔为1分钟，工作时间为1秒钟
                    Thread.Sleep(GetInterval() * 1000);
                }
            }, cts.Token);
        }

        public virtual void Stop()
        {
            cts.Cancel();
        }
    }
}
