using System.Web.Mvc;

namespace Despacho.Controllers
{
	public class SolicitudController : Controller
	{
		public ActionResult Index()
		{
			return View();
		}

		public ActionResult Detalle(int solicitudId)
		{
			Datos.Modelo.Solicitud solicitud = Datos.Datos.Solicitud.ObtenerSolicitud(solicitudId);

			return View(solicitud);
		}

		public ActionResult Solicitud(int? id)
		{
			if (id > 0)
			{
				Datos.Modelo.Solicitud solicitud = Datos.Datos.Solicitud.ObtenerSolicitud(id.Value);
				return View(solicitud);
			}
			else
			{
				return View(new Datos.Modelo.Solicitud());
			}
		}

		[HttpPost]
		public ActionResult Create(Datos.Modelo.Solicitud solicitud)
		{
			Datos.Modelo.Usuario solicitante = (Datos.Modelo.Usuario)Session["usuario"];

			solicitud.SolicitanteId = solicitante.UsuarioId;

			bool exito = Datos.Datos.Solicitud.Crear(solicitud);

			return Json(new { exito = exito });
		}

		[HttpPost]
		public ActionResult Edit(Datos.Modelo.Solicitud solicitud)
		{
			bool exito = Datos.Datos.Solicitud.Modificar(solicitud);

			return Json(new { exito = exito });
		}

		[HttpPost]
		public JsonResult ObtenerSolicitudes(int solicitudId)
		{
			if (solicitudId > 0)
			{
				return Json(Datos.Datos.Solicitud.ObtenerSolicitud(solicitudId));
			}
			else
			{
				return Json(Datos.Datos.Solicitud.ObtenerSolicitudes());
			}
		}
	}
}
