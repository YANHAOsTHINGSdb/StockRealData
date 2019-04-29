using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LooWooTech.AssetsTrade.StockService
{
    public class LogWriter
    {
        public static void Default(string msg)
        {
            Console.WriteLine("[" + DateTime.Now + "]\t" + msg);
        }

        public static void Info(string msg)
        {
            Console.ForegroundColor = ConsoleColor.Blue;
            Console.WriteLine("[" + DateTime.Now + "]\t" + msg);
            Console.ForegroundColor = ConsoleColor.White;
        }

        public static void Warning(string msg)
        {
            Console.ForegroundColor = ConsoleColor.Yellow;
            Console.WriteLine("[" + DateTime.Now + "]\t" + msg);
            Console.ForegroundColor = ConsoleColor.White;
        }

        public static void Success(string msg)
        {
            Console.ForegroundColor = ConsoleColor.Green;
            Console.WriteLine("[" + DateTime.Now + "]\t" + msg);
            Console.ForegroundColor = ConsoleColor.White;
        }

        public static void Error(string msg)
        {
            Console.ForegroundColor = ConsoleColor.Red;
            Console.WriteLine("[" + DateTime.Now + "]\t" + msg);
            Console.ForegroundColor = ConsoleColor.White;
        }
    }
}
