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
			Datos.Modelo.Usuario usuario = Session["usuario"] as Datos.Modelo.Usuario;

			if (!serie.Equals(string.Empty))
			{
				return Json(Datos.Datos.Existencia.ObtenerExistencia(serie));
			}
			else
			{
				 return Json(Datos.Datos.Existencia.ObtenerExistencias(usuario.ClienteId != null ? usuario.ClienteId.Value : 0));
			}
		}

		[HttpPost]
		public JsonResult ObtenerBins(string bodega)
		{
			Datos.Modelo.Usuario usuario = Session["usuario"] as Datos.Modelo.Usuario;

			return Json(Datos.Datos.Bin.ObtenerBins(bodega, usuario.ClienteId != null ? usuario.ClienteId.Value : 0));
		}
	}
}
