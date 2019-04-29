using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;

namespace LooWooTech.AssetsTrade.TradeApi
{
    internal class TdxMarketApi
    {
        [DllImport("TdxHqApi.dll")]
        public static extern void TdxL2Hq_Connect(string ip, int port, StringBuilder Result, StringBuilder ErrInfo);

        [DllImport("TdxHqApi.dll")]
        public static extern void TdxL2Hq_GetSecurityQuotes10(byte[] Market, string[] Zqdm, ref short Count, StringBuilder Result, StringBuilder ErrInfo);

        [DllImport("TdxHqApi.dll")]
        public static extern void TdxL2Hq_Disconnect();
    }
}
