using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LooWooTech.AssetsTrade.Managers
{
    public class ManagerCore
    {
        public static readonly ManagerCore Instance = new ManagerCore();

        private ManagerCore()
        {
            foreach (var p in this.GetType().GetProperties())
            {
                if (p.PropertyType == this.GetType())
                {
                    continue;
                }
                var val = p.GetValue(this);
                if (val == null)
                {
                    p.SetValue(this, Activator.CreateInstance(p.PropertyType));
                }
            }
        }

        public AccoutManager AccountManager { get; private set; }

        public StockManager StockManager { get; private set; }

        public AuthorizeManager AuthorizeManager { get; private set; }

        public TradeManager TradeManager { get; private set; }

        public ApiHostManager ApiHostManager { get; private set; }

        public ServiceManager ServiceManager { get; private set; }

    }
}
