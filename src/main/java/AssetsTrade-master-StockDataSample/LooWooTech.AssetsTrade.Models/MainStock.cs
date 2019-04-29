using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LooWooTech.AssetsTrade.Models
{
    [Table("MainStock")]
    public class MainStock
    {
        [Key]
        public string ID { get; set; }
        /// <summary>
        /// 主账号,外键，与MainAccountCode表的MainID关联
        /// </summary>
        public string MainID { get; set; }
        /// <summary>
        /// 证券代码
        /// </summary>
        public string StockCode { get; set; }
        /// <summary>
        /// 证券名称
        /// </summary>
        public string StockName { get; set; }
        /// <summary>
        /// 期初数量
        /// </summary>
        public string InitCount { get; set; }
        /// <summary>
        /// 所有数量
        /// </summary>
        public string AllCount { get; set; }
        /// <summary>
        /// 可用数量
        /// </summary>
        public string UseableCount { get; set; }
        /// <summary>
        /// 成本价
        /// </summary>
        public string PrimeCost { get; set; }
        /// <summary>
        /// 市价
        /// </summary>
        public string CurrentPrice { get; set; }
        /// <summary>
        /// 最新更新时间
        /// </summary>
        public string LastUpdateTime { get; set; }

    }
}
