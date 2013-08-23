using System.Web;
using System.Web.Optimization;

namespace Conekt.Billing
{
	public class BundleConfig
	{
		//For more information on Bundling, visit http://go.microsoft.com/fwlink/?LinkId=254725
		public static void RegisterBundles(BundleCollection bundles)
		{
			bundles.Add(new ScriptBundle("~/bundles/jquery").Include(
						"~/scripts/jquery-{version}.js"
						));

			bundles.Add(new ScriptBundle("~/bundles/jqueryui").Include(
						"~/scripts/jquery-ui-{version}.js"
						));

			bundles.Add(new ScriptBundle("~/bundles/jqueryval").Include(
						"~/scripts/jquery.unobtrusive*",
						"~/scripts/jquery.validate*"
						));

			//Use the development version of Modernizr to develop with and learn from. Then, when you're
			//ready for production, use the build tool at http://modernizr.com to pick only the tests you need.
			bundles.Add(new ScriptBundle("~/bundles/modernizr").Include(
						"~/scripts/modernizr-*"
						));

			bundles.Add(new StyleBundle("~/content/themes/base/css").Include(
						"~/content/themes/base/jquery.ui.core.css",
						"~/content/themes/base/jquery.ui.resizable.css",
						"~/content/themes/base/jquery.ui.selectable.css",
						"~/content/themes/base/jquery.ui.accordion.css",
						"~/content/themes/base/jquery.ui.autocomplete.css",
						"~/content/themes/base/jquery.ui.button.css",
						"~/content/themes/base/jquery.ui.dialog.css",
						"~/content/themes/base/jquery.ui.slider.css",
						"~/content/themes/base/jquery.ui.tabs.css",
						"~/content/themes/base/jquery.ui.datepicker.css",
						"~/content/themes/base/jquery.ui.progressbar.css",
						"~/content/themes/base/jquery.ui.theme.css"
						));
			
			//Payment bundles
			bundles.Add(new ScriptBundle("~/scripts/bootstrap").Include(
						"~/scripts/bootstrap.js",
						"~/scripts/conekt.payment.demo.js",
						"~/scripts/highlight.js",
						"~/scripts/jquery.stepy.js",
						"~/scripts/spin.js"
						));

			bundles.Add(new StyleBundle("~/styles/bootstrap").Include(
						"~/content/bootstrap.css",
						"~/content/bootstrap.payment.css"
						));

			bundles.Add(new ScriptBundle("~/scripts/payment").Include(
						"~/scripts/conekt.payments.js",
						"~/scripts/spin.js"
						));

			bundles.Add(new StyleBundle("~/styles/payment").Include(
						"~/content/highlight.css",
						"~/content/payment.css"
						));

			bundles.Add(new ScriptBundle("~/scripts/ie").Include(
						"~/scripts/json2.js"
						));
		}
	}
}