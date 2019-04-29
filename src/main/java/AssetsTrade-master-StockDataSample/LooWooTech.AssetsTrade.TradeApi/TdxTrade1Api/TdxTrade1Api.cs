using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;

namespace LooWooTech.AssetsTrade.TradeApi
{
    public class TdxTrade1Api
    {
        [DllImport("trade.dll", CharSet = CharSet.Ansi)]
        public static extern void OpenTdx();

        [DllImport("trade.dll", CharSet = CharSet.Ansi)]
        public static extern void CloseTdx();
        /// <summary>
        /// 交易账户登录
        /// </summary>
        /// <param name="IP">券商交易服务器IP</param>
        /// <param name="Port">券商交易服务器端口</param>
        /// <param name="Version">设置通达信客户端的版本号</param>
        /// <param name="YybID">营业部代码， 普通交易 “0”    信用交易“3” </param>
        /// <param name="AccountNo">完整的登录账号，券商一般使用资金帐户或客户号</param>
        /// <param name="TradeAccount">交易账号，一般与登录帐号相同. 请登录券商通达信软件，查询股东列表，股东列表内的资金帐号就是交易帐号, 具体查询方法请见网站“热点问答”栏目</param>
        /// <param name="JyPassword">交易密码</param>
        /// <param name="TxPassword">通讯密码</param>
        /// <param name="ErrInfo">此API执行返回后，如果出错，保存了错误信息说明。一般要分配256字节的空间。没出错时为空字符串。</param>
        /// <returns>客户端ID，失败时返回-1</returns>
        [DllImport("trade.dll", CharSet = CharSet.Ansi)]
        public static extern int Logon(string IP, short Port, string Version, string YybID, string AccountNo, string TradeAccount, string JyPassword, string TxPassword, StringBuilder ErrInfo);
        /// <summary>
        /// 交易账户注销
        /// </summary>
        /// <param name="ClientID">客户端ID</param>
        [DllImport("trade.dll", CharSet = CharSet.Ansi)]
        public static extern void Logoff(int ClientID);
        /// <summary>
        /// 查询各种交易数据
        /// </summary>
        /// <param name="ClientID">客户端ID</param>
        /// <param name="Category">表示查询信息的种类，0资金  1股份   2当日委托  3当日成交     4可撤单   5股东代码  6融资余额   7融券余额  8可融证券</param>
        /// <param name="Result">此API执行返回后，Result内保存了返回的查询数据, 形式为表格数据，行数据之间通过\n字符分割，列数据之间通过\t分隔。一般要分配1024*1024字节的空间。出错时为空字符串。</param>
        /// <param name="ErrInfo">同Logon函数的ErrInfo说明</param>
        [DllImport("trade.dll", CharSet = CharSet.Ansi)]
        public static extern void QueryData(int ClientID, int Category, StringBuilder Result, StringBuilder ErrInfo);
        /// <summary>
        /// 下委托交易证券
        /// </summary>
        /// <param name="ClientID">客户端ID</param>
        /// <param name="Category">表示委托的种类，0买入 1卖出  2融资买入  3融券卖出   4买券还券   5卖券还款  6现券还券</param>
        /// <param name="PriceType">表示报价方式 0上海限价委托 深圳限价委托 1(市价委托)深圳对方最优价格  2(市价委托)深圳本方最优价格  3(市价委托)深圳即时成交剩余撤销  4(市价委托)上海五档即成剩撤 深圳五档即成剩撤 5(市价委托)深圳全额成交或撤销 6(市价委托)上海五档即成转限价
        /// <param name="Gddm">股东代码, 交易上海股票填上海的股东代码；交易深圳的股票填入深圳的股东代码</param>
        /// <param name="Zqdm">证券代码</param>
        /// <param name="Price">委托价格</param>
        /// <param name="Quantity">委托数量</param>
        /// <param name="Result">同上,其中含有委托编号数据</param>
        /// <param name="ErrInfo">同上</param>
        [DllImport("trade.dll", CharSet = CharSet.Ansi)]
        public static extern void SendOrder(int ClientID, int Category, int PriceType, string Gddm, string Zqdm, float Price, int Quantity, StringBuilder Result, StringBuilder ErrInfo);
        /// <summary>
        /// 撤委托
        /// </summary>
        /// <param name="ClientID">客户端ID</param>
        /// <param name="ExchangeID">交易所ID， 上海1，深圳0(招商证券普通账户深圳是2)</param>
        /// <param name="hth">表示要撤的目标委托的编号</param>
        /// <param name="Result">同上</param>
        /// <param name="ErrInfo">同上</param>
        [DllImport("trade.dll", CharSet = CharSet.Ansi)]
        public static extern void CancelOrder(int ClientID, string hth, StringBuilder Result, StringBuilder ErrInfo);
        /// <summary>
        /// 获取证券的实时五档行情
        /// </summary>
        /// <param name="ClientID">客户端ID</param>
        /// <param name="Zqdm">证券代码</param>
        /// <param name="Result">同上</param>
        /// <param name="ErrInfo">同上</param>
        [DllImport("trade.dll", CharSet = CharSet.Ansi)]
        public static extern void GetQuote(int ClientID, string Zqdm, StringBuilder Result, StringBuilder ErrInfo);

