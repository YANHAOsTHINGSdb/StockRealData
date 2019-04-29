using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using LooWooTech.AssetsTrade.Common;

namespace LooWooTech.AssetsTrade.Models
{
    /// <summary>
    /// 资金流水记录
    /// </summary>
    public class FundFlow
    {
        public static FundFlow Parse(string queryData)
        {
            //发生日期\t    备注\t    证券代码\t  证券名称\t  买卖标志\t  买卖标志5\t  成交数量6\t  成交价格\t  成交金额\t      佣金\t    印花税10\t   过户费\t   发生金额12\t      剩余金额\t      申报序号\t      股东代码13\t      帐号类别\t      席位代码15\t   委托编号\t   成交编号\t  证券数量20\t  其他费\t   保留信息\n
            //20160324\t    证券买入\t603128\t  华贸物流\t  0\t         买入\t       10000.00\t 10.260\t    102600.00\t    20.52\t  0\t          2.05\t    -102622.57\t    -22340.46\t       1713\t        A474859797\t    1\t             41067\t       1713\t      58807\t    10000.00\t    0.00\t"
            
            //0       1           2         3           4          5   6   7            8       9         10  11    12      13          14         15      16      17      18
            //18195   20160225    证券卖出    91702.98 - 96619.19   0   1   A474859797  600785  新华百货    1   卖出  24.815 - 3700.00    证券卖出    18.36   91.81   1.85    0.00
            //18197   20160225    证券买入 - 49072.80 - 145691.99  0   1   A474859797  600737  中粮屯河    0   买入  12.580  3900.00 证券买入    9.81    0   0.99    0.00
            var fields = queryData.Split('\t');
            //第一列如果是日期，则是使用的TdxTrade1Api
            if (fields[0].Length == 8)
            {
                return new FundFlow {
                    ID = fields[19],
                    CreateDate = fields[0].ToDate(),
                    BusinessName = fields[1],
                    AmountMoney = double.Parse(fields[12]),
                    RemnantMoney = double.Parse(fields[13]),
                    Currency = "人民币",
                    StockHolderCode = fields[20],
                    StockCode = fields[2],
                    StockName= fields[3],
                    TradeFlag = fields[5],
                    StrikeCount = (int)double.Parse(fields[6]),
                    StrikePrice = double.Parse(fields[7]),
                    Memo = fields[1],
                    Commission = double.Parse(fields[9]),
                    YinHuaShui = double.Parse(fields[10]),
                    GuoHuFei = double.Parse(fields[11]),
                    OtherCost = double.Parse(fields[21])
                };
            }
            else
            {
                return new FundFlow
                {
                    ID = fields[0],
                    CreateDate = fields[1].ToDate(),
                    BusinessName = fields[2],
                    AmountMoney = double.Parse(fields[3]),
                    RemnantMoney = double.Parse(fields[4]),
                    Currency = "人民币",
                    StockHolderCode = fields[7],
                    StockCode = fields[8],
                    StockName = fields[9],
                    TradeFlag = fields[11],
                    StrikePrice = double.Parse(fields[12]),
                    StrikeCount = (int)double.Parse(fields[13]),
                    Memo = fields[14],
                    Commission = double.Parse(fields[15]),
                    YinHuaShui = double.Parse(fields[16]),
                    GuoHuFei = double.Parse(fields[17]),
                    OtherCost = double.Parse(fields[18]),
                };
            }
        }
        /// <summary>
        /// 流水号
        /// </summary>
        public string ID { get; set; }
        /// <summary>
        /// 发生日期
        /// </summary>
        public DateTime CreateDate { get; set; }

        /// <summary>
        /// 业务名称
        /// </summary>
        public string BusinessName { get; set; }

        /// <summary>
        /// 发生金额
        /// </summary>
        public double AmountMoney { get; set; }
        /// <summary>
        /// 剩余金额
        /// </summary>
        public double RemnantMoney { get; set; }
        /// <summary>
        /// 币种
        /// </summary>
        public string Currency { get; set; }
        /// <summary>
        /// 股东代码
        /// </summary>
        public string StockHolderCode { get; set; }

        public string StockCode { get; set; }

        public string StockName { get; set; }

        /// <summary>
        /// 交易标志 买入或卖出
        /// </summary>
        public string TradeFlag { get; set; }
        /// <summary>
        /// 成交价格
        /// </summary>
        public double StrikePrice { get; set; }
        /// <summary>
        /// 成交数量
        /// </summary>
        public int StrikeCount { get; set; }
        /// <summary>
        /// 备注
        /// </summary>
        public string Memo { get; set; }

        /// <summary>
        /// 佣金
        /// </summary>
        public double Commission { get; set; }
        /// <summary>
        /// 印花税
        /// </summary>
        public double YinHuaShui { get; set; }
        /// <summary>
        /// 过户费
        /// </summary>
        public double GuoHuFei { get; set; }
        /// <summary>
        /// 其他费用
        /// </summary>
        public double OtherCost { get; set; }
    }
}
