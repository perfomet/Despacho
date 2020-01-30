using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Despacho.Controllers
{
	public class EmpresaTransporteController : Controller
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
			return View("EmpresaTransporte", new Datos.Modelo.EmpresaTransporte());
		}

		[HttpPost]
		public ActionResult Create(Datos.Modelo.EmpresaTransporte empresatransporte)
		{
			bool exito = Datos.Datos.EmpresaTransporte.Crear(empresatransporte);

			return Json(new { exito = exito });
		}

		public ActionResult Edit(int id)
		{
			Datos.Modelo.EmpresaTransporte empresatransporte = Datos.Datos.EmpresaTransporte.ObtenerEmpresaTransporte(id);

			return View("EmpresaTransporte", empresatransporte);
		}

		[HttpPost]
		public ActionResult Edit(Datos.Modelo.EmpresaTransporte empresatransporte)
		{
			bool exito = Datos.Datos.EmpresaTransporte.Modificar(empresatransporte);

			return Json(new { exito = exito });
		}

		[HttpPost]
		public JsonResult EstaActivo(int id)
		{
			bool exito = Datos.Datos.EmpresaTransporte.EstaActivo(id);

			return Json(new { exito = exito });
		}

		[HttpPost]
		public JsonResult Listar(int Id)
		{
			if (Id > 0)
			{
				return Json(Datos.Datos.EmpresaTransporte.ObtenerEmpresaTransporte(Id));
			}
			else
			{
				return Json(Datos.Datos.EmpresaTransporte.ObtenerEmpresasdeTransportes());
			}
		}
	}
}