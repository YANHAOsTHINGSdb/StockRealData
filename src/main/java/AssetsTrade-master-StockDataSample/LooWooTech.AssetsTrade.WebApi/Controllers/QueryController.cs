using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;

namespace LooWooTech.AssetsTrade.WebApi.Controllers
{
    public class QueryController : ControllerBase
    {
        /// <summary>
        /// 查询持仓
        /// </summary>
        public ActionResult Stocks()
        {
            var list = Core.StockManager.GetChildStocks(CurrentAccount);
            return SuccessResult(list);
        }

        /// <summary>
        /// 查询当日委托
        /// </summary>
        public ActionResult Authorizes()
        {
            var list = Core.AuthorizeManager.GetList(CurrentAccount);
            return SuccessResult(list);
        }

        /// <summary>
        /// 查询当日成交
        /// </summary>
        public ActionResult Trades()
        {
            var list = Core.TradeManager.GetTodayTrades(CurrentAccount);
            return SuccessResult(list);
        }

        /// <summary>
        /// 查询当日撤单
        /// </summary>
        public ActionResult CancelOrders()
        {
            var list = Core.AuthorizeManager.GetList(CurrentAccount);
            return SuccessResult(list.Where(e => e.AuthorizeState.Contains("撤")).ToList());
        }

        /// <summary>
        /// 查询历史成交
        /// </summary>
        /// <returns></returns>
        public ActionResult HistoryTrades(DateTime? start, DateTime? end)
        {
            var startDate = start.HasValue ? start.Value : DateTime.Today.AddDays(-1);
            var endDate = end.HasValue ? end.Value : DateTime.Today.AddDays(1);

            var list = Core.TradeManager.GetHistoryTrades(startDate, endDate, CurrentAccount);
            return SuccessResult(list);
        }

        public ActionResult Account()
        {
            return SuccessResult(CurrentAccount);
        }

        /// <summary>
        /// 查询历史资金流水
        /// </summary>
        public ActionResult HistoryMoney(DateTime? start, DateTime? end)
        {
            var startDate = start.HasValue ? start.Value : DateTime.Today.AddDays(-1);
            var endDate = end.HasValue ? end.Value : DateTime.Today.AddDays(1);

            var list = Core.TradeManager.GetHistoryMoney(startDate, endDate, CurrentAccount);
            return SuccessResult(list);
        }

        public ActionResult FrozenMoney()
        {
            var result = Core.TradeManager.GetFrozenMoney(CurrentAccount);
            return SuccessResult(result);
        }

        public ActionResult Market(string stockCodes)
        {
            if (string.IsNullOrEmpty(stockCodes))
            {
                throw new ArgumentException("参数错误");
            }
            var data = Core.StockManager.QueryMarket(stockCodes.Split(new[] { ',' }, StringSplitOptions.RemoveEmptyEntries));
            return SuccessResult(data.Replace("\t", "|"));
        }
    }
}