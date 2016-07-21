using System.Web;
using System.Web.Mvc;
using System.Linq;
using System.Net;
using System.Net.Http;

namespace AntBoxFrontEnd
{
    public class FilterConfig
    {
        public static void RegisterGlobalFilters(GlobalFilterCollection filters)
        {
            filters.Add(new HandleErrorAttribute());




        }



    }
}
