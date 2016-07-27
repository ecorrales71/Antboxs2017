using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace AntBoxFrontEnd.Infrastructure
{
    public static class UrlsConstants
    {
        public static String BaseUrl { get { return "http://173.203.159.102:8081/v1"; } }

        public static String CustomerAddress { get { return BaseUrl + "/customer-address"; } }

        public static String Customer { get { return BaseUrl + "/customer"; } }

        public static String Task { get { return BaseUrl + "/tasks"; } }

        public static String Login { get { return BaseUrl + "/login"; } }
    }
}