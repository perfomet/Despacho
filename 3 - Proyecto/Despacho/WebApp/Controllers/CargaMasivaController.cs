using System.Collections.Generic;
using System.Web.Mvc;

namespace Despacho.Controllers
{
	public class CargaMasivaController : Controller
	{
		public ActionResult Index()
		{
			return View();
		}

		public ActionResult CargaMasiva()
		{
			return View();
		}

		public ActionResult Details(int id)
		{
			return View();
		}

		public ActionResult Create()
		{
			return View("/CargaMasiva/Modificar", new { });
		}

		[HttpPost]
		public ActionResult Create(Datos.Modelo.CargaMasiva cargamasiva, List<Datos.Modelo.CargaMasivaDetalle> detallecargamasiva, List<Datos.Modelo.CargaMasivaDetalleProducto> cargaMasivaDetalleProductos)
		{
			int idcargamasiva = Datos.Datos.CargaMasiva.Crear(cargamasiva);
			
			detallecargamasiva.ForEach((detalle) =>
			{
				detalle.CargaMasivaId = idcargamasiva;
				int detalleid = Datos.Datos.CargaMasivaDetalle.Crear(detalle);

				Datos.Modelo.Solicitud solicitud = new Datos.Modelo.Solicitud
				{
					//TipoSolicitudId = Datos.Datos.TipoSolicitud.ObtenerTipoSolicitud(detalle.TipoSolicitud).Tiposolicitudid
				};

				int solicitudId = Datos.Datos.Solicitud.Crear(solicitud);

				List<Datos.Modelo.EquipoSolicitado> equipos = new List<Datos.Modelo.EquipoSolicitado>();

				cargaMasivaDetalleProductos.ForEach((producto) =>
				{
					if (producto.NumeroSolicitud == detalle.NumeroSolicitud)
					{
						producto.CargaMasivaDetalleId = detalleid;
						equipos.Add(new Datos.Modelo.EquipoSolicitado
						{
							NumeroPlaca = producto.NumeroPlaca,
							SolicitudDespachoId = solicitudId
						});
					}
				});

				//INSERTAS LOS DETALLE PRODUCTO
				//INSERTAR EQUIPOS SOLICITADOS
				Datos.Datos.EquipoSolicitado.Crear(equipos);
			});

			return Json(new { exito = idcargamasiva > 0 });
		}

		public ActionResult Edit(int id)
		{
			Datos.Modelo.Cliente cliente = Datos.Datos.Cliente.ObtenerCliente(id);

			return View("/CargaMasiva/Modificar", cliente);
		}

		[HttpPost]
		public ActionResult Edit(Datos.Modelo.Cliente cliente)
		{
			bool exito = Datos.Datos.Cliente.Modificar(cliente);

			return Json(new { exito = exito });
		}

		[HttpPost]
		public JsonResult Delete(int id)
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
