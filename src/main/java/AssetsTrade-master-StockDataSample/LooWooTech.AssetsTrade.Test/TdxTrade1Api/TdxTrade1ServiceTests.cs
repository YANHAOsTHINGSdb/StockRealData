using Microsoft.VisualStudio.TestTools.UnitTesting;
using LooWooTech.AssetsTrade.TradeApi;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using LooWooTech.AssetsTrade.Managers;

namespace LooWooTech.AssetsTrade.TradeApi.Tests
{
    [TestClass()]
    public class TdxTrade1ServiceTests
    {
        private ManagerCore Core = ManagerCore.Instance;
        private static readonly TdxTrade1Service Service = new TdxTrade1Service();
        public TdxTrade1ServiceTests()
        {
            Service.Account = Core.AccountManager.GetMainAccount("666600244543");
            Service.Host = Core.ApiHostManager.GetFastHost(typeof(TdxTrade1Service));
            Service.Login();
        }

        [TestMethod()]
        public void QueryAuthroizesTest()
        {
            var result = Service.QueryAuthroizes();
            Console.WriteLine(result.Data);
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