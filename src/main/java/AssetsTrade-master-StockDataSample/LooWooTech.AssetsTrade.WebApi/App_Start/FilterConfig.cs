using System.Web;
using System.Web.Mvc;

namespace LooWooTech.AssetsTrade.WebApi
{
    public class FilterConfig
    {
        public static void RegisterGlobalFilters(GlobalFilterCollection filters)
        {
            filters.Add(new HandleErrorAttribute());
        }
    }
}
