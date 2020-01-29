using System.Web.Mvc;

namespace Despacho.Controllers
{
	public class ExistenciaController : Controller
	{
		public ActionResult Index()
		{
			return View();
		}

		[HttpPost]
		public JsonResult Listar(string serie)
		{
			if (!serie.Equals(string.Empty))
			{
				return Json(Datos.Datos.Existencia.ObtenerExistencia(serie));
			}
			else
			{
				return Json(Datos.Datos.Existencia.ObtenerExistencias());
			}
		}
	}
}
