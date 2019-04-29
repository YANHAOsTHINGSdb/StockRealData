using LooWooTech.AssetsTrade.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace LooWooTech.AssetsTrade.WebApi.Controllers
{
    public class TradeController : ControllerBase
    {
        /// <summary>
        /// 买入股
        /// </summary>
        public ActionResult Buy(string stockCode, int number, double price)
        {
            if (string.IsNullOrEmpty(stockCode))
            {
                throw new ArgumentException("请输入股票代码");
            }
            if (number % 100 > 0)
            {
                throw new ArgumentException("购买数量必须是100的整倍数");
            }
            if (price <= 0)
            {
                throw new ArgumentException("价格不正确");
            }
            Core.TradeManager.ToBuy(stockCode, number, price, CurrentAccount);
            return SuccessResult();
        }

        /// <summary>
        /// 卖出股票
        /// </summary>
        public ActionResult Sell(string stockCode, int number, double price)
        {
            if (string.IsNullOrEmpty(stockCode) || number <= 0 || price <= 0)
            {
                throw new ArgumentException("参数不正确");
            }
            Core.TradeManager.ToSell(stockCode, number, price, CurrentAccount);
            return SuccessResult();
        }

        /// <summary>
        /// 撤单
        /// </summary>
        public ActionResult Cancel(string stockCode, int authorizeIndex)
        {
            if (string.IsNullOrEmpty(stockCode) || authorizeIndex == 0)
            {
                throw new ArgumentException("参数不正确");
            }
            Core.TradeManager.CancelOrder(authorizeIndex, stockCode, CurrentAccount);
            return SuccessResult();
        }
    }
}