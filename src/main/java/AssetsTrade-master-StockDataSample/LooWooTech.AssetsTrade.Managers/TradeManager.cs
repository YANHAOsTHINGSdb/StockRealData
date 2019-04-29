using LooWooTech.AssetsTrade.Common;
using LooWooTech.AssetsTrade.Models;

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LooWooTech.AssetsTrade.Managers
{
    public class TradeManager : ManagerBase
    {
        private bool IsTradeTime()
        {
            var time = DateTime.Now;
            var week = time.DayOfWeek;
            if (week == DayOfWeek.Saturday || week == DayOfWeek.Sunday)
            {
                return false;
            }
            if (time.Hour < 9 || (time.Hour >= 15 && time.Hour < 21))
            {
                return false;
            }
            //if (time.Hour >= 9 && time.Minute < 15)
            //{
            //    return false;
            //}
            return true;
        }

        /// <summary>
        /// 买入股票
        /// </summary>
        public void ToBuy(string stockCode, int number, double price, ChildAccount child)
        {
            using (var db = GetDbContext())
            {
                if(!IsTradeTime())
                {
                    throw new Exception("当前时间不能交易");
                }
                //总费用
                var totalPrice = number * price + child.GetShouXuFei(stockCode, price, number) + child.GetGuoHuFei(stockCode, price, number);
                //判断余额
                if (child.UseableMoney < totalPrice)
                {
                    throw new Exception("余额不足");
                }
                //验证是否被禁止购买
                if (db.StockTradeSets.Any(e => e.ParentID == child.ParentID && e.StockCode == stockCode))
                {
                    throw new Exception("该股票禁止购买");
                }

                //调用接口购买
                var result = Core.ServiceManager.Buy(child.Parent, stockCode, number, price);
                var authorize = new ChildAuthorize
                {
                    ID = DateTime.Now.ToString("yyyyMMddHHmmssffff"),
                    AuthorizeIndex = 0,
                    AuthorizeCount = number,
                    AuthorizePrice = price,
                    ChildID = child.ChildID,
                    ChildCommission = child.Commission,
                    StockCode = stockCode,
                    StockName = stockCode,//委托创建时无法获得股票名称
                    TradeFlag = "1",
                    MainCommission = child.Parent.Commission,
                    MainGuoHuFei = child.Parent.GuoHuFei,
                    MainYinHuaShui = child.Parent.YinHuaShui,
                    AuthorizeState = "未报",
                    AuthorizeTime = DateTime.Now,
                    OverFlowMoney = child.UseableMoney - totalPrice,//佣金、过户费要在成交时扣除
                };

                if (result.Result)
                {
                    var toUpdateChildEntity = db.ChildAccounts.FirstOrDefault(e => e.ChildID == child.ChildID);
                    //冻结股票资金和相关费用
                    toUpdateChildEntity.UseableMoney -= totalPrice;
                    //委托编号
                    authorize.AuthorizeIndex = int.Parse(result.Data);

                    db.ChildAuthorizes.Add(authorize);
                    db.SaveChanges();
                }
                else
                {
                    authorize.AuthorizeState = "失败";
                    db.ChildAuthorizes.Add(authorize);
                    db.SaveChanges();
                    throw new Exception("委托失败\n" + result.Error);
                }

            }

        }

        /// <summary>
        /// 卖出股票
        /// </summary>
        public void ToSell(string stockCode, int number, double price, ChildAccount child)
        {
            using (var db = GetDbContext())
            {
                if (!IsTradeTime())
                {
                    throw new Exception("当前时间不能交易");
                }
                var stock = db.ChildStocks.FirstOrDefault(e => e.StockCode == stockCode && e.ChildID == child.ChildID);
                if (stock == null)
                {
                    throw new ArgumentException("没有持有该股票");
                }
                //查看持仓 数量是否符合
                if (stock.UseableCount < number)
                {
                    throw new ArgumentException("没有足够的股票可以卖出");
                }
                //如果有零手股，则number必须是100的倍数或等于可用股票余额数
                if (stock.UseableCount % 100 != 0 && number % 100 != 0 && number != stock.UseableCount)
                {
                    throw new ArgumentException("卖出股票数量不正确");
                }

                //调用卖出接口 
                var result = Core.ServiceManager.Sell(child.Parent, stockCode, number, price);
                //声明一个新委托
                var model = new ChildAuthorize
                {
                    ID = DateTime.Now.ToString("yyyyMMddHHmmssffff"),
                    AuthorizeIndex = 0,
                    AuthorizeCount = number,
                    AuthorizePrice = price,
                    StockCode = stockCode,
                    AuthorizeState = "待报",
                    AuthorizeTime = DateTime.Now,
                    ChildCommission = child.Commission,
                    ChildID = child.ChildID,
                    StockName = stockCode,
                    TradeFlag = "0",
                    OverFlowMoney = child.UseableMoney,
                    MainCommission = child.Parent.Commission,
                    MainYinHuaShui = child.Parent.YinHuaShui,
                    MainGuoHuFei = child.Parent.GuoHuFei,
                };
                //如果调用接口成功
                if (result.Result)
                {
                    //如果是卖出，先扣除印花税，成交时不再扣，如果部分成交，则需要返还部分税
                    var toUpdateChild = db.ChildAccounts.FirstOrDefault(e => e.ChildID == child.ChildID);
                    toUpdateChild.UseableMoney -= child.GetYinHuaShui(model.StockCode, model.StrikePrice, model.StrikeCount);

                    //赋值委托编号
                    model.AuthorizeIndex = int.Parse(result.Data);
                    //持仓总量-卖出数量
                    stock.AllCount -= number;
                    //可用数量-卖出数量（可卖出股票必定是可用股票）
                    stock.UseableCount -= number;

                    db.ChildAuthorizes.Add(model);
                    db.SaveChanges();
                }
                else
                {
                    db.ChildAuthorizes.Add(model);
                    db.SaveChanges();
                    throw new Exception("卖出失败\n" + result.Error);
                }
            }
        }

        /// <summary>
        /// 撤单
        /// </summary>
        public void CancelOrder(int authorizeIndex, string stockCode, ChildAccount child)
        {
            using (var db = GetDbContext())
            {
                if (!IsTradeTime())
                {
                    throw new Exception("当前时间不能交易");
                }
                var authorize = db.ChildAuthorizes.FirstOrDefault(e => e.AuthorizeIndex == authorizeIndex && e.ChildID == child.ChildID);
                if (authorize == null)
                {
                    throw new ArgumentException("没有找到该委托");
                }
                if (authorize.AuthorizeState.Contains("撤"))
                {
                    throw new Exception("该委托已提交过撤单");
                }
                var result = Core.ServiceManager.Cancel(child.Parent, stockCode, authorize.AuthorizeIndex.ToString());
                if (result.Result)
                {
                    authorize.AuthorizeState = "待撤";
                    db.SaveChanges();
                }
                else
                {
                    throw new Exception("撤单失败\n" + result.Error);
                }
            }
        }

        /// <summary>
        /// 更新委托的状态
        /// </summary>
        public void UpdateAuthorize(ChildAuthorize model)
        {
            using (var db = GetDbContext())
            {
                var entity = db.ChildAuthorizes.FirstOrDefault(e => e.AuthorizeIndex == model.AuthorizeIndex);
                if (entity == null) return;
                var endStatus = "已成,已撤,部撤,部废,废单";
                //如果状态不变且处于终结状态，则说明是当前状态已处理过的委托
                if (entity.AuthorizeState == model.AuthorizeState && endStatus.Contains(model.AuthorizeState))
                {
                    return;
                }

                var isBuy = entity.TradeFlag == "1";
                var child = db.ChildAccounts.FirstOrDefault(e => e.ChildID == entity.ChildID);
                var stock = db.ChildStocks.FirstOrDefault(e => e.StockCode == model.StockCode && e.ChildID == entity.ChildID);
                //有成交量，先结算成交量
                if ("已成,部撤,部废".Contains(model.AuthorizeState))
                {
                    if (isBuy)
                    {
                        //如果是买入，持仓股票总量增加
                        if (stock != null)
                        {
                            stock.AllCount += model.StrikeCount;
                        }
                        else
                        {
                            //如果持仓不存在该股，则添加新纪录
                            stock = new ChildStock
                            {
                                AllCount = model.StrikeCount,
                                ChildID = entity.ChildID,
                                CurrentPrice = model.StrikePrice,
                                LastTime = DateTime.Now.ToUnixTime(),
                                PrimeCost = model.StrikePrice,
                                StockCode = model.StockCode,
                                StockName = model.StockName,
                                ID = DateTime.Now.ToString("yyyyMMddHHmmssffff")
                            };
                            db.ChildStocks.Add(stock);
                        }
                    }
                    else
                    {
                        //如果卖出成功，则只需要余额增加股票成交额即可，手续费在发起委托时已扣
                        child.UseableMoney += model.StrikePrice * model.StrikeCount;
                        //更新持仓数量
                        stock.AllCount -= model.StrikeCount;
                        stock.UseableCount -= model.StrikeCount;
                    }
                }
                //有废单或撤单，撤销相关资产
                if ("废单,已撤,部撤,部废".Contains(model.AuthorizeState))
                {
                    //如果是撤单，更新撤单数量字段
                    if (model.AuthorizeState.Contains("撤"))
                    {
                        entity.UndoCount = model.AuthorizeCount - model.StrikeCount;
                    }
                    //不论买入还是卖出，都事先扣除了手续费，在撤单时要归还撤单数量对应的手续费
                    var undoMoney = model.AuthorizePrice * model.UndoCount +
                        child.GetGuoHuFei(model.StockCode, model.AuthorizePrice, model.UndoCount) +
                        child.GetShouXuFei(model.StockCode, model.AuthorizePrice, model.UndoCount);
                    child.UseableMoney += undoMoney;

                    if (isBuy)
                    {
                        //买入并不会更新股票可用余额 所以只需要更新总量
                        stock.AllCount -= model.StrikeCount;
                    }
                    else
                    {
                        //更新持仓数量
                        stock.AllCount += model.StrikeCount;
                        stock.UseableCount += model.StrikeCount;
                    }
                }

                entity.StrikeCount = model.StrikeCount;
                entity.StrikePrice = model.StrikePrice;
                entity.AuthorizeState = model.AuthorizeState;
                entity.StockName = model.StockName;

                db.SaveChanges();
            }
        }

        /// <summary>
        /// 获取历史资金流水
        /// </summary>
        public List<FundFlow> GetHistoryMoney(DateTime startDate, DateTime endDate, ChildAccount child)
        {
            var result = Core.ServiceManager.QueryHistoryMoney(child.Parent, startDate, endDate);
            if (result.Result)
            {
                var list = new List<FundFlow>();
                var lines = result.Data.Split(new[] { '\n' }, StringSplitOptions.RemoveEmptyEntries);
                foreach (var line in lines)
                {
                    list.Add(FundFlow.Parse(line));
                }
                var childStockCodes = Core.StockManager.GetStockCodes(child.ChildID);
                return list.Where(e => childStockCodes.Contains(e.StockCode)).ToList();
            }
            throw new Exception(result.Error);
        }

        /// <summary>
        /// 获取历史成交
        /// </summary>
        public List<StockTrade> GetHistoryTrades(DateTime startDate, DateTime endDate, ChildAccount child)
        {
            var result = Core.ServiceManager.QueryHistoryTrade(child.Parent, startDate, endDate);
            if (result.Result)
            {
                var list = new List<StockTrade>();
                var lines = result.Data.Split(new[] { '\n' }, StringSplitOptions.RemoveEmptyEntries);
                foreach (var line in lines)
                {
                    list.Add(StockTrade.Parse(line));
                }

                var childStockCodes = Core.StockManager.GetStockCodes(child.ChildID);
                return list.Where(e => childStockCodes.Contains(e.StockCode)).ToList();
            }
            throw new Exception(result.Error);
        }

        /// <summary>
        /// 获取当日成交
        /// </summary>
        public List<StockTrade> GetTodayTrades(ChildAccount child)
        {
            var result = Core.ServiceManager.QueryTrades(child.Parent);
            if (result.Result)
            {
                var list = new List<StockTrade>();
                var lines = result.Data.Split(new[] { '\n' }, StringSplitOptions.RemoveEmptyEntries);
                foreach (var line in lines)
                {
                    list.Add(StockTrade.Parse(line));
                }

                var childStockCodes = Core.StockManager.GetStockCodes(child.ChildID);
                return list.Where(e => childStockCodes.Contains(e.StockCode)).ToList();
            }
            throw new Exception(result.Error);
        }

        /// <summary>
        /// 获取冻结资金
        /// </summary>
        public double GetFrozenMoney(ChildAccount child)
        {
            //因为每天需要结算，所以如果是昨天的委托，则判断状态，如果是今天的，查询买入且没有撤单、废单、部撤的
            using (var db = GetDbContext())
            {
                var minDate = DateTime.Today.AddDays(-1).ToUnixTime();
                var list = db.ChildAuthorizes.Where(e => e.ChildID == child.ChildID && e.AuthorizeTimeValue > minDate).ToList();
                double result = 0;
                foreach (var item in list)
                {
                    //如果是昨日成交的委托，则不参与计算
                    if (item.TradeTime > 0 && item.TradeTime < DateTime.Today.ToUnixTime())
                    {
                        continue;
                    }
                    result += item.GetFrozenMoney(child);
                }
                return result;
            }
        }

    }
}
