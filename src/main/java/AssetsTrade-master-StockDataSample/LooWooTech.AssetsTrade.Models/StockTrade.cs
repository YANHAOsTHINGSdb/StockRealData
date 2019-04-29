using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LooWooTech.AssetsTrade.Models
{
    /// <summary>
    /// 股票成交
    /// </summary>
    public class StockTrade
    {
        public static StockTrade Parse(string queryData)
        {
            //TdxTrade1Api接口结果
            //证券代码\t证券名称\t买卖标志\t买卖标志\t成交价格\t成交数量\t成交金额\t成交编号\t委托编号\t股东代码\t帐号类别\t保留信息\n
            //159915\t创业板\t1\t卖出\t2.095\t100.00\t209.50\t27404\t27521\t0107874749\t0\t\n


            //TdxTradeApi接口结果
            //当日成交
            //0         1          2    3     4       5       6          7成交编号  8委托编号       9           10
            //300191\t  潜能恒信\t  0\t  买入\t32.960\t100.00\t3296.00\t   56716\t   52933\t        0107874749\t0  \t  \t
            //002015    霞客环保    0   买入   9.560   600.00  5736.00    19698    19370          0107874749  0
            //000856    冀东装备    1   卖出   10.730  100.00  1073.00    19506    19206          0107874749  0
            //历史成交
            //0           1       2          3   4       5        6            7       8       9          10   11   12
            //20160225    002790  瑞尔特      0   买入    0       32.00         405417 23488    0107874749 0    0    
            //20160225\t002790\t瑞尔特\t0\t买入\t0\t32.00\t405417\t23488\t0107874749\t0\t0\t
            if (string.IsNullOrEmpty(queryData)) return null;
            var fields = queryData.Split('\t');
            //当日交易
            if (fields[0].Length == 6)
            {
                //fields = fields[0].Split(' ')
                return new StockTrade
                {
                    StockCode = fields[0],
                    StockName = fields[1],
                    TradeFlag = fields[3],
                    StrikePrice = double.Parse(fields[4]),
                    StrikeCount = (int)double.Parse(fields[5]),
                    AuthorizeIndex = fields[7],
                    StockHolderCode = fields[8],
                    TradeDate = DateTime.Today
                };
            }
            else
            {
                var year = int.Parse(fields[0].Substring(0, 4));
                var month = int.Parse(fields[0].Substring(4, 2));
                var day = int.Parse(fields[0].Substring(6));
                return new StockTrade
                {
                    StockCode = fields[1],
                    StockName = fields[2],
                    TradeFlag = fields[4],
                    StrikePrice = double.Parse(fields[5]),
                    StrikeCount = (int)double.Parse(fields[6]),

                    TradeID = fields[7],
                    AuthorizeIndex = fields[8],
                    StockHolderCode = fields[9],
                    TradeDate = new DateTime(year, month, day)
                };
            }
        }

        public string StockCode { get; set; }

        public string StockName { get; set; }

        /// <summary>
        /// 成交编号
        /// </summary>
        public string TradeID { get; set; }

        /// <summary>
        /// 委托编号
        /// </summary>
        public string AuthorizeIndex { get; set; }
        /// <summary>
        /// 股东代码
        /// </summary>
        public string StockHolderCode { get; set; }

        /// <summary>
        /// 成交金额
        /// </summary>
        public double StrikeMoney
        {
            get
            {
                return Math.Abs(StrikeCount * StrikePrice);
            }
        }
        /// <summary>
        /// 成交价格
        /// </summary>
        public double StrikePrice { get; set; }

        /// <summary>
        /// 成交数量
        /// </summary>
        public int StrikeCount { get; set; }
        /// <summary>
        /// 成交日期
        /// </summary>
        public DateTime TradeDate { get; set; }

        /// <summary>
        /// 操作类型（买入、卖出）
        /// </summary>
        public string TradeFlag { get; set; }

    }
}
