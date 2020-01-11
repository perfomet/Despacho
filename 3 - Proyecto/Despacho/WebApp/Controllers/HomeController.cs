using Datos.Datos;
using System.Data;
using System.Web.Mvc;

namespace Despacho.Controllers
{
    public class HomeController : Controller
    {
        public ActionResult Index()
        {
            DataTable tabla = Usuario.ObtenerUsuarios();
            return View();
        }

        public ActionResult About()
        {
            ViewBag.Message = "Your application description page.";

            return View();
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }
    }
}