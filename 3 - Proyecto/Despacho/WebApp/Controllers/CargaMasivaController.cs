﻿using Datos.DB;
using System.Collections.Generic;
using System.Data;
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
		public JsonResult Create(Datos.Modelo.CargaMasiva cargamasiva, List<Datos.Modelo.CargaMasivaDetalle> detallecargamasiva)
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
			if (clienteId > 0)
			{
				return Json(Datos.Datos.Cliente.ObtenerCliente(clienteId));
			}
			else
			{
				return Json(Datos.Datos.Cliente.ObtenerClientes());
			}
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
				if (!Datos.Datos.Internos.ExisteContenido("MiLogistic", "Existencia", "Placa", detalle.NumeroPlaca, Datos.Datos.Internos.snumero))
				{
					detalle.Errores.Add(Datos.Modelo.CargaMasivaDetalleError.tipoPlaca);
				}
				else
				{
					//tarea
					//VALIDAR PLACA REPETIDA
				}
			}
			
			//Debo Guardar Registro
			//GuardaRegistro(detalles)
			);

			return Json(detalles);
		}
	}
}
