using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Despacho.Controllers
{
    public class UnidadNegocioController : Controller
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
			return View("/UnidadNegocio/Modificar", new Datos.Modelo.UnidadNegocio());
		}

		[HttpPost]
		public ActionResult Create(Datos.Modelo.UnidadNegocio unidadnegocio)
		{
			bool exito = Datos.Datos.UnidadNegocio.Crear(unidadnegocio);

			return Json(new { exito = exito });
		}

		public ActionResult Edit(int id)
		{
			Datos.Modelo.UnidadNegocio unidadnegocio = Datos.Datos.UnidadNegocio.ObtenerUnidadNegocio(id);

			return View("/UnidadNegocio/Modificar", unidadnegocio);
		}

		[HttpPost]
		public ActionResult Edit(Datos.Modelo.UnidadNegocio unidadnegocio)
		{
			bool exito = Datos.Datos.UnidadNegocio.Modificar(unidadnegocio);

			return Json(new { exito = exito });
		}

		[HttpPost]
		public JsonResult Delete(int id)
		{
			bool exito = Datos.Datos.UnidadNegocio.Eliminar(id);

			return Json(new { exito = exito });
		}

		[HttpPost]
		public JsonResult Listar(int Id)
		{
			if (Id > 0)
			{
				return Json(Datos.Datos.UnidadNegocio.ObtenerUnidadNegocio(Id));
			}
			else
			{
				return Json(Datos.Datos.UnidadNegocio.ObtenerUnidadesNegocio());
			}
		}
	}
}