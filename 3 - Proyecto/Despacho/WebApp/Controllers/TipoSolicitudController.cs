using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Despacho.Controllers
{

	public class TipoSolicitudController : Controller
	{
		public ActionResult Index()
		{
			return View();
		}

		public ActionResult Details(int id)
		{
			return View();
		}

		public ActionResult Create()
		{
			return View("TipodeSolicitud", new Datos.Modelo.TipoSolicitud());
		}

		[HttpPost]
		public ActionResult Create(Datos.Modelo.TipoSolicitud solicitud)
		{
			bool exito = Datos.Datos.TipoSolicitud.Crear(solicitud);

			return Json(new { exito = exito });
		}

		public ActionResult Edit(int id)
		{
			Datos.Modelo.TipoSolicitud solicitud = Datos.Datos.TipoSolicitud.ObtenerTipoSolicitud(id);

			return View("TipoSolicitud", solicitud);
		}

		[HttpPost]
		public ActionResult Edit(Datos.Modelo.TipoSolicitud solicitud)
		{
			bool exito = Datos.Datos.TipoSolicitud.Modificar(solicitud);

			return Json(new { exito = exito });
		}

		[HttpPost]
		public JsonResult EstaActivo(int id)
		{
			bool exito = Datos.Datos.TipoSolicitud.EstaActivo(id);

			return Json(new { exito = exito });
		}

		[HttpPost]
		public JsonResult Listar(int id)
		{
			if (id > 0)
			{
				return Json(Datos.Datos.TipoSolicitud.ObtenerTipoSolicitud(id));
			}
			else
			{
				return Json(Datos.Datos.TipoSolicitud.ObtenerTiposSolicitudes());
			}
		}
	}

}