using System.Collections.Generic;
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
		public ActionResult Create(Datos.Modelo.Solicitud solicitud, List<Datos.Modelo.EquipoSolicitado> equiposSolicitados)
		{
			Datos.Modelo.Usuario solicitante = (Datos.Modelo.Usuario)Session["usuario"];

			solicitud.SolicitanteId = solicitante.UsuarioId;

			int solicitudId = Datos.Datos.Solicitud.Crear(solicitud);

			equiposSolicitados.ForEach((equipo) =>
			{
				equipo.SolicitudDespachoId = solicitudId;
			});

			Datos.Datos.EquipoSolicitado.Crear(equiposSolicitados);

			return Json(new { exito = solicitudId > 0 });
		}

		[HttpPost]
		public ActionResult Edit(Datos.Modelo.Solicitud solicitud, List<Datos.Modelo.EquipoSolicitado> equiposSolicitados)
		{
			Datos.Datos.EquipoSolicitado.EliminarPorSolicitud(solicitud.SolicitudDespachoId);
			Datos.Datos.EquipoSolicitado.Crear(equiposSolicitados);

			bool exito = Datos.Datos.Solicitud.Modificar(solicitud);

			return Json(new { exito = exito });
		}

		[HttpPost]
		public JsonResult ObtenerSolicitudes(int solicitudId)
		{
			Datos.Modelo.Usuario usuario = Session["usuario"] as Datos.Modelo.Usuario;

			if (solicitudId > 0)
			{
				return Json(Datos.Datos.Solicitud.ObtenerSolicitud(solicitudId));
			}
			else
			{
				return Json(Datos.Datos.Solicitud.ObtenerSolicitudes(usuario.ClienteId != null ? usuario.ClienteId.Value : 0));
			}
		}

		[HttpPost]
		public JsonResult ObtenerEquiposSolicitados(int solicitudId)
		{
			return Json(Datos.Datos.EquipoSolicitado.ObtenerEquiposSolicitados(solicitudId));
		}

		[HttpPost]
		public JsonResult ObtenerComunas(int regionId)
		{
			return Json(Datos.Datos.Comuna.ObtenerComunas(regionId, Datos.Datos.TipoFiltroComuna.RegionId));
		}
	}
}
