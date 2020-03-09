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
		public JsonResult Create(Datos.Modelo.CargaMasiva cargamasiva, List<Datos.Modelo.CargaMasivaDetalle> registros)
		{
			Datos.Modelo.Usuario usuario = Session["usuario"] as Datos.Modelo.Usuario;

			cargamasiva.FechaHora = DateTime.Now.ToString("dd/MM/yyyy HH:mm:ss");
			cargamasiva.UsuarioId = usuario.UsuarioId;

			int idcargamasiva = Datos.Datos.CargaMasiva.Crear(cargamasiva);

			registros.ForEach((registro) =>
			{
				registro.CargaMasivaId = idcargamasiva;
				int detalleid = Datos.Datos.CargaMasivaDetalle.Crear(registro);

				if (registro.Errores != null)
				{
					registro.Errores.ForEach((error) =>
					{
						error.CargaMasivaDetalleId = detalleid;
					});

					Datos.Datos.CargaMasivaDetalleError.Crear(registro.Errores);
				}
			});

			List<Datos.Modelo.CargaMasivaDetalle> paraCrear = registros.Where(r => r.Acciones != null && r.Acciones.Any(a => a == 1)).ToList();

			if (paraCrear != null && paraCrear.Count > 0)
			{
				paraCrear.ForEach((registro) =>
				{
					Datos.Modelo.TipoSolicitud tipoSolicitud = Datos.Datos.TipoSolicitud.ObtenerTipoSolicitud(registro.TipoSolicitud);
					Datos.Modelo.Region region = Datos.Datos.Region.ObtenerRegion(registro.RegionCliente);
					Datos.Modelo.Comuna comuna = Datos.Datos.Comuna.ObtenerComuna(registro.ComunaCliente);
					Datos.Modelo.Prioridad prioridad = Datos.Datos.Prioridad.ObtenerPrioridad(registro.Prioridad);
					Datos.Modelo.UnidadNegocio unidadNegocio = Datos.Datos.UnidadNegocio.ObtenerUnidadNegocio(registro.UnidadNegocio);
					Datos.Modelo.Gerencia gerencia = Datos.Datos.Gerencia.ObtenerGerencia(registro.Gerencia);
					string rut = registro.RutCliente.Replace(".", "");

					Datos.Modelo.Solicitud solicitud = new Datos.Modelo.Solicitud
					{
						NumeroSolicitud = int.Parse(registro.NumeroSolicitud),
						TipoSolicitudId = tipoSolicitud.Tiposolicitudid,
						EstadoSolicitudId = 1,
						FechaSolicitud = registro.FechaSolicitud,
						FechaRecepcion = registro.FechaRecepcion,
						NumeroCliente = registro.NumeroCliente,
						NombreCliente = registro.NombreCliente,
						CalleDireccionCliente = registro.CalleDireccionCliente,
						NumeroDireccionCliente = int.Parse(registro.NumeroDireccionCliente),
						RegionClienteId = region.RegionId,
						ComunaClienteId = comuna.ComunaId,
						NumeroTelefonoContacto = registro.NumeroTelefonoContacto,
						NumeroTelefonoContactoAdicional = registro.NumeroTelefonoContactoAdicional,
						RutCliente = rut.Split('-')[0],
						VRutCliente = rut.IndexOf('-') > 0 ? rut.Split('-')[1] : "",
						PrioridadId = prioridad.prioridadid,
						UnidadNegocioId = unidadNegocio.UnidadNegocioId,
						GerenciaId = gerencia.Gerenciaid,
						ObservacionAof = registro.ObservacionAof,
						SolicitanteId = usuario.UsuarioId
					};

					int solicitudId = Datos.Datos.Solicitud.Crear(solicitud);

					Datos.Modelo.Existencia existencia = Datos.Datos.Existencia.ObtenerExistencia(registro.NumeroPlaca);

					List<Datos.Modelo.EquipoSolicitado> solicitados = new List<Datos.Modelo.EquipoSolicitado> {
						new Datos.Modelo.EquipoSolicitado
						{
							SolicitudDespachoId = solicitudId,
							NumeroPlaca = registro.NumeroPlaca,
							Modelo = existencia.CodArt
						}
					};

					registros.Where(r => r.Acciones != null && r.Acciones.Any(a => a == 2) && r.NumeroSolicitud == registro.NumeroSolicitud).ToList().ForEach((reg) =>
									{
										Datos.Modelo.Existencia e = Datos.Datos.Existencia.ObtenerExistencia(reg.NumeroPlaca);

										solicitados.Add(new Datos.Modelo.EquipoSolicitado
										{
											SolicitudDespachoId = solicitudId,
											NumeroPlaca = reg.NumeroPlaca,
											Modelo = e.CodArt

										});
									});

					Datos.Datos.EquipoSolicitado.Crear(solicitados);
				});
			}

			return Json(new { exito = idcargamasiva > 0 });
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

				if (!Datos.Datos.Internos.ExisteContenido("MiLogistic", "Existencia", "Serie", detalle.NumeroPlaca, Datos.Datos.Internos.stexto))
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
