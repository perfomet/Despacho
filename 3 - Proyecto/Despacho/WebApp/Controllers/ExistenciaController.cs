using System.Web.Mvc;

namespace Despacho.Controllers
{
	public class ExistenciaController : Controller
	{
		public ActionResult Index()
		{
			return View();
		}

		public ActionResult Bodega()
		{
			return View("Bodega");
		}

		[HttpPost]
		public JsonResult Listar(string serie)
		{
			Datos.Modelo.Usuario usuario = Session["usuario"] as Datos.Modelo.Usuario;
			JsonResult result;

			if (!serie.Equals(string.Empty))
			{
				result = Json(Datos.Datos.Existencia.ObtenerExistencia(serie));
			}
			else
			{
				result = Json(Datos.Datos.Existencia.ObtenerExistencias(usuario.ClienteId != null ? usuario.ClienteId.Value : 0));
			}

			result.MaxJsonLength = 10000000;

			return result;
		}

		[HttpPost]
		public JsonResult ObtenerBins(string bodega)
		{
			Datos.Modelo.Usuario usuario = Session["usuario"] as Datos.Modelo.Usuario;

			return Json(Datos.Datos.Bin.ObtenerBins(bodega, usuario.ClienteId != null ? usuario.ClienteId.Value : 0));
		}
	}
}
