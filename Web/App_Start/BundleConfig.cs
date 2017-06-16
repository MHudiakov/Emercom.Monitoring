// --------------------------------------------------------------------------------------------------------------------
// <copyright file="BundleConfig.cs" company="ИНИТ-центр">
//   ИНИТ-центр, 2014г.
// </copyright>
// <summary>
//   Defines the BundleConfig type.
// </summary>
// --------------------------------------------------------------------------------------------------------------------

namespace Web
{
    using System.Web.Optimization;

    public class BundleConfig
    {
        public static void AddDefaultIgnorePatterns(IgnoreList ignoreList)
        {
            if (ignoreList == null)
                throw new System.ArgumentNullException("ignoreList");
            ignoreList.Ignore("*.intellisense.js");
            ignoreList.Ignore("*-vsdoc.js");
            ignoreList.Ignore("*.debug.js", OptimizationMode.WhenEnabled);
            ignoreList.Ignore("*.min.css", OptimizationMode.WhenDisabled);
        }

        // For more information on Bundling, visit http://go.microsoft.com/fwlink/?LinkId=254725
        public static void RegisterBundles(BundleCollection bundles)
        {
            bundles.IgnoreList.Clear();
            AddDefaultIgnorePatterns(bundles.IgnoreList);

            bundles.Add(new ScriptBundle("~/jsmain").Include(
                "~/Scripts/jquery-{version}.js",
                "~/Scripts/bootstrap.min.js",
                "~/Scripts/conditionizr.min.js",
                "~/Scripts/plugins/core/nicescroll/jquery.nicescroll.min.js",
                "~/Scripts/plugins/core/jrespond/jRespond.min.js",
                "~/Scripts/plugins/ui/jgrowl/jquery.jgrowl.min.js",
                "~/Scripts/plugins/forms/uniform/jquery.uniform.min.js",
                "~/Scripts/app.js",
                "~/Scripts/jquery.genyxAdmin.js",
                "~/Scripts/plugins/tables/datatables/jquery.dataTables.min.js",
                //"~/Scripts/plugins/misc/gallery/jquery.magnific-popup.min.js",
                //"~/Scripts/plugins/misc/fullcalendar/fullcalendar.min.js",
                "~/Scripts/modernizr-*"));

            bundles.Add(new StyleBundle("~/cssBundles").Include(
                  "~/Content/themes/base/jquery.ui.core.css",
                  "~/Content/themes/base/jquery.ui.resizable.css",
                  "~/Content/themes/base/jquery.ui.selectable.css",
                  "~/Content/themes/base/jquery.ui.accordion.css",
                  "~/Content/themes/base/jquery.ui.autocomplete.css",
                  "~/Content/themes/base/jquery.ui.button.css",
                  "~/Content/themes/base/jquery.ui.dialog.css",
                  "~/Content/themes/base/jquery.ui.slider.css",
                  "~/Content/themes/base/jquery.ui.tabs.css",
                //  "~/Content/themes/base/jquery.ui.datepicker.css",
                "~/Content/themes/base/jquery.ui.progressbar.css",
                "~/Content/themes/base/jquery.ui.theme.css",
                "~/Content/bootstrap.css",
                "~/Content/bootstrap-theme.css",
                "~/Content/site.css",
                "~/Content/css/genyx-theme/jquery.ui.genyx.css",
                "~/Content/bootstrap-mvc-validation.css",
                "~/Content/css/icons.css",
                "~/Scripts/plugins/misc/gallery/magnific-popup.css",
                "~/Scripts/plugins/forms/uniform/uniform.default.css",
                "~/Scripts/plugins/misc/fullcalendar/fullcalendar.css",
                "~/Scripts/plugins/ui/jgrowl/jquery.jgrowl.css",
                "~/Scripts/plugins/tables/datatables/jquery.dataTables.css",
                "~/Scripts/plugins/forms/switch/bootstrapSwitch.css",
                  "~/Content/css/app.css",
                "~/Content/bootstrap-datetimepicker.css",
                "~/Content/css/genyx-theme/jquery.ui.genyx.css",
                "~/Content/portal.css",
                "~/Content/css/fonts/icomoon.woff",
                "~/Content/css/fonts/icomoon.ttf"));
        }
    }
}
