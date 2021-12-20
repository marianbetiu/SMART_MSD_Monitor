using System.Web;
using System.Web.Optimization;

namespace WebManagement
{
    public class BundleConfig
    {
        // For more information on bundling, visit http://go.microsoft.com/fwlink/?LinkId=301862
        public static void RegisterBundles(BundleCollection bundles)
        {
            //------------------------------------------- Scripts -------------------------------------------

            bundles.Add(new ScriptBundle("~/bundles/jquery").Include(
                "~/Scripts/jquery-{version}.js"
            ));

            bundles.Add(new ScriptBundle("~/bundles/jqueryval").Include(
                "~/Scripts/jquery.validate*"
            ));

            // Use the development version of Modernizr to develop with and learn from. Then, when you're
            // ready for production, use the build tool at http://modernizr.com to pick only the tests you need.
            bundles.Add(new ScriptBundle("~/bundles/modernizr").Include(
                "~/Scripts/modernizr-*"
            ));

            bundles.Add(new ScriptBundle("~/bundles/bootstrap").Include(
                "~/Scripts/bootstrap.js",
                "~/Scripts/respond.js"
            ));

            bundles.Add(new ScriptBundle("~/bundles/jqueryui").Include(
                "~/Scripts/jquery-ui-{version}.js"
            ));

            bundles.Add(new ScriptBundle("~/bundles/datatables").Include(
                "~/Scripts/DataTables/jquery.dataTables*",
                "~/Scripts/DataTables/dataTables.bootstrap*",
                "~/Scripts/DataTables/dataTables.fixedHeader*"
            ));

            bundles.Add(new ScriptBundle("~/bundles/d3").Include(
                "~/Scripts/d3/d3*"
            ));

            bundles.Add(new ScriptBundle("~/bundles/treetable").Include(
                "~/Scripts/treetable/jquery-treetable.js"
            ));

            bundles.Add(new ScriptBundle("~/bundles/lightbox").Include(
                "~/Scripts/lightbox*"
            ));

            bundles.Add(new ScriptBundle("~/bundles/timepicker").Include(
                "~/Scripts/jquery-ui-timepicker-addon*"
            ));

            bundles.Add(new ScriptBundle("~/Scripts/script-custom-editor"));

            //------------------------------------------- Styles -------------------------------------------

            bundles.Add(new StyleBundle("~/Content/css").Include(
                "~/Content/bootstrap.css",
                "~/Content/font-awesome.css",
                "~/Content/DataTables/css/jquery.dataTables*",
                "~/Content/DataTables/css/dataTables.bootstrap*",
                "~/Content/DataTables/css/fixedHeader.bootstrap*",
                "~/Content/DataTables/css/fixedHeader.dataTables*",
                "~/Scripts/treetable/jquery-treetable.css",
                "~/Content/lightbox*",
                "~/Content/jquery-ui-timepicker-addon*"
            ));

            bundles.Add(new StyleBundle("~/Content/themes").Include(
                "~/Content/themes/base/jquery-ui*",
                "~/Content/themes/ui-lightness/jquery-ui*"
            ));

            bundles.Add(new StyleBundle("~/Content/Site").Include(
                "~/Content/bootstrap.navbar.custom.css",
                "~/Content/Site.css"
            ));

            BundleTable.EnableOptimizations = false;
        }
    }
}
