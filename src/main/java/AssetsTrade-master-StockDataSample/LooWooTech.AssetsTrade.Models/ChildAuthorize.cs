using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using LooWooTech.AssetsTrade.Common;


namespace LooWooTech.AssetsTrade.Models
{
    /// <summary>
    /// 子帐户委托表
    /// </summary>
    [Table("ChildAuthorize")]
    public class ChildAuthorize
    {
        public static ChildAuthorize Parse(string queryData)
        {
            if (string.IsNullOrEmpty(queryData)) return null;
            //0           1       2         3    4    5     6       7           8      9   10  11   12   13         14  15
            //20:44:20  \t600118\t中国卫星  \t1\t卖出\t废单\t31.360\t100.00\t   15\t    0\t  0\t 0\t 买卖\tA474859797\t1\t1\t\n
            //10:26:46    002790  瑞尔特      0   买入 已报  16.580  16000.00    23488   0   0   0   申购  0107874749  0   0
            //13:27:03    000060  中金岭南    1   卖出 已报  11.450  5000.00     47334   0   0   0   买卖  0107874749  0   0
            //10:12:01\t732520\t司太申购\t0\t买入\t已报\t12.150\t12000.00\t19459\t0\t0\t0\t申购\tA474859797\t1\t1\t\t
            var fields = queryData.Split('\t');
            return new ChildAuthorize
            {
                StockCode = fields[1],
                StockName = fields[2],
                TradeFlag = fields[3],
                AuthorizeState = fields[5],
                AuthorizePrice = double.Parse(fields[6]),
                AuthorizeCount = (int)double.Parse(fields[7]),
                AuthorizeIndex = int.Parse(fields[8]),
                StrikeCount = (int)double.Parse(fields[9]),
                StrikePrice = double.Parse(fields[11]),
            };
        }

        [Key]
        [MaxLength(20)]
        public string ID { get; set; }
        /// <summary>
        /// 子帐号ID，外键，与子帐号表中的主键关联
        /// </summary>
        public int ChildID { get; set; }
        /// <summary>
        /// 委托时间
        /// </summary>
        [Column("AuthorizeTime")]
        public int AuthorizeTimeValue
        {
            get
            {
                return _authorizeTime.ToUnixTime();
            }
            set
            {
                _authorizeTime = value.ToDateTime();
            }
        }

        private DateTime _authorizeTime;
        [NotMapped]
        public DateTime AuthorizeTime
        {
            get
            {
                return _authorizeTime;
            }
            set
            {
                _authorizeTime = value;
            }
        }
        /// <summary>
        /// 证券代码
        /// </summary>
        public string StockCode { get; set; }
        /// <summary>
        /// 证券名称
        /// </summary>
        public string StockName { get; set; }
        /// <summary>
        /// 委托数量
        /// </summary>
        public int AuthorizeCount { get; set; }
        /// <summary>
        /// 委托价格
        /// </summary>
        public double AuthorizePrice { get; set; }
        /// <summary>
        /// 未报、待报、正报、已报、废单、部成、已成、部撤、已撤(被撤委托)、
        /// 待撤(被撤委托)、未撤(撤单委托)、待撤(撤单委托)、正撤(撤单委托)、
        /// 撤认(撤单委托)、撤废(撤单委托)、已撤(撤单委托)
        /// </summary>
        public string AuthorizeState { get; set; }
        /// <summary>
        /// 买卖标志，0表示卖出，1表示买入
        /// </summary>
        public string TradeFlag { get; set; }
        /// <summary>
        /// 委单号
        /// </summary>
        public int AuthorizeIndex { get; set; }
        /// <summary>
        /// 成交价格
        /// </summary>
        public double StrikePrice { get; set; }
        /// <summary>
        /// 成交数量
        /// </summary>
        public int StrikeCount { get; set; }
        /// <summary>
        /// 撤单数量
        /// </summary>
        public int UndoCount { get; set; }
        /// <summary>
        /// 成交时间
        /// </summary>
        public int TradeTime { get; set; }
        /// <summary>
        /// 子帐户佣金比例
        /// </summary>
        public double ChildCommission { get; set; }
        /// <summary>
        /// 主帐户佣金比例
        /// </summary>
        public double MainCommission { get; set; }
        /// <summary>
        /// 主帐户印花税比例
        /// </summary>
        public double MainYinHuaShui { get; set; }
        /// <summary>
        /// 主帐户过户费
        /// </summary>
        public double MainGuoHuFei { get; set; }
        /// <summary>
        /// 资金余额
        /// </summary>
        public double OverFlowMoney { get; set; }

        /// <summary>
        /// 计算冻结资金
        /// </summary>
        /// <returns></returns>
        public double GetFrozenMoney(ChildAccount child)
        {
            //卖出不是
            if (TradeFlag == "0") return 0;
            var total = AuthorizePrice * AuthorizeCount + child.GetGuoHuFei(StockCode, AuthorizePrice, AuthorizeCount)
                + child.GetShouXuFei(StockCode, AuthorizePrice, AuthorizeCount);

            switch (AuthorizeState)
            {
                case "失败":
                case "废单":
                    return 0;
                case "已成":
                case "部成":
                    var tradeMoney = child.GetGuoHuFei(StockCode, StrikePrice, StrikeCount) + child.GetShouXuFei(StockCode, StrikePrice, StrikeCount) + StrikeCount * StrikePrice;
                    return total - tradeMoney;
                default:
                    if (AuthorizeState.Contains("撤"))
                    {
                        return 0;
                    }
                    return total;
            }
        }
    }
}
