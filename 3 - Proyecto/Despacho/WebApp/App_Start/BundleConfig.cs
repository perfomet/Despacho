using System.Web;
using System.Web.Optimization;

namespace Despacho
{
    public class BundleConfig
    {
        // Para obtener más información sobre las uniones, visite https://go.microsoft.com/fwlink/?LinkId=301862
        public static void RegisterBundles(BundleCollection bundles)
        {
            bundles.Add(new ScriptBundle("~/bundles/jquery").Include(
                        "~/Content/js/jquery-{version}.js"));

            bundles.Add(new ScriptBundle("~/bundles/jqueryval").Include(
                        "~/Content/js/jquery.validate*"));

            // Utilice la versión de desarrollo de Modernizr para desarrollar y obtener información. De este modo, estará
            // para la producción, use la herramienta de compilación disponible en https://modernizr.com para seleccionar solo las pruebas que necesite.
            bundles.Add(new ScriptBundle("~/bundles/modernizr").Include(
                        "~/Content/js/modernizr-*"));

            bundles.Add(new ScriptBundle("~/bundles/bootstrap").Include(
                      "~/Content/js/bootstrap.js"));

            bundles.Add(new ScriptBundle("~/bundles/jquery").Include(
                      "~/Content/js/jquery-3.4.1.js"));

            bundles.Add(new StyleBundle("~/Content/css").Include(
                      "~/Content/css/bootstrap.css",
                      "~/Content/css/site.css"));

            bundles.Add(new StyleBundle("~/Content/css/login").Include(
                      "~/Content/css/login.css"));

            bundles.Add(new StyleBundle("~/Content/css/fontawasome").Include(
                      "~/Content/librerias/fontawesome-free-5.12.0/css/fontawasome.css"));
        }
    }
}
