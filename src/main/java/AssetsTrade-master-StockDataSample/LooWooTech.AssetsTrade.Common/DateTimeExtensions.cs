using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LooWooTech.AssetsTrade.Common
{
   public static  class DateTimeExtensions
    {
        private static DateTime _startTime = new DateTime(1970, 1, 1);
        public static int ToUnixTime(this DateTime time)
        {
            return (int)time.Subtract(_startTime).TotalSeconds;
        }

        public static DateTime ToDateTime(this int unixTime)
        {
            return _startTime.AddSeconds(unixTime);
        }
    }
}
