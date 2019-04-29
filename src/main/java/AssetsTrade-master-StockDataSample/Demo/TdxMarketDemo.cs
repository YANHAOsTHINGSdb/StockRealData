using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Demo
{
    public class TdxMarketDemo
    {
        public static void Test()
        {
            var result = new StringBuilder(1024 * 1024);
            var error = new StringBuilder(1024 * 1024);
            TdxMarketApi.TdxL2Hq_Connect("61.152.107.173", 7707, result, error);
            var markets = new byte[] { 1 };
            var stockCodes = new[] { "603077" };
            TdxMarketApi.TdxL2Hq_GetSecurityQuotes10(markets, stockCodes, 1, result, error);
            Console.WriteLine(result.ToString());
            TdxMarketApi.TdxL2Hq_Disconnect();

        }
    }
}
