namespace Datos.Modelo
{
	public class CargaMasivaDetalleError
	{
		static string msgerror = "no existe o incorrecta";
		static string msgfalta = "Falta el campo ";
		public int id { get; set; }
		public string title { get; set; }
		public string clase { get; set; }
		public string tipo { get; set; }

		public static CargaMasivaDetalleError tipoSolicitud = new CargaMasivaDetalleError
		{
			id = 20,
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

		public static CargaMasivaDetalleError tipoUnidadNegocio = new CargaMasivaDetalleError
		{
			id = 30,
			title = "Unidad de Negocio " + msgerror,
			clase = "m-badge--danger",
			tipo = "danger"
		};

		public static CargaMasivaDetalleError tipoGerencia = new CargaMasivaDetalleError
		{
			id = 31,
			title = "Gerencia ingresada " + msgerror,
			clase = "m-badge--danger",
			tipo = "danger"
		};

		public static CargaMasivaDetalleError tipoPrioridad = new CargaMasivaDetalleError
		{
			id = 32,
			title = "Prioridad " + msgerror,
			clase = "m-badge--danger",
			tipo = "danger"
		};

		public static CargaMasivaDetalleError tipoPlaca = new CargaMasivaDetalleError
		{
			id = 33,
			title = "Placa " + msgerror,
			clase = "m-badge--danger",
			tipo = "danger"
		};
	}
}