        /// <summary>
        /// 属于普通批量版功能,查询各种历史数据
        /// </summary>
        /// <param name="ClientID">客户端ID</param>
        /// <param name="Category">表示查询信息的种类，0历史委托  1历史成交   2交割单</param>
        /// <param name="StartDate">表示开始日期，格式为yyyyMMdd,比如2014年3月1日为  20140301
        /// <param name="EndDate">表示结束日期，格式为yyyyMMdd,比如2014年3月1日为  20140301
        /// <param name="Result">同上</param>
        /// <param name="ErrInfo">同上</param>
        [DllImport("trade.dll", CharSet = CharSet.Ansi)]
        public static extern void QueryHistoryData(int ClientID, int Category, string StartDate, string EndDate, StringBuilder Result, StringBuilder ErrInfo);
        /// <summary>
        /// 属于普通批量版功能,批量查询各种交易数据,用数组传入每个委托的参数，数组第i个元素表示第i个查询的相应参数
        /// </summary>
        /// <param name="ClientID">客户端ID</param>
        /// <param name="Category">信息的种类的数组, 第i个元素表示第i个查询的信息种类，0资金  1股份   2当日委托  3当日成交     4可撤单   5股东代码  6融资余额   7融券余额  8可融证券</param>
        /// <param name="Count">查询的个数，即数组的长度</param>
        /// <param name="Result">返回数据的数组, 第i个元素表示第i个委托的返回信息. 此API执行返回后，Result[i]含义同上。</param>
        /// <param name="ErrInfo">错误信息的数组，第i个元素表示第i个委托的错误信息. 此API执行返回后，ErrInfo[i]含义同上。</param>
        [DllImport("trade.dll", CharSet = CharSet.Ansi)]
        public static extern void QueryDatas(int ClientID, int[] Category, int Count, IntPtr[] Result, IntPtr[] ErrInfo);

        [DllImport("trade.dll", CharSet = CharSet.Ansi)]
        public static extern void SendOrders(int ClientID, int[] Category, int[] PriceType, string[] Gddm, string[] Zqdm, float[] Price, int[] Quantity, int Count, IntPtr[] Result, IntPtr[] ErrInfo);

        [DllImport("trade.dll", CharSet = CharSet.Ansi)]
        public static extern void CancelOrders(int ClientID, string[] ExchangeID, string[] hth, int Count, IntPtr[] Result, IntPtr[] ErrInfo);

        [DllImport("trade.dll", CharSet = CharSet.Ansi)]
        public static extern void GetQuotes(int ClientID, string[] Zqdm, int Count, IntPtr[] Result, IntPtr[] ErrInfo);

        [DllImport("trade.dll", CharSet = CharSet.Ansi)]
        public static extern void Repay(int ClientID, string Amount, StringBuilder Result, StringBuilder ErrInfo);

    }
}
