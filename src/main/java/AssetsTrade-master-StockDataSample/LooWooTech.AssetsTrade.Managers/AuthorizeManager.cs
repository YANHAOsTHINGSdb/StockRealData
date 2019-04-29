using LooWooTech.AssetsTrade.Common;
using LooWooTech.AssetsTrade.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LooWooTech.AssetsTrade.Managers
{
    public class AuthorizeManager : ManagerBase
    {
        /// <summary>
        /// 通过接口获取“当日委托”列表
        /// </summary>
        public List<ChildAuthorize> GetTodayAuthorize(MainAccount account)
        {
            var result = Core.ServiceManager.QueryAuthroizes(account);
            if (!result.Result)
            {
                throw new Exception("当日委托获取失败\n" + result.Error);
            }

            var list = new List<ChildAuthorize>();
            var rows = result.Data.Split(new[] { '\n' }, StringSplitOptions.RemoveEmptyEntries);
            foreach (var row in rows)
            {
                list.Add(ChildAuthorize.Parse(row));
            }
            return list;
        }

        public List<ChildAuthorize> GetList(ChildAccount child)
        {
            using (var db = GetDbContext())
            {
                return db.ChildAuthorizes.Where(e => e.ChildID == child.ChildID).ToList();
            }
        }

        /// <summary>
        /// 结算所有未完成的委托
        /// </summary>
        public void CloseAllAuthorize()
        {
            using (var db = GetDbContext())
            {
                var startTime = DateTime.Today.AddDays(-1).ToUnixTime();
                var list = db.ChildAuthorizes.Where(e => e.AuthorizeTimeValue > startTime && e.AuthorizeIndex != 0);
                foreach (var model in list)
                {
                    if ("已成,已撤,部撤,废单".Contains(model.AuthorizeState))
                    {
                        continue;
                    }
                    //如果有成交量，则是部撤，否则为已撤
                    model.AuthorizeState = model.StrikeCount > 0 ? "部废" : "废单";
                    Core.TradeManager.UpdateAuthorize(model);
                }
            }
        }
    }
}
