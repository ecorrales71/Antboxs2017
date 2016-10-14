using System.Web;
using System.Web.Optimization;

namespace AntBoxFrontEnd
{
    public class BundleConfig
    {
        // For more information on bundling, visit http://go.microsoft.com/fwlink/?LinkId=301862
        public static void RegisterBundles(BundleCollection bundles)
        {
            bundles.Add(new ScriptBundle("~/bundles/jquery").Include(
                        "~/Scripts/jquery-{version}.js"));

            bundles.Add(new ScriptBundle("~/bundles/antbox").Include(
                        "~/Scripts/antbox/jquery-2.1.4.min.js",
                        "~/Scripts/antbox/plugins.js",
                        "~/Scripts/antbox/fontawesome-markers.min.js",
                        "~/Scripts/antbox/custom.js",
                        "~/Scripts/antbox/calendar.js",
                        "~/Scripts/antbox/calendar-es.js",
                        "~/Scripts/antbox/calendar-setup.js",
                        "~/Scripts/antbox/openpay.v1.js",
                        "~/Scripts/antbox/openpay-data.v1.js"
            ));

            bundles.Add(new ScriptBundle("~/bundles/jqueryval").Include(
                        "~/Scripts/jquery.validate*"));

            bundles.Add(new ScriptBundle("~/bundles/simplepaginationjs").Include(
                        "~/Scripts/jquery.simplePagination.js*"));

            // Use the development version of Modernizr to develop with and learn from. Then, when you're
            // ready for production, use the build tool at http://modernizr.com to pick only the tests you need.
            bundles.Add(new ScriptBundle("~/bundles/modernizr").Include(
                        "~/Scripts/modernizr-*"));

            bundles.Add(new ScriptBundle("~/bundles/bootstrap").Include(
                      "~/Scripts/bootstrap.js",
                      "~/Scripts/respond.js"));

            bundles.Add(new StyleBundle("~/Content/css").Include(
                      "~/Content/preloader.css",
                      "~/Content/bootstrap.css",
                      "~/Content/font-awesome.min.css",
                      "~/Content/animate.css",
                      "~/Content/revolution.css",
                      "~/Content/style.css",
                      "~/Content/calendar.css",
                      "~/Content/custom.css",
                      "~/Content/custom-responsive.css",
                      "~/Content/font.css"
                      ));
            bundles.Add(new ScriptBundle("~/Content/simplepaginationcss").Include(
                        "~/Content/simplePagination.css*"));
            bundles.Add(new StyleBundle("~/Content/Customer2").Include(
                      "~/Content/Customer/preloader.css",
                      "~/Content/bootstrap.css",
                      "~/Content/Customer/font-awesome.min.css",
                      "~/Content/animate.css",
                      "~/Content/Customer/animate.css",
                      "~/Content/Customer/revolution.css",
                      "~/Content/Customer/style.css",
                      "~/Content/Customer/calendar.css",
                      "~/Content/Customer/custom.css",
                      "~/Content/Customer/custom-responsive.css",
                      "~/Content/Customer/font.css"
                      ));
            bundles.Add(new StyleBundle("~/Content/Admin2").Include(
                      "~/Content/Customer/preloader.css",
                      "~/Content/bootstrap.css",
                      "~/Content/Customer/font-awesome.min.css",
                      "~/Content/animate.css",
                      "~/Content/Customer/animate.css",
                      "~/Content/Customer/revolution.css",
                      "~/Content/Admin/style.css",
                      "~/Content/Customer/calendar.css",
                      "~/Content/Admin/custom.css",
                      "~/Content/Admin/custom-responsive.css",
                      "~/Content/Admin/font.css"
                      ));
            bundles.Add(new StyleBundle("~/Content/Login2").Include(
                      "~/Content/bootstrap.css",
                      "~/Content/Admin/custom.css",
                      "~/Content/Admin/custom-responsive.css",
                      "~/Content/Admin/font.css"
                      ));
        }
    }
}
