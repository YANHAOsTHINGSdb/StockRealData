using LooWooTech.AssetsTrade.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.ServiceProcess;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace LooWooTech.AssetsTrade.StockService
{
    static class Program
    {
        private static readonly ServiceHostManager ServiceHostManager = new ServiceHostManager();

        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        static void Main()
        {
            Environment.CurrentDirectory = AppDomain.CurrentDomain.BaseDirectory;

            Console.ForegroundColor = ConsoleColor.White;
            AppDomain.CurrentDomain.UnhandledException += CurrentDomain_UnhandledException;
            ServiceHostManager.Start();
            LogWriter.Success("服务已启动");
            while (true)
            {
                var cmd = Console.ReadLine();
                switch (cmd)
                {
                    case "stop":
                        ServiceHostManager.Stop();
                        Console.WriteLine("请关闭程序重新打开");
                        break;
                    case "start":
                        ServiceHostManager.Start();
                        break;
                    case "help":
                        Console.WriteLine("start|stop|exit");
                        break;
                    case "exit":
                        break;
                }
            }
        }

        private static void CurrentDomain_UnhandledException(object sender, UnhandledExceptionEventArgs e)
        {
            var ex = (Exception)e.ExceptionObject;
            LogWriter.Error(ex.Message);
            LogHelper.WriteLog(ex);
        }
    }
}
