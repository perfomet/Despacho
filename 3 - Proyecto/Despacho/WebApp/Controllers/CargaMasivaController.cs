using System.Collections.Generic;
using System.Web.Mvc;
using Datos.DB;
using System.Data;
using System.Text;
using System;

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
			return (Datos.Datos.Internos.ExisteContenido("Despacho","CargaMasivaDetalle", "NumeroSolicitud", Valor.ToString(), Datos.Datos.Internos.snumero));
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
		public JsonResult MakeCargaMasiva(Datos.Modelo.CargaMasiva cargamasiva)
		{
			string INSERTSentence = "INSERT INTO CargaMasiva (UsuarioId, FechaHora, Archivo)";
			string VALUESSentence = " VALUES({1}, '{2}', '{3}');";
			string SQLSentence = INSERTSentence + VALUESSentence;
			StringBuilder builder = new StringBuilder();
			builder.AppendFormat(SQLSentence, cargamasiva.UsuarioId, cargamasiva.FechaHora, cargamasiva.Archivo);
			DataBase.ExecuteNonQuery(builder.ToString());
						
			return Json(int.Parse(DataBase.ExecuteScalar("SELECT SCOPE_IDENTITY()").ToString()));
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
			bool ExisteNumeroSolicitud = false;

			detalles.ForEach((detalle) =>
			{
				ExisteNumeroSolicitud = false;
				// VALIDA NUMERO DE SOLICITUD
				if (Datos.Datos.Internos.IsNullOrEmpty(detalle.NumeroSolicitud.ToString()))
				{
					detalle.estados.Add(Datos.Modelo.EstadoCargaMasivaDetalle.tipoNumeroSolicitud);
				}
				else
				{
					ExisteNumeroSolicitud = TieneNumeroSolicitud(detalle.NumeroSolicitud);
					// VALIDA TIPO DE SOLICITUD
					if (detalle.TipoSolicitud == null)
					{
						detalle.estados.Add(Datos.Modelo.EstadoCargaMasivaDetalle.faltaTipoSolicitud);
						if (ExisteNumeroSolicitud)
						{
							detalle.TipoSolicitud = ValorCargaMasivaDetalle(detalle.NumeroSolicitud, "TipoSolicitud");
						}
						else
						{

						}
					}
					else
					{
						if (!Datos.Datos.Internos.ExisteContenido("Despacho", "TipoSolicitud", "Descripcion", detalle.TipoSolicitud, Datos.Datos.Internos.stexto))
						{

							detalle.estados.Add(Datos.Modelo.EstadoCargaMasivaDetalle.tipoSolicitud);
						}
						else
						{

						}
					}
					//VALIDA FECHA SOLICITUD Y FECHA RECEPCION
					if (detalle.FechaSolicitud == null)
					{
						detalle.estados.Add(Datos.Modelo.EstadoCargaMasivaDetalle.faltaFechaSolicitud);
						if (ExisteNumeroSolicitud)
						{
							detalle.FechaSolicitud = ValorCargaMasivaDetalle(detalle.NumeroSolicitud, "FechaSolicitud");
						}
						else
						{

						}
					}
					else
					{
						if (!Datos.Datos.Internos.IsDate(detalle.FechaSolicitud))
						{
							detalle.estados.Add(Datos.Modelo.EstadoCargaMasivaDetalle.tipoFechaSolicitud);
							if (ExisteNumeroSolicitud)
							{
								detalle.FechaSolicitud = ValorCargaMasivaDetalle(detalle.NumeroSolicitud, "FechaSolicitud");
							}
							else
							{
							}
						}
						else
						{
							if (detalle.FechaRecepcion == null)
							{
								detalle.estados.Add(Datos.Modelo.EstadoCargaMasivaDetalle.faltaFechaRecepcion);
								if (ExisteNumeroSolicitud)
								{
									detalle.FechaRecepcion = ValorCargaMasivaDetalle(detalle.NumeroSolicitud, "FechaRecepcion");
								}
								else
								{
								}
							}
							else
							{
								if (!Datos.Datos.Internos.IsDate(detalle.FechaRecepcion))
								{
									detalle.estados.Add(Datos.Modelo.EstadoCargaMasivaDetalle.tipoFechaRecepcion);
									if (ExisteNumeroSolicitud)
									{
										detalle.FechaRecepcion = ValorCargaMasivaDetalle(detalle.NumeroSolicitud, "FechaRecepcion");
									}
									else
									{
									}
								}
								else
								{
									if (Datos.Datos.Internos.RelacionFechaSolicitudFechaRecepcion(detalle.FechaRecepcion.ToString(), detalle.FechaSolicitud.ToString()))
									{
										detalle.estados.Add(Datos.Modelo.EstadoCargaMasivaDetalle.fechaRecepcionmenorFechaSolicitud);
									}
									else
									{

									}
								}
							}
						}
					}

					//VALIDA FECHA RECEPCION
					if (!Datos.Datos.Internos.IsDate(detalle.FechaRecepcion))
					{
						detalle.estados.Add(Datos.Modelo.EstadoCargaMasivaDetalle.tipoFechaRecepcion);
						if (ExisteNumeroSolicitud)
						{
							detalle.FechaRecepcion = ValorCargaMasivaDetalle(detalle.NumeroSolicitud, "FechaRecepcion");
						}
						else
						{
						}
					}
					else
					{

					}
					//VALIDA REGION
					if (detalle.RegionCliente == null)
					{
						detalle.estados.Add(Datos.Modelo.EstadoCargaMasivaDetalle.faltaRegionCliente);
						if (ExisteNumeroSolicitud)
						{
							detalle.RegionCliente = ValorCargaMasivaDetalle(detalle.NumeroSolicitud, "RegionCliente");
						}
						else
						{
						}
					}
					else
					{

					}
					if (detalle.RegionCliente == null)
					{
						detalle.estados.Add(Datos.Modelo.EstadoCargaMasivaDetalle.faltaRegionCliente);
						if (ExisteNumeroSolicitud)
						{
							detalle.RegionCliente = ValorCargaMasivaDetalle(detalle.NumeroSolicitud, "RegionCliente");
						}
						else
						{
						}
					}
					else
					{
					}
					if (detalle.RegionCliente == null)
					{

					}
					else
					{
						if (!Datos.Datos.Internos.ExisteContenido("Despacho", "Region", "Region", detalle.RegionCliente, Datos.Datos.Internos.stexto))
						{
							detalle.estados.Add(Datos.Modelo.EstadoCargaMasivaDetalle.tipoRegionCliente);
							if (ExisteNumeroSolicitud)
							{
								detalle.RegionCliente = ValorCargaMasivaDetalle(detalle.NumeroSolicitud, "RegionCliente");
							}
							else
							{
							}
						}
						else
						{
							if (!Datos.Datos.Internos.CorrespondeaRegion(detalle.ComunaCliente, detalle.RegionCliente))
							{
								detalle.estados.Add(Datos.Modelo.EstadoCargaMasivaDetalle.tipoComunaCliente);
								detalle.estados.Add(Datos.Modelo.EstadoCargaMasivaDetalle.tipoRegionCliente);

							}
						}
					}

					//VALIDA COMUNA
					if (detalle.ComunaCliente == null)
					{
						detalle.estados.Add(Datos.Modelo.EstadoCargaMasivaDetalle.faltaComunaCliente);
						if (ExisteNumeroSolicitud)
						{
							detalle.ComunaCliente = ValorCargaMasivaDetalle(detalle.NumeroSolicitud, "ComunaCliente");
						}
						else
						{

						}
					}
					else
					{
						if (!Datos.Datos.Internos.ExisteContenido("Despacho", "Comuna", "Comuna", detalle.ComunaCliente, Datos.Datos.Internos.stexto))
						{
							detalle.estados.Add(Datos.Modelo.EstadoCargaMasivaDetalle.tipoComunaCliente);
							if (ExisteNumeroSolicitud)
							{
								detalle.ComunaCliente = ValorCargaMasivaDetalle(detalle.NumeroSolicitud, "ComunaCliente");
							}
							else
							{
							}
						}
						else
						{

							if (!Datos.Datos.Internos.ExisteContenido("Despacho", "Region", "Region", detalle.RegionCliente, Datos.Datos.Internos.stexto))
							{
								if (!Datos.Datos.Internos.CorrespondeaRegion(detalle.ComunaCliente, detalle.RegionCliente))
								{
									detalle.estados.Add(Datos.Modelo.EstadoCargaMasivaDetalle.tipoComunaCliente);
									detalle.estados.Add(Datos.Modelo.EstadoCargaMasivaDetalle.tipoRegionCliente);
								}
								else
								{

								}
							}
							else
							{
								if (!Datos.Datos.Internos.CorrespondeaRegion(detalle.ComunaCliente, detalle.RegionCliente))
								{
									detalle.estados.Add(Datos.Modelo.EstadoCargaMasivaDetalle.tipoComunaCliente);
									detalle.estados.Add(Datos.Modelo.EstadoCargaMasivaDetalle.tipoRegionCliente);
								}
								else
								{

								}
							}
						}

					}
					//VALIDA NUMERO TELEFONO CONTACTO
					if (detalle.NumeroTelefonoContacto == null)
					{
						detalle.estados.Add(Datos.Modelo.EstadoCargaMasivaDetalle.faltaNumeroTelefonoContacto);
						if (ExisteNumeroSolicitud)
						{
							detalle.NumeroTelefonoContacto = ValorCargaMasivaDetalle(detalle.NumeroSolicitud, "NumeroTelefonoContacto");
						}
						else
						{
						}

					}
					else
					{
						if (!Datos.Datos.Internos.IsNumeric(detalle.NumeroTelefonoContacto))
						{
							detalle.estados.Add(Datos.Modelo.EstadoCargaMasivaDetalle.tipoNumeroTelefonoContacto);
							if (ExisteNumeroSolicitud)
							{
								detalle.NumeroTelefonoContacto = ValorCargaMasivaDetalle(detalle.NumeroSolicitud, "NumeroTelefonoContacto");
							}
							else
							{
							}
						}
						else
						{

						}
					}
					//VALIDA NUMERO TELEFONO CONTACTO ADICIONAL
					if (detalle.NumeroTelefonoContactoAdicional == null)
					{
						detalle.estados.Add(Datos.Modelo.EstadoCargaMasivaDetalle.faltaNumeroTelefonoContactoAdicional);
						if (ExisteNumeroSolicitud)
						{
							detalle.NumeroTelefonoContactoAdicional = ValorCargaMasivaDetalle(detalle.NumeroSolicitud, "NumeroTelefonoContactoAdicional");
						}
						else
						{
						}

					}
					else
					{
						if (!Datos.Datos.Internos.IsNumeric(detalle.NumeroTelefonoContactoAdicional))
						{
							detalle.estados.Add(Datos.Modelo.EstadoCargaMasivaDetalle.tipoNumeroTelefonoContactoAdicional);
							if (ExisteNumeroSolicitud)
							{
								detalle.NumeroTelefonoContacto = ValorCargaMasivaDetalle(detalle.NumeroSolicitud, "NumeroTelefonoContactoAdicional");
							}
							else
							{
							}
						}
						else
						{

						}
					}
					//VALIDA UNIDAD DE NEGOCIO
					if (detalle.UnidadNegocio == null)
					{
						detalle.estados.Add(Datos.Modelo.EstadoCargaMasivaDetalle.faltaUnidadNegocio);
						if (ExisteNumeroSolicitud)
						{
							detalle.UnidadNegocio = ValorCargaMasivaDetalle(detalle.NumeroSolicitud, "UnidadNegocio");
						}
						else
						{
						}
					}
					else
					{
						if (!Datos.Datos.Internos.ExisteContenido("Despacho", "UnidadNegocio", "Descripcion", detalle.UnidadNegocio, Datos.Datos.Internos.stexto))
						{
							detalle.estados.Add(Datos.Modelo.EstadoCargaMasivaDetalle.tipoUnidadNegocio);
							if (ExisteNumeroSolicitud)
							{
								detalle.UnidadNegocio = ValorCargaMasivaDetalle(detalle.NumeroSolicitud, "UnidadNegocio");
							}
							else
							{
							}
						}
					}
					//VALIDA GERENCIA
					if (detalle.Gerencia == null)
					{
						if (ExisteNumeroSolicitud)
						{
							detalle.Gerencia = ValorCargaMasivaDetalle(detalle.NumeroSolicitud, "Gerencia");
						}
						else
						{
						}
					}
					else
					{
						if (!Datos.Datos.Internos.ExisteContenido("Despacho", "Gerencia", "Descripcion", detalle.Gerencia, Datos.Datos.Internos.stexto))
						{
							detalle.estados.Add(Datos.Modelo.EstadoCargaMasivaDetalle.tipoGerencia);
							if (ExisteNumeroSolicitud)
							{
								detalle.Gerencia = ValorCargaMasivaDetalle(detalle.NumeroSolicitud, "Gerencia");
							}
							else
							{
							}
						}
					}
					//VALIDA OBSERVACIONAOF
					if (detalle.ObservacionAof == null)
					{
						detalle.estados.Add(Datos.Modelo.EstadoCargaMasivaDetalle.faltaObservacionAof);
						if (ExisteNumeroSolicitud)
						{
							detalle.ObservacionAof = ValorCargaMasivaDetalle(detalle.NumeroSolicitud, "ObservacionAof");
						}
						else
						{
						}

					}
					else
					{
					}

					//VALIDA PRIORIDAD
					if (detalle.Prioridad == null)
					{
						detalle.estados.Add(Datos.Modelo.EstadoCargaMasivaDetalle.faltaPrioridad);
						if (ExisteNumeroSolicitud)
						{
							detalle.Prioridad = ValorCargaMasivaDetalle(detalle.NumeroSolicitud, "Prioridad");
						}
						else
						{
						}
					}
					else
					{
						if (!Datos.Datos.Internos.ExisteContenido("Despacho", "Prioridad", "Descripcion", detalle.Prioridad, Datos.Datos.Internos.stexto))
						{
							detalle.estados.Add(Datos.Modelo.EstadoCargaMasivaDetalle.tipoPrioridad);
							if (ExisteNumeroSolicitud)
							{
								detalle.Prioridad = ValorCargaMasivaDetalle(detalle.NumeroSolicitud, "Prioridad");
							}
							else
							{
							}
						}
					}
					//VALIDA PLACA
					if (detalle.NumeroPlaca == null)
					{
						detalle.estados.Add(Datos.Modelo.EstadoCargaMasivaDetalle.faltaPlaca);
					}
					else
					{
						if (!Datos.Datos.Internos.ExisteContenido("MiLogistic", "Existencia", "Placa", detalle.NumeroPlaca, Datos.Datos.Internos.snumero))
						{
							detalle.estados.Add(Datos.Modelo.EstadoCargaMasivaDetalle.tipoPlaca);
						}
					}

				}

			}
			//Debo Guardar Registro
			//GuardaRegistro(detalles)
			); 

			return Json(detalles);
		}
	}
}
