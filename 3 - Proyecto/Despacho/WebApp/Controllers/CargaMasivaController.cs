using Datos._librerias;
using Datos.DB;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
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

		public static bool TieneNumeroSolicitud(int Valor)
		{
			return (Datos.Datos.Internos.ExisteContenido("Despacho", "CargaMasivaDetalle", "NumeroSolicitud", Valor.ToString(), Datos.Datos.Internos.snumero));
		}

		public static string ValorCargaMasivaDetalle(int NumSolicitud, string NombreCampo)
		{
			string resultado = "";

			string SELECTSentence = "SELECT CargaMasivaDetalle." + NombreCampo;
			string FROMSentence = " FROM CargaMasivaDetalle";
			string WHERESentence = " WHERE CargaMasivaDetalle.NumeroSolicitud = " + NumSolicitud.ToString();
			string SQLSentence = SELECTSentence + FROMSentence + WHERESentence;
			DataTable datatable = DataBase.ExecuteReader(SQLSentence);
			foreach (DataRow fila in datatable.Rows)
			{
				resultado = (fila[0]).ToString();
			}

			return resultado;
		}

		[HttpPost]
		public JsonResult Create(Datos.Modelo.CargaMasiva cargaMasiva, List<Datos.Modelo.CargaMasivaDetalle> cargaMasivaDetalles)
		{
			Datos.Modelo.Usuario usuario = Session["usuario"] as Datos.Modelo.Usuario;

			cargaMasiva.FechaHora = DateTime.Now.ToString("dd/MM/yyyy HH:mm:ss");
			cargaMasiva.UsuarioId = usuario.UsuarioId;

			int cargaMasivaId = Datos.Datos.CargaMasiva.Crear(cargaMasiva);

			if (cargaMasivaId <= 0) return Json(new { exito = false });

			bool errorDetectado = false;

			int solicitudId = 0;

			cargaMasivaDetalles.ForEach((cargaMasivaDetalle) =>
			{
				cargaMasivaDetalle.CargaMasivaId = cargaMasivaId;
				int cargaMasivaDetalleId = Datos.Datos.CargaMasivaDetalle.Crear(cargaMasivaDetalle);

				errorDetectado = errorDetectado || cargaMasivaDetalleId <= 0;

				if (cargaMasivaDetalle.Errores != null)
				{
					cargaMasivaDetalle.Errores.ForEach((error) =>
					{
						error.CargaMasivaDetalleId = cargaMasivaDetalleId;
					});

					bool creados = Datos.Datos.CargaMasivaDetalleError.Crear(cargaMasivaDetalle.Errores);

					errorDetectado = errorDetectado || !creados;
				}

				if (cargaMasivaDetalle.Acciones != null && cargaMasivaDetalle.Acciones.Contains(1)) // SI TIENE LA ACCIÓN CREAR SOLICITUD
				{
					Datos.Modelo.TipoSolicitud tipoSolicitud = Datos.Datos.TipoSolicitud.ObtenerTipoSolicitud(cargaMasivaDetalle.TipoSolicitud);
					Datos.Modelo.Region region = Datos.Datos.Region.ObtenerRegion(cargaMasivaDetalle.RegionCliente);
					Datos.Modelo.Comuna comuna = Datos.Datos.Comuna.ObtenerComuna(cargaMasivaDetalle.ComunaCliente);
					Datos.Modelo.Prioridad prioridad = Datos.Datos.Prioridad.ObtenerPrioridad(cargaMasivaDetalle.Prioridad);
					Datos.Modelo.UnidadNegocio unidadNegocio = Datos.Datos.UnidadNegocio.ObtenerUnidadNegocio(cargaMasivaDetalle.UnidadNegocio);
					Datos.Modelo.Gerencia gerencia = Datos.Datos.Gerencia.ObtenerGerencia(cargaMasivaDetalle.Gerencia);
					string rut = cargaMasivaDetalle.RutCliente.Replace(".", "");

					Datos.Modelo.Solicitud solicitud = new Datos.Modelo.Solicitud
					{
						NumeroSolicitud = int.Parse(cargaMasivaDetalle.NumeroSolicitud),
						TipoSolicitudId = tipoSolicitud.Tiposolicitudid,
						EstadoSolicitudId = 1,
						FechaSolicitud = cargaMasivaDetalle.FechaSolicitud,
						FechaRecepcion = cargaMasivaDetalle.FechaRecepcion,
						NumeroCliente = cargaMasivaDetalle.NumeroCliente,
						NombreCliente = cargaMasivaDetalle.NombreCliente,
						CalleDireccionCliente = cargaMasivaDetalle.CalleDireccionCliente,
						NumeroDireccionCliente = int.Parse(cargaMasivaDetalle.NumeroDireccionCliente),
						RegionClienteId = region.RegionId,
						ComunaClienteId = comuna.ComunaId,
						NumeroTelefonoContacto = cargaMasivaDetalle.NumeroTelefonoContacto,
						NumeroTelefonoContactoAdicional = cargaMasivaDetalle.NumeroTelefonoContactoAdicional,
						RutCliente = rut.Split('-')[0],
						VRutCliente = rut.IndexOf('-') > 0 ? rut.Split('-')[1] : "",
						PrioridadId = prioridad.prioridadid,
						UnidadNegocioId = unidadNegocio.UnidadNegocioId,
						GerenciaId = gerencia.Gerenciaid,
						ObservacionAof = cargaMasivaDetalle.ObservacionAof,
						SolicitanteId = usuario.UsuarioId
					};

					solicitudId = Datos.Datos.Solicitud.Crear(solicitud);
				}

				errorDetectado = errorDetectado || solicitudId <= 0;

				bool equipoAgregado = false;

				if (cargaMasivaDetalle.Acciones != null && cargaMasivaDetalle.Acciones.Contains(2)) // SI TIENE LA ACCiÓN AGREGAR EQUIPO
				{
					Datos.Modelo.Existencia existencia = Datos.Datos.Existencia.ObtenerExistencia(cargaMasivaDetalle.NumeroPlaca);

					equipoAgregado = Datos.Datos.EquipoSolicitado.Crear(new Datos.Modelo.EquipoSolicitado
					{
						SolicitudDespachoId = solicitudId,
						NumeroPlaca = cargaMasivaDetalle.NumeroPlaca,
						Modelo = existencia.CodArt,
						EstadoEquipoId = 1
					});
				}

				errorDetectado = errorDetectado || !equipoAgregado;
			});

			if (errorDetectado) Json(new { exito = false });

			return Json(new { exito = true });
		}

		public ActionResult Edit(int id)
		{
			Datos.Modelo.Cliente cliente = Datos.Datos.Cliente.ObtenerCliente(id);

			return View("/CargaMasiva/Modificar", cliente);
		}

		[HttpPost]
		public JsonResult Edit(Datos.Modelo.Cliente cliente)
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
			return Json(Datos.Datos.CargaMasiva.ObtenerCargasMasivas());
		}

		[HttpPost]
		public JsonResult ValidaNumerosSolicitudes(List<Datos.Modelo.CargaMasivaDetalle> detalles)
		{
			List<string> numerosCreados = new List<string>();

			detalles.ForEach((detalle) =>
			{
				numerosCreados.Add(detalle.NumeroSolicitud);
			});
			return Json(numerosCreados);
		}

		[HttpPost]
		public JsonResult Validar(List<Datos.Modelo.CargaMasivaDetalle> detalles)
		{

			bool RegionExiste = false;
			bool ComunaExiste = false;
			List<string> placascreadas = new List<string>();

			bool PlacaDuplicada = false;
			detalles.ForEach((detalle) =>
			{
				if (detalle.Errores == null) detalle.Errores = new List<Datos.Modelo.CargaMasivaDetalleError>();
				if (detalle.Acciones == null) detalle.Acciones = new List<int>();
				//VALIDA NUMERO DE SOLICITUD
				//VALIDADO EN EL CLIENTE//

				/*
				if(detalle.Acciones.Any(a => a == 1) && NumeroSolicitudDuplicadoEnBD(detalle.NumeroSolicitud))
				{
					detalle.Errores.Add(numeroSolicitudRepetida);
				}
				*/

				// VALIDA TIPO DE SOLICITUD
				if (!Datos.Datos.Internos.ExisteContenido("Despacho", "TipoSolicitud", "Descripcion", detalle.TipoSolicitud, Datos.Datos.Internos.stexto))
				{
					detalle.Errores.Add(Datos.Modelo.CargaMasivaDetalleError.tipoSolicitud);
				}

				//VALIDA FECHA SOLICITUD Y FECHA RECEPCION
				//VALIDADAS EN EL CLIENTE

				//VALIDA REGION Y COMUNA
				RegionExiste = Datos.Datos.Internos.ExisteContenido("Despacho", "Region", "Region", detalle.RegionCliente, Datos.Datos.Internos.stexto);
				ComunaExiste = Datos.Datos.Internos.ExisteContenido("Despacho", "Comuna", "Comuna", detalle.ComunaCliente, Datos.Datos.Internos.stexto);

				if (RegionExiste && ComunaExiste)
				{
					if (!Datos.Datos.Internos.CorrespondeaRegion(detalle.ComunaCliente, detalle.RegionCliente))
					{
						detalle.Errores.Add(Datos.Modelo.CargaMasivaDetalleError.tipoRegionComunaClientenomatch);
					}
				}
				else
				{
					if (!RegionExiste && ComunaExiste)
					{
						detalle.Errores.Add(Datos.Modelo.CargaMasivaDetalleError.tipoRegionCliente);
					}
					else
					{
						detalle.Errores.Add(Datos.Modelo.CargaMasivaDetalleError.tipoComunaCliente);
					}
				}

				//VALIDA NUMERO TELEFONO CONTACTO
				//VALIDADO EN EL CLIENTE

				//VALIDA NUMERO TELEFONO CONTACTO ADICIONAL
				//VALIDA EN EL CLIENTE

				//VALIDA UNIDAD DE NEGOCIO
				if (!Datos.Datos.Internos.ExisteContenido("Despacho", "UnidadNegocio", "Descripcion", detalle.UnidadNegocio, Datos.Datos.Internos.stexto))
				{
					detalle.Errores.Add(Datos.Modelo.CargaMasivaDetalleError.tipoUnidadNegocio);
				}

				//VALIDA GERENCIA
				if (!Datos.Datos.Internos.ExisteContenido("Despacho", "Gerencia", "Descripcion", detalle.Gerencia, Datos.Datos.Internos.stexto))
				{
					detalle.Errores.Add(Datos.Modelo.CargaMasivaDetalleError.tipoGerencia);
				}
				//VALIDA OBSERVACIONAOF
				//VALIDADO EN EL CLIENTE

				//VALIDA PRIORIDAD
				if (!Datos.Datos.Internos.ExisteContenido("Despacho", "Prioridad", "Descripcion", detalle.Prioridad, Datos.Datos.Internos.stexto))
				{
					detalle.Errores.Add(Datos.Modelo.CargaMasivaDetalleError.tipoPrioridad);
				}

				//VALIDA PLACA

				if (!Datos.Datos.Internos.ExisteContenido(LibConfig.DbExistencias, LibConfig.TablaExistencias, "Serie", detalle.NumeroPlaca, Datos.Datos.Internos.stexto))
				{
					detalle.Errores.Add(Datos.Modelo.CargaMasivaDetalleError.tipoPlaca);
				}
				else
				{
					//VALIDAR PLACA REPETIDA
					PlacaDuplicada = placascreadas.Contains(detalle.NumeroPlaca);
					if (PlacaDuplicada)
					{
						detalle.Errores.Add(Datos.Modelo.CargaMasivaDetalleError.numeroPlacaRepetida);
					}
					else
					{
						placascreadas.Add(detalle.NumeroPlaca);
					}

				}
			}

			);

			return Json(detalles);
		}
	}
}
