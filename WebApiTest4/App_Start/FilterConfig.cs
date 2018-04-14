using System.Web;
using System.Web.Mvc;
using WebApiTest4.Util;

namespace WebApiTest4
{
    public class FilterConfig
    {
        public static void RegisterGlobalFilters(GlobalFilterCollection filters)
        {
            filters.Add(new HandleErrorAttribute());
        }
    }
}
