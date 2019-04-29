using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LooWooTech.AssetsTrade.Models
{
    public class StockMarket
    {
        public string StockCode { get; set; }

        public string Name { get; set; }

        public double CurrentPrice { get; set; }

        public static StockMarket Parse(string row)
        {
            //市场	代码	活跃度	现价	昨收	开盘	最高	最低	时间	保留	总量	现量	总金额			内盘	外盘	保留	保留	买一价	卖一价	买一量	卖一量	买二价	卖二价	买二量	卖二量	买三价	卖三价	买三量	卖三量	买四价	卖四价	买四量	卖四量	买五价	卖五价	买五量	卖五量	保留	总笔	保留	保留	保留	买六价	卖六价	买六量	卖六量	买七价	卖七价	买七量	卖七量	买八价	卖八价	买八量	卖八量	买九价	卖九价	买九量	卖九量	买十价	卖十价	买十量	卖十量	买均	卖均	总买	总卖
            //1		603077	2922	5.190	5.290	5.250	5.270	5.170	150011	0		226538	1		118126992.00	131508	95030	-1		0		5.190	5.200	221	2600	5.180	5.210	5562	1454	5.170	5.220	3983	1984	5.160	5.230	2470	913	5.150	5.240	4637	2546	1592	9606	0	0	0	5.140	5.250	2217	5082	5.130	5.260	2697	1810	5.120	5.270	1637	3762	5.110	5.280	1341	1178	5.100	5.290	1873	3015	5.090	5.430	37367	70030

            var fields = row.Split(new[] { ' ', '\t' }, StringSplitOptions.RemoveEmptyEntries);
            return new StockMarket
            {
                StockCode = fields[1],
                CurrentPrice = double.Parse(fields[3])
            };

        }

    }
}
