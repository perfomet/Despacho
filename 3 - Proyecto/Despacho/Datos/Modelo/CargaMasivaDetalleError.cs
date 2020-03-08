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

		public static CargaMasivaDetalleError faltaNumeroSolicitud = new CargaMasivaDetalleError
		{
			id = 1,
			title = msgfalta + " 'NumeroSolicitud'",
			clase = "m-badge--danger",
			tipo = "danger"
		};
		public static CargaMasivaDetalleError faltaTipoSolicitud = new CargaMasivaDetalleError
		{
			id = 2,
			title = msgfalta + " 'TipoSolicitud'",
			clase = "m-badge--danger",
			tipo = "danger"
		};
		public static CargaMasivaDetalleError faltaFechaSolicitud = new CargaMasivaDetalleError
		{
			id = 3,
			title = msgfalta + " 'FechaSolicitud'",
			clase = "m-badge--danger",
			tipo = "danger"
		};
		public static CargaMasivaDetalleError faltaFechaRecepcion = new CargaMasivaDetalleError
		{
			id = 4,
			title = msgfalta + " 'FechaRecepcion'",
			clase = "m-badge--danger",
			tipo = "danger"
		};
		public static CargaMasivaDetalleError faltaNumeroCliente = new CargaMasivaDetalleError
		{
			id = 5,
			title = msgfalta + " 'NumeroCliente'",
			clase = "m-badge--danger",
			tipo = "danger"
		};
		public static CargaMasivaDetalleError faltaNombreCliente = new CargaMasivaDetalleError
		{
			id = 6,
			title = msgfalta + " 'NombreCliente'",
			clase = "m-badge--danger",
			tipo = "danger"
		};
		public static CargaMasivaDetalleError faltaCalleDireccionCliente = new CargaMasivaDetalleError
		{
			id = 7,
			title = msgfalta + " 'CalleDireccionCliente'",
			clase = "m-badge--danger",
			tipo = "danger"
		};
		public static CargaMasivaDetalleError faltaNumeroDireccionCliente = new CargaMasivaDetalleError
		{
			id = 8,
			title = msgfalta + " 'NumeroDireccionCliente'",
			clase = "m-badge--danger",
			tipo = "danger"
		};
		public static CargaMasivaDetalleError faltaRegionCliente = new CargaMasivaDetalleError
		{
			id = 9,
			title = msgfalta + " 'RegionCliente'",
			clase = "m-badge--danger",
			tipo = "danger"
		};
		public static CargaMasivaDetalleError faltaComunaCliente = new CargaMasivaDetalleError
		{
			id = 10,
			title = msgfalta + " 'ComunaCliente'",
			clase = "m-badge--danger",
			tipo = "danger"
		};
		public static CargaMasivaDetalleError faltaNumeroTelefonoContacto = new CargaMasivaDetalleError
		{
			id = 11,
			title = msgfalta + "'NumeroTelefonoContacto'",
			clase = "m-badge--danger",
			tipo = "danger"
		};
		public static CargaMasivaDetalleError faltaNumeroTelefonoContactoAdicional = new CargaMasivaDetalleError
		{
			id = 12,
			title = msgfalta + " 'NumeroTelefonoContactoAdicional'",
			clase = "m-badge--danger",
			tipo = "danger"
		};
		public static CargaMasivaDetalleError faltaRUTCliente = new CargaMasivaDetalleError
		{
			id = 13,
			title = msgfalta + " 'RUTCliente'",
			clase = "m-badge--danger",
			tipo = "danger"
		};
		public static CargaMasivaDetalleError faltaUnidadNegocio = new CargaMasivaDetalleError
		{
			id = 14,
			title = msgfalta + " 'UnidadNegocio'",
			clase = "m-badge--danger",
			tipo = "danger"
		};
		public static CargaMasivaDetalleError faltaGerencia = new CargaMasivaDetalleError
		{
			id = 15,
			title = msgfalta + " 'Gerencia'",
			clase = "m-badge--danger",
			tipo = "danger"
		};
		public static CargaMasivaDetalleError faltaObservacionAof = new CargaMasivaDetalleError
		{
			id = 16,
			title = msgfalta + " 'ObservacionAof'",
			clase = "m-badge--danger",
			tipo = "danger"
		};
		public static CargaMasivaDetalleError faltaPrioridad = new CargaMasivaDetalleError
		{
			id = 17,
			title = msgfalta + " 'Prioridad'",
			clase = "m-badge--danger",
			tipo = "danger"
		};
		public static CargaMasivaDetalleError faltaPlaca = new CargaMasivaDetalleError
		{
			id = 18,
			title = msgfalta + " 'Placa'",
			clase = "m-badge--danger",
			tipo = "danger"
		};

		public static CargaMasivaDetalleError tipoNumeroSolicitud = new CargaMasivaDetalleError
		{
			id = 19,
			title = "Número de Solicitud incorrecta (debe ser numérica)",
			clase = "m-badge--danger",
			tipo = "danger"
		};

		public static CargaMasivaDetalleError tipoSolicitud = new CargaMasivaDetalleError
		{
			id = 20,
			title = "Tipo de Solicitud incorrecta",
			clase = "m-badge--danger",
			tipo = "danger"
		};
		public static CargaMasivaDetalleError tipoFechaSolicitud = new CargaMasivaDetalleError
		{
			id = 21,
			title = "Fecha de Solicitud incorrecta",
			clase = "m-badge--danger",
			tipo = "danger"
		};
		public static CargaMasivaDetalleError tipoFechaRecepcion = new CargaMasivaDetalleError
		{
			id = 22,
			title = "Fecha de Recepción incorrecta",
			clase = "m-badge--danger",
			tipo = "danger"
		};
		public static CargaMasivaDetalleError tipoNumeroCliente = new CargaMasivaDetalleError
		{
			id = 23,
			title = "Número Cliente incorrecto",
			clase = "m-badge--danger",
			tipo = "danger"
		};
		public static CargaMasivaDetalleError tipoNombreCliente = new CargaMasivaDetalleError
		{
			id = 24,
			title = "Nombre Cliente incorrecto",
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
		public static CargaMasivaDetalleError tipoNumeroTelefonoContacto = new CargaMasivaDetalleError
		{
			id = 27,
			title = "El Teléfono de Contacto " + msgerror,
			clase = "m-badge--danger",
			tipo = "danger"
		};
		public static CargaMasivaDetalleError tipoNumeroTelefonoContactoAdicional = new CargaMasivaDetalleError
		{
			id = 28,
			title = "El Teléfono de Contacto Adicional " + msgerror,
			clase = "m-badge--danger",
			tipo = "danger"
		};
		public static CargaMasivaDetalleError rutInvalido = new CargaMasivaDetalleError
		{
			id = 29,
			title = "El RUT ingresado no es válido",
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

		public static CargaMasivaDetalleError fechaRecepcionmenorFechaSolicitud = new CargaMasivaDetalleError
		{
			id = 34,
			title = "La Fecha de Solicitud es mayor que la Fecha de Recepción",
			clase = "m-badge--danger",
			tipo = "danger"
		};

	}
}
