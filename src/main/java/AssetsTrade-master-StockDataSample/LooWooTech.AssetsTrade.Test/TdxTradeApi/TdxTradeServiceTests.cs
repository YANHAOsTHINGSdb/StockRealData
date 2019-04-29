using Microsoft.VisualStudio.TestTools.UnitTesting;
using LooWooTech.AssetsTrade.TradeApi;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using LooWooTech.AssetsTrade.Managers;
using System.Diagnostics;

namespace LooWooTech.AssetsTrade.TradeApi.Tests
{
    [TestClass()]
    public class TdxTradeServiceTests
    {
        private ManagerCore Core = ManagerCore.Instance;
        private static readonly TdxTradeService Service = new TdxTradeService();
        public TdxTradeServiceTests()
        {
            Service.Account = Core.AccountManager.GetMainAccount("666600244543");
            Service.Host = Core.ApiHostManager.GetFastHost(typeof(TdxTradeService));
            Service.Login();
        }

        [TestMethod()]
        public void LoginTest()
        {
            var result = Service.Login();
            Assert.AreEqual(true, result.Result);
        }

        [TestMethod()]
        public void QueryAuthroizesTest()
        {
            var result = Service.QueryAuthroizes();
            Console.WriteLine(result.Result);
            Assert.AreEqual(true, result.Result);
        }

        [TestMethod()]
        public void QueryStocksTest()
        {
            var result = Service.QueryStocks();
            Trace.WriteLine(result.Data);
            Assert.AreEqual(true, result.Result);
        }

        [TestMethod()]
        public void QueryMoneyTest()
        {
            var result = Service.QueryMoney();
            Trace.WriteLine(result.Data);
            Assert.AreEqual(true, result.Result);
        }

        [TestMethod()]
        public void QueryTradesTest()
        {
            var result = Service.QueryTrades();
            Trace.WriteLine(result.Data);
            Assert.AreEqual(true, result.Result);
        }

        [TestMethod()]
        public void QueryHistoryTradeTest()
        {
            var result = Service.QueryHistoryTrade(DateTime.Today.AddDays(-1), DateTime.Today.AddDays(1));
            Trace.WriteLine(result.Data);
            Assert.AreEqual(true, result.Result);
        }

        [TestMethod()]
        public void QueryHistoryMoneyTest()
        {
            var result = Service.QueryHistoryMoney(DateTime.Today.AddDays(-1), DateTime.Today.AddDays(1));
            Trace.WriteLine(result.Data);
            Assert.AreEqual(true, result.Result);
        }

        [TestMethod()]
        public void BuyTest()
        {
            var result = Service.Buy("600307", 100, 5.21f);
            Assert.AreEqual(true, result.Result);
        }
    }
}