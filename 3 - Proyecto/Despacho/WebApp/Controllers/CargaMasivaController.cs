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
		public ActionResult Create(Datos.Modelo.CargaMasiva cargamasiva, List<Datos.Modelo.CargaMasivaDetalle> detallecargamasiva, List<Datos.Modelo.CargaMasivaDetalle> cargaMasivaDetalle)
		{
			int idcargamasiva = Datos.Datos.CargaMasiva.Crear(cargamasiva);

			detallecargamasiva.ForEach((detalle) =>
			{
				detalle.CargaMasivaId = idcargamasiva;
				int detalleid = Datos.Datos.CargaMasivaDetalle.Crear(detalle);

				Datos.Modelo.Solicitud solicitud = new Datos.Modelo.Solicitud
				{
					TipoSolicitudId = Datos.Datos.TipoSolicitud.ObtenerTipoSolicitud(detalle.TipoSolicitud).Tiposolicitudid
				};

				int solicitudId = Datos.Datos.Solicitud.Crear(solicitud);

				List<Datos.Modelo.EquipoSolicitado> equipos = new List<Datos.Modelo.EquipoSolicitado>();

				cargaMasivaDetalle.ForEach((producto) =>
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

		public JsonResult Validar(List<Datos.Modelo.CargaMasivaDetalle> detalles)
		{
			detalles.ForEach((detalle) =>
			{
				if (Datos.Datos.Internos.Existe(detalle.NumeroSolicitud))
				{
					//YA EXISTE ESTA CAGADA
				}
				// VALIDA NUMERO DE SOLICITUD

				// VALIDA TIPO DE SOLICITUD
				if (!Datos.Datos.Internos.ExisteContenido("TipoSolicitud", "Descripcion", detalle.TipoSolicitud, Datos.Datos.Internos.stexto))
				{
					detalle.estados.Add(Datos.Modelo.EstadoCargaMasivaDetalle.tipoSolicitud);
				}
				else
				{
				}
				//
				//VALIDA REGION
				if (!Datos.Datos.Internos.ExisteContenido("Region", "Region", detalle.RegionCliente, Datos.Datos.Internos.stexto))
				{
					detalle.estados.Add(Datos.Modelo.EstadoCargaMasivaDetalle.tipoRegionCliente);
				}
				
				//VALIDA COMUNA
				if (!Datos.Datos.Internos.ExisteContenido("Comuna", "Comuna", detalle.ComunaCliente, Datos.Datos.Internos.stexto))
				{
					detalle.estados.Add(Datos.Modelo.EstadoCargaMasivaDetalle.tipoComunaCliente);
				}
				//VALIDA UNIDAD DE NEGOCIO
				if (!Datos.Datos.Internos.ExisteContenido("UnidadNegocio", "Descripcion", detalle.UnidadNegocio, Datos.Datos.Internos.stexto))
				{
					detalle.estados.Add(Datos.Modelo.EstadoCargaMasivaDetalle.tipoUnidadNegocio);
				}
				//VALIDA GERENCIA
				if (!Datos.Datos.Internos.ExisteContenido("Gerencia", "Descripcion", detalle.Gerencia, Datos.Datos.Internos.stexto))
				{
					detalle.estados.Add(Datos.Modelo.EstadoCargaMasivaDetalle.tipoGerencia);
				}
				//VALIDA PRIORIDAD
				if (!Datos.Datos.Internos.ExisteContenido("Prioridad", "Descripcion", detalle.Prioridad, Datos.Datos.Internos.stexto))
				{
					detalle.estados.Add(Datos.Modelo.EstadoCargaMasivaDetalle.tipoPrioridad);
				}
				//VALIDA PLACA
				if (!Datos.Datos.Internos.ExisteContenido("Existencia", "Placa", detalle.NumeroPlaca, Datos.Datos.Internos.snumero))
				{
					detalle.estados.Add(Datos.Modelo.EstadoCargaMasivaDetalle.tipoPlaca);
				}
			});

			return Json(detalles);
		}
	}
}
