using LooWooTech.AssetsTrade.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LooWooTech.AssetsTrade.Managers
{
    /// <summary>
    /// 子帐户持仓管理
    /// </summary>
    public class StockManager : ManagerBase
    {
        /// <summary>
        /// 查询持仓
        /// </summary>
        public List<ChildStock> GetChildStocks(ChildAccount child)
        {
            using (var db = GetDbContext())
            {
                return db.ChildStocks.Where(e => e.ChildID == child.ChildID).ToList();
            }
        }

        /// <summary>
        /// 同步持仓股票市价
        /// </summary>
        public void SyncStocks(MainAccount account)
        {
            using (var db = GetDbContext())
            {
                var childIds = Core.AccountManager.GetChildIds(account.MainID);
                var list = db.ChildStocks.Where(e => childIds.Contains(e.ChildID));
                var stockCodes = list.Select(e => e.StockCode).ToArray();
                var data = QueryMarket(stockCodes);
                var rows = data.Split(new[] { '\n' }, StringSplitOptions.RemoveEmptyEntries);
                foreach (var row in rows)
                {
                    var market = StockMarket.Parse(row);
                    var entity = list.FirstOrDefault(e => e.StockCode == market.StockCode);
                    entity.CurrentPrice = market.CurrentPrice;
                }
                db.SaveChanges();
            }
        }

        public List<ChildStock> GetMainStocks(string mainId)
        {
            using (var db = GetDbContext())
            {
                var childIds = Core.AccountManager.GetChildIds(mainId);
                return db.ChildStocks.Where(e => childIds.Contains(e.ChildID)).ToList();
            }
        }

        public string[] GetStockCodes(int childId)
        {
            using (var db = GetDbContext())
            {
                return db.ChildStocks.Where(e => e.ChildID == childId).Select(e => e.StockCode).ToArray();
            }
        }

        /// <summary>
        /// 查询持仓接口
        /// </summary>
        /// <returns></returns>
        public List<ChildStock> QueryStocks(MainAccount account)
        {
            var list = new List<ChildStock>();
            var result = Core.ServiceManager.QueryStocks(account);
            if (result.Result)
            {
                foreach (var line in result.Data.Split(new[] { '\n' }, StringSplitOptions.RemoveEmptyEntries))
                {
                    list.Add(ChildStock.Parse(line));
                }
            }
            return list;
        }

        /// <summary>
        /// 同步股票市价
        /// </summary>
        public string QueryMarket(string[] stockCodes)
        {
            var result = Core.ServiceManager.QueryMarket(stockCodes);
            if(result.Result)
            {
                return result.Data;
            }
            else
            {
                throw new Exception(result.Error);
            }
        }
    }
}
