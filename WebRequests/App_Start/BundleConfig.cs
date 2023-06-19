using System.Web;
using System.Web.Optimization;

namespace WebRequests
{
    public class BundleConfig
    {
        // For more information on bundling, visit https://go.microsoft.com/fwlink/?LinkId=301862
        public static void RegisterBundles(BundleCollection bundles)
        {
            bundles.Add(new ScriptBundle("~/bundles/jquery").Include(
                        "~/Scripts/jquery-{version}.js"));

            bundles.Add(new ScriptBundle("~/bundles/jqueryval").Include(
                        "~/Scripts/jquery.validate*"));

            // Use the development version of Modernizr to develop with and learn from. Then, when you're
            // ready for production, use the build tool at https://modernizr.com to pick only the tests you need.
            bundles.Add(new ScriptBundle("~/bundles/modernizr").Include(
                        "~/Scripts/modernizr-*"));

            bundles.Add(new ScriptBundle("~/bundles/bootstrap").Include(
                         "~/Scripts/bootstrap.js",
                         "~/Scripts/bootstrap.bundle.min.js"
                         ));

            bundles.Add(new ScriptBundle("~/bundles/jqueryui").Include(
                        "~/Scripts/jquery-ui.min.js"));


            bundles.Add(new ScriptBundle("~/bundles/fileinput").Include(
                         "~/Scripts/fileinput.min.js"));

            bundles.Add(new ScriptBundle("~/bundles/theme").Include(
                       "~/Scripts/theme.min.js"));


            bundles.Add(new ScriptBundle("~/bundles/moment").Include(
                       "~/Scripts/moment.min.js"));

            bundles.Add(new ScriptBundle("~/bundles/tempus-dominus").Include(
                       "~/Scripts/tempusdominus-bootstrap-4.min.js"));

            bundles.Add(new ScriptBundle("~/bundles/datatables").Include(
           "~/Scripts/datatables.min.js"));

            bundles.Add(new StyleBundle("~/Content/css").Include(
                      "~/Content/bootstrap.min.css",
                      "~/Content/jquery-ui.min.css",
                      "~/Content/font-awesome.css",
                      "~/Content/fileinput.min.css",
                      "~/Content/tempusdominus-bootstrap-4.min.css",
                      "~/Content/site.css",
                      "~/Content/datatables.min.css"
                      ));
        }
    }
}
