using System.Web;
using System.Web.Optimization;

namespace Despacho
{
	public class BundleConfig
	{
		// Para obtener más información sobre las uniones, visite https://go.microsoft.com/fwlink/?LinkId=301862
		public static void RegisterBundles(BundleCollection bundles)
		{

			#region Originales

			bundles.Add(new ScriptBundle("~/bundles/jquery")
					.Include("~/Content/js/externos/jquery-{version}.js"));

			bundles.Add(new ScriptBundle("~/bundles/jqueryval")
					.Include("~/Content/js/externos/jquery.validate*"));

			// Utilice la versión de desarrollo de Modernizr para desarrollar y obtener información. De este modo, estará
			// para la producción, use la herramienta de compilación disponible en https://modernizr.com para seleccionar solo las pruebas que necesite.
			bundles.Add(new ScriptBundle("~/bundles/modernizr")
					.Include("~/Content/js/externos/modernizr-*"));

			bundles.Add(new ScriptBundle("~/bundles/bootstrap")
					.Include("~/Content/js/externos/bootstrap.js"));

			bundles.Add(new ScriptBundle("~/bundles/jquery")
					.Include("~/Content/js/externos/jquery-3.4.1.js"));

			bundles.Add(new StyleBundle("~/Content/css")
					.Include("~/Content/css/externos/bootstrap.css"));

			#endregion

			#region Librerias

			bundles.Add(new StyleBundle("~/Content/css/fontawesome")
					.Include("~/Content/librerias/fontawesome/css/fontawesome.css")
					.Include("~/Content/librerias/fontawesome/css/brands.css")
					.Include("~/Content/librerias/fontawesome/css/solid.css"));

			bundles.Add(new StyleBundle("~/bundles/fontawesome")
					.Include("~/Content/librerias/fontawesome/js/fontawesome.js")
					.Include("~/Content/librerias/fontawesome/js/brands.js")
					.Include("~/Content/librerias/fontawesome/js/solid.js"));

			bundles.Add(new StyleBundle("~/Content/css/metronic")
					.Include("~/Content/librerias/metronic/assets/vendors/custom/fullcalendar/fullcalendar.bundle.css")
					.Include("~/Content/librerias/metronic/assets/vendors/base/vendors.bundle.css")
					.Include("~/Content/librerias/metronic/assets/themes/demo12/base/style.bundle.css"));

			bundles.Add(new ScriptBundle("~/bundles/metronic")
					.Include("~/Content/librerias/metronic/assets/vendors/base/vendors.bundle.js")
					.Include("~/Content/librerias/metronic/assets/themes/demo12/base/scripts.bundle.js")
					.Include("~/Content/librerias/metronic/assets/vendors/custom/fullcalendar/fullcalendar.bundle.js")
					.Include("~/Content/librerias/metronic/assets/app/js/dashboard.js"));

			#endregion

			#region Páginas

			bundles.Add(new ScriptBundle("~/bundles/general")
								.Include("~/Content/js/General.js"));

			bundles.Add(new ScriptBundle("~/bundles/login")
					.Include("~/Content/js/Login.js"));

			bundles.Add(new ScriptBundle("~/bundles/cliente")
					.Include("~/Content/js/Cliente.js"));

			bundles.Add(new StyleBundle("~/Content/less/general")
					.Include("~/Content/css/general.less"));

			bundles.Add(new StyleBundle("~/Content/less/login")
					.Include("~/Content/css/login.less"));

			#endregion
		}
	}
}
