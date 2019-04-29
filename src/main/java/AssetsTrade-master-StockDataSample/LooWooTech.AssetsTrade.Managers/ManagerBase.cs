using LooWooTech.AssetsTrade.Common;
using LooWooTech.AssetsTrade.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LooWooTech.AssetsTrade.Managers
{
    public class ManagerBase
    {
        private LocalCacheService _cache;
        protected LocalCacheService Cache
        {
            get { return _cache == null ? _cache = new LocalCacheService() : _cache; }
        }

        protected ManagerCore Core
        {
            get { return ManagerCore.Instance; }
        }

        protected DataContext GetDbContext()
        {
            return new DataContext();
        }

    }
}
