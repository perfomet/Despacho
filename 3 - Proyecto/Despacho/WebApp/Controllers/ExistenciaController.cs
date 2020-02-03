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
			JsonResult result;

			if (!serie.Equals(string.Empty))
			{
				result = Json(Datos.Datos.Existencia.ObtenerExistencia(serie));
			}
			else
			{
				result = Json(Datos.Datos.Existencia.ObtenerExistencias());
			}

			result.MaxJsonLength = 50000000;

			return result;
		}

		[HttpPost]
		public JsonResult ObtenerBins(string bodega)
		{
			return Json(Datos.Datos.Bin.ObtenerBins(bodega));
		}
	}
}
