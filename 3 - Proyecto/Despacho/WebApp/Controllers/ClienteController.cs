﻿using System.Web.Mvc;

namespace Despacho.Controllers
{
	public class ClienteController : Controller
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
			return View("Cliente", new Datos.Modelo.Cliente());
		}

		[HttpPost]
		public ActionResult Create(Datos.Modelo.Cliente cliente)
		{
			bool exito = Datos.Datos.Cliente.Crear(cliente);

			return Json(new { exito = exito });
		}

		public ActionResult Edit(int id)
		{
			Datos.Modelo.Cliente cliente = Datos.Datos.Cliente.ObtenerCliente(id);

			return View("Cliente", cliente);
		}

		[HttpPost]
		public ActionResult Edit(Datos.Modelo.Cliente cliente)
		{
			bool exito = Datos.Datos.Cliente.Modificar(cliente);

			return Json(new { exito = exito });
		}

		[HttpPost]
		public JsonResult EstaActivo(int id)
		{
			bool exito = Datos.Datos.Cliente.EstaActivo(id);

			return Json(new { exito = exito });
		}

		[HttpPost]
		public JsonResult Listar(int clienteId)
		{
			if (clienteId > 0)
			{
				return Json(Datos.Datos.Cliente.ObtenerCliente(clienteId));
			}
			else
			{
				return Json(Datos.Datos.Cliente.ObtenerClientes());
			}
		}
	}
}
