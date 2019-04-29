using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;

namespace LooWooTech.AssetsTrade.WebApi
{
    public class ApiResult
    {
        /// <summary>
        /// 1：成功 0：错误
        /// </summary>
        public int Code { get; set; }
        /// <summary>
        /// 成功时返回的数据
        /// </summary>
        public object Data { get; set; }
        /// <summary>
        /// 错误时返回的信息
        /// </summary>
        public string Message { get; set; }
        /// <summary>
        /// 错误的详细信息
        /// </summary>
        public string StackTrace { get; set; }
    }
}
