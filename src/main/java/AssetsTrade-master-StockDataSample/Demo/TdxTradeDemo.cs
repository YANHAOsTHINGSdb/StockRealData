using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;

namespace Demo
{
    public class TdxTradeDemo
    {
        public static void Test()
        {
            TdxTradeApi.SetServer("202.102.53.99", 7708);
            TdxTradeApi.SetAccount("666600244543", "603772", "33183318");
            TdxTradeApi.Login();

            var result = new StringBuilder(1024 * 1024);
            var error = new StringBuilder(1024 * 1024);

            string stockCode = "603077";
            int number = 100;
            float price = 5.21f;
            TdxTradeApi.ToBuy(stockCode, number, price, result, error);
            Console.WriteLine("买入成功", result);
            Thread.Sleep(1000 * 60);
            var index = result.ToString();
            TdxTradeApi.CancelOrder(stockCode, index, result, error);
            Console.WriteLine("撤单成功", result);
        }
    }
}
