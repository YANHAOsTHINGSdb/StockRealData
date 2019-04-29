using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LooWooTech.AssetsTrade.Models
{
    /// <summary>
    /// 设置子账户某支股票不能买
    /// </summary>
    [Table("StockTradeSet")]
    public class StockTradeSet
    {
        [Key]
        public string ID { get; set; }

        public string ParentID { get; set; }

        /// <summary>
        /// 是否新股
        /// </summary>
        public int IsNew { get; set; }
        /// <summary>
        /// 是否ST股票
        /// </summary>
        public int IsST { get; set; }
        /// <summary>
        /// 是否是创业板股票
        /// </summary>
        public int IsChuangYeBan { get; set; }

        [MaxLength(8)]
        public string StockCode { get; set; }

        [MaxLength(10)]
        public string StockName { get; set; }

    }
}
