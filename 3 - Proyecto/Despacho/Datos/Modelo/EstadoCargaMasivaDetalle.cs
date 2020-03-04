using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Datos.Modelo
{
	public class EstadoCargaMasivaDetalle
	{
    static string msgerror = "no existe o incorrecta";
    static string msgfalta = "Falta el campo ";
    public int id { get; set; }
		public string title { get; set; }
		public string clase { get; set; }
		public string tipo { get; set; }

    public static EstadoCargaMasivaDetalle faltaNumeroSolicitud = new EstadoCargaMasivaDetalle
    {
      id = 1,
      title = msgfalta + "'NumeroSolicitud'",
      clase = "m-badge--danger",
      tipo = "danger"
    };
    public static EstadoCargaMasivaDetalle faltaTipoSolicitud = new EstadoCargaMasivaDetalle
    {
      id = 2,
      title = msgfalta + "'TipoSolicitud'",
      clase = "m-badge--danger",
      tipo = "danger"
    };
    public static EstadoCargaMasivaDetalle faltaFechaSolicitud = new EstadoCargaMasivaDetalle
    {
      id = 3,
      title = msgfalta + "'FechaSolicitud'",
      clase = "m-badge--danger",
      tipo = "danger"
    };
    public static EstadoCargaMasivaDetalle faltaFechaRecepcion = new EstadoCargaMasivaDetalle
    {
      id = 4,
      title = msgfalta + "'FechaRecepcion'",
      clase = "m-badge--danger",
      tipo = "danger"
    };
    public static EstadoCargaMasivaDetalle faltaNumeroCliente = new EstadoCargaMasivaDetalle
    {
      id = 5,
      title = msgfalta + "'NumeroCliente'",
      clase = "m-badge--danger",
      tipo = "danger"
    };
    public static EstadoCargaMasivaDetalle faltaNombreCliente = new EstadoCargaMasivaDetalle
    {
      id = 6,
      title = msgfalta + " 'NombreCliente'",
      clase = "m-badge--danger",
      tipo = "danger"
    };
    public static EstadoCargaMasivaDetalle faltaCalleDireccionCliente = new EstadoCargaMasivaDetalle
    {
      id = 7,
      title = msgfalta + "'CalleDireccionCliente'",
      clase = "m-badge--danger",
      tipo = "danger"
    };
    public static EstadoCargaMasivaDetalle faltaNumeroDireccionCliente = new EstadoCargaMasivaDetalle
    {
      id = 8,
      title = msgfalta + "'NumeroDireccionCliente'",
      clase = "m-badge--danger",
      tipo = "danger"
    };
    public static EstadoCargaMasivaDetalle faltaRegionCliente = new EstadoCargaMasivaDetalle
    {
      id = 9,
      title = msgfalta + "'RegionCliente'",
      clase = "m-badge--danger",
      tipo = "danger"
    };
    public static EstadoCargaMasivaDetalle faltaComunaCliente = new EstadoCargaMasivaDetalle
    {
      id = 10,
      title = msgfalta + " 'ComunaCliente'",
      clase = "m-badge--danger",
      tipo = "danger"
    };
    public static EstadoCargaMasivaDetalle faltaNumeroTelefonoContacto = new EstadoCargaMasivaDetalle
    {
      id = 11,
      title = msgfalta + "'NumeroTelefonoContacto'",
      clase = "m-badge--danger",
      tipo = "danger"
    };
    public static EstadoCargaMasivaDetalle faltaNumeroTelefonoContactoAdicional = new EstadoCargaMasivaDetalle
    {
      id = 12,
      title = msgfalta + "'NumeroTelefonoContactoAdicional'",
      clase = "m-badge--danger",
      tipo = "danger"
    };
    public static EstadoCargaMasivaDetalle faltaRUTCliente = new EstadoCargaMasivaDetalle
    {
      id = 13,
      title = msgfalta + "'RUTCliente'",
      clase = "m-badge--danger",
      tipo = "danger"
    };
    public static EstadoCargaMasivaDetalle faltaUnidadNegocio = new EstadoCargaMasivaDetalle
    {
      id = 14,
      title = msgfalta + "'UnidadNegocio'",
      clase = "m-badge--danger",
      tipo = "danger"
    };
    public static EstadoCargaMasivaDetalle faltaGerencia = new EstadoCargaMasivaDetalle
    {
      id = 15,
      title = msgfalta + "'Gerencia'",
      clase = "m-badge--danger",
      tipo = "danger"
    };
    public static EstadoCargaMasivaDetalle faltaObservacionAof = new EstadoCargaMasivaDetalle
    {
      id = 16,
      title = msgfalta + "'ObservacionAof'",
      clase = "m-badge--danger",
      tipo = "danger"
    };
    public static EstadoCargaMasivaDetalle faltaPrioridad = new EstadoCargaMasivaDetalle
    {
      id = 17,
      title = msgfalta + "'Prioridad'",
      clase = "m-badge--danger",
      tipo = "danger"
    };
    public static EstadoCargaMasivaDetalle faltaPlaca = new EstadoCargaMasivaDetalle
    {
      id = 18,
      title = msgfalta + "'Placa'",
      clase = "m-badge--danger",
      tipo = "danger"
    };
        
    public static EstadoCargaMasivaDetalle tipoNumeroSolicitud = new EstadoCargaMasivaDetalle
    {
      id = 19,
      title = "Número de Solicitud incorrecta (debe ser numérica)",
      clase = "m-badge--danger",
      tipo = "danger"
    };

    public static EstadoCargaMasivaDetalle tipoSolicitud = new EstadoCargaMasivaDetalle
    {
      id = 20,
      title = "Tipo de Solicitud incorrecta",
      clase = "m-badge--danger",
      tipo = "danger"
    };
    public static EstadoCargaMasivaDetalle tipoFechaSolicitud = new EstadoCargaMasivaDetalle
    {
      id = 21,
      title = "Fecha de Solicitud incorrecta",
      clase = "m-badge--danger",
      tipo = "danger"
    };
    public static EstadoCargaMasivaDetalle tipoFechaRecepcion = new EstadoCargaMasivaDetalle
    {
      id = 22,
      title = "Fecha de Recepción incorrecta",
      clase = "m-badge--danger",
      tipo = "danger"
    };
    public static EstadoCargaMasivaDetalle tipoNumeroCliente = new EstadoCargaMasivaDetalle
    {
      id = 23,
      title = "Número Cliente incorrecto",
      clase = "m-badge--danger",
      tipo = "danger"
    };
    public static EstadoCargaMasivaDetalle tipoNombreCliente = new EstadoCargaMasivaDetalle
    {
      id = 24,
      title = "Nombre Cliente incorrecto",
      clase = "m-badge--danger",
      tipo = "danger"
    };
    
    public static EstadoCargaMasivaDetalle tipoRegionCliente = new EstadoCargaMasivaDetalle
    {
      id = 25,
      title = "Nombre Región " + msgerror,
      clase = "m-badge--danger",
      tipo = "danger"
    };
    public static EstadoCargaMasivaDetalle tipoComunaCliente = new EstadoCargaMasivaDetalle
    {
      id = 26,
      title = "La Comuna ingresada " + msgerror,
      clase = "m-badge--danger",
      tipo = "danger"
    };
    public static EstadoCargaMasivaDetalle tipoNumeroTelefonoContacto = new EstadoCargaMasivaDetalle
    {
      id = 27,
      title = "El Teléfono de Contacto " + msgerror,
      clase = "m-badge--danger",
      tipo = "danger"
    };
    public static EstadoCargaMasivaDetalle tipoNumeroTelefonoContactoAdicional = new EstadoCargaMasivaDetalle
    {
      id = 28,
      title = "El Teléfono de Contacto Adicional " + msgerror,
      clase = "m-badge--danger",
      tipo = "danger"
    };
    public static EstadoCargaMasivaDetalle rutInvalido = new EstadoCargaMasivaDetalle
    {
      id = 29,
      title = "El RUT ingresado no es válido",
      clase = "m-badge--danger",
      tipo = "danger"
    };
    public static EstadoCargaMasivaDetalle tipoUnidadNegocio = new EstadoCargaMasivaDetalle
    {
      id = 30,
      title = "Unidad de Negocio " + msgerror,
      clase = "m-badge--danger",
      tipo = "danger"
    };
    public static EstadoCargaMasivaDetalle tipoGerencia = new EstadoCargaMasivaDetalle
    {
      id = 31,
      title = "Gerencia ingresada " + msgerror,
      clase = "m-badge--danger",
      tipo = "danger"
    };
    
    public static EstadoCargaMasivaDetalle tipoPrioridad = new EstadoCargaMasivaDetalle
    {
      id = 32,
      title = "Prioridad " + msgerror,
      clase = "m-badge--danger",
      tipo = "danger"
    };
    public static EstadoCargaMasivaDetalle tipoPlaca = new EstadoCargaMasivaDetalle
    {
      id = 33,
      title = "Placa " + msgerror,
      clase = "m-badge--danger",
      tipo = "danger"
    };

    public static EstadoCargaMasivaDetalle fechaRecepcionmenorFechaSolicitud = new EstadoCargaMasivaDetalle
    {
      id = 34,
      title = "La Fecha de Solicitud es mayor que la Fecha de Recepción",
      clase = "m-badge--danger",
      tipo = "danger"
    };

}
}
