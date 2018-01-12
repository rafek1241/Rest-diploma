using System.Web;
using System.Web.Optimization;

namespace Rest.Web.Engineer
{
    public class BundleConfig
    {
        // For more information on bundling, visit https://go.microsoft.com/fwlink/?LinkId=301862
        public static void RegisterBundles(BundleCollection bundles)
        {
            bundles.Add(new ScriptBundle("~/Scripts/all")
                .Include(
                    "~/Scripts/jquery-3.2.1.js",
                    "~/Scripts/angular.js",
                    "~/Scripts/angular-ui/ui-bootstrap.js",
                    "~/Scripts/filesaver.js",
                    "~/Scripts/i18n/angular-locale_pl-pl.js"
                )
                .IncludeDirectory("~/Scripts", "*.js")
                .IncludeDirectory("~/App", "*.module.js", true)
                .IncludeDirectory("~/App", "*.js", true)
            );

            bundles.Add(new StyleBundle("~/Content/css").Include(
                "~/Content/site.css"));
        }
    }
}