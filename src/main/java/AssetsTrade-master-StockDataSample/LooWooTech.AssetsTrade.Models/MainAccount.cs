using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LooWooTech.AssetsTrade.Models
{
    [Table("MainAccountCode")]
    public class MainAccount
    {
        /// <summary>
        /// 主帐号在证券公司的资金帐号
        /// </summary>
        [Key]
        [MaxLength(20)]
        public string MainID { get; set; }
        /// <summary>
        /// 管理员ID，外键，与UserManager表的ID关联
        /// </summary>
        [MaxLength(20)]
        public string UserManagerID { get; set; }
        /// <summary>
        /// 主帐号名称
        /// </summary>
        [MaxLength(20)]
        public string MainCodeName { get; set; }
        /// <summary>
        /// 主帐号在证券公司的交易密码
        /// </summary>
        [MaxLength(20)]
        public string TradePassword { get; set; }
        /// <summary>
        /// 主帐号在证券公司的通信密码
        /// </summary>
        [MaxLength(20)]
        public string MessagePassword { get; set; }
        /// <summary>
        /// 期初资金,银证转账后需更新，资金划转不需更新
        /// </summary>
        public double InitMoney { get; set; }
        /// <summary>
        /// 可用余额
        /// </summary>
        public double UseableMoney { get; set; }
        /// <summary>
        /// 上次资金同步时间，银证转帐后
        /// </summary>
        public int LastTime { get; set; }
        /// <summary>
        /// 在证券公司开户的手续费率，注意这里必须跟在证券公司开户时一致，在证券公司费率调整后，这里必须同步调整，保持一致
        /// </summary>
        public double Commission { get; set; }
        /// <summary>
        /// 印花税，取系统默认，不允许修改，默认为成交额的千一
        /// </summary>
        public double YinHuaShui { get; set; }
        /// <summary>
        /// 过户费，取系统默认，不允许修改，默认为成交面额的万六
        /// </summary>
        public double GuoHuFei { get; set; }
        /// <summary>
        /// 所开户证券公司过户费是否最低收一元，不确定不要修改，0表示不是，1表示是
        /// </summary>
        public int IsOneMoney { get; set; }
        /// <summary>
        /// 是否登陆，0没有登陆，1登陆
        /// </summary>
        public int Isogin { get; set; }
        /// <summary>
        /// 备注
        /// </summary>
        [MaxLength(200)]
        public string Memo { get; set; }

        public int? UpdateTime { get; set; }

        /// <summary>
        /// 上海股东代码
        /// </summary>
        public string SH_GDDM { get; set; }
        /// <summary>
        /// 深圳股东代码
        /// </summary>
        public string SZ_GDDM { get; set; }
        /// <summary>
        /// 接口版本
        /// </summary>
        public string TradeApiVersion { get; set; }
        /// <summary>
        /// 营业部代码
        /// </summary>
        public string YingYeBuDM { get; set; }

    }
}
