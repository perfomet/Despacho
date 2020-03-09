using System.Data;

namespace Datos.Modelo
{
	public class CargaMasivaDetalleError
	{
		static string msgerror = "no existe o incorrecta";

		public int id { get; set; }
		public string title { get; set; }
		public string clase { get; set; }
		public string tipo { get; set; }
		public int CargaMasivaDetalleId { get; set; }

		public void FromDataRow(DataRow fila)
		{
			//CAMBIO DE PRUEBA
			this.id = int.Parse(fila[0].ToString());
			this.title = fila[1].ToString();
			this.clase = fila[3].ToString();
			this.tipo = fila[3].ToString();
			this.CargaMasivaDetalleId = int.Parse(fila[4].ToString());
		}

		public static CargaMasivaDetalleError tipoSolicitud = new CargaMasivaDetalleError
		{
			id = 24,
			title = "Tipo de Solicitud incorrecta",
			clase = "m-badge--danger",
			tipo = "danger"
		};

		public static CargaMasivaDetalleError tipoRegionCliente = new CargaMasivaDetalleError
		{
			id = 25,
			title = "Nombre Región " + msgerror,
			clase = "m-badge--danger",
			tipo = "danger"
		};

		public static CargaMasivaDetalleError tipoComunaCliente = new CargaMasivaDetalleError
		{
			id = 26,
			title = "La Comuna ingresada " + msgerror,
			clase = "m-badge--danger",
			tipo = "danger"
		};

		public static CargaMasivaDetalleError tipoRegionComunaClientenomatch = new CargaMasivaDetalleError
		{
			id = 27,
			title = "La Comuna ingresada no corresponde con la Región seleccionada",
			clase = "m-badge--danger",
			tipo = "danger"
		};

		public static CargaMasivaDetalleError tipoUnidadNegocio = new CargaMasivaDetalleError
		{
			id = 28,
			title = "Unidad de Negocio " + msgerror,
			clase = "m-badge--danger",
			tipo = "danger"
		};

		public static CargaMasivaDetalleError tipoGerencia = new CargaMasivaDetalleError
		{
			id = 29,
			title = "Gerencia ingresada " + msgerror,
			clase = "m-badge--danger",
			tipo = "danger"
		};

		public static CargaMasivaDetalleError tipoPrioridad = new CargaMasivaDetalleError
		{
			id = 30,
			title = "Prioridad " + msgerror,
			clase = "m-badge--danger",
			tipo = "danger"
		};

		public static CargaMasivaDetalleError tipoPlaca = new CargaMasivaDetalleError
		{
			id = 31,
			title = "Placa " + msgerror,
			clase = "m-badge--danger",
			tipo = "danger"
		};
		public static CargaMasivaDetalleError numeroSolicitudRepetida = new CargaMasivaDetalleError
		{
			id = 32,
			title = "Número de Solicitud repetida " + msgerror,
			clase = "m-badge--danger",
			tipo = "danger"
		};
	}
}
