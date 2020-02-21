using Datos.DB;
using System.Collections.Generic;
using System.Data;
using System.Text;

namespace Datos.Datos
{
	public class Solicitud
	{

		public static List<Modelo.Solicitud> ObtenerSolicitudes(int clienteId)
		{
			string SELECTSentence = "SELECT *";
			string FROMSentence = " FROM SolicitudDespacho";
			string WHERESentence = clienteId > 0 ? (" WHERE C.ClienteId = " + clienteId) : "";
			string ORDERSentence = ";";
			string SQLSentence = SELECTSentence + FROMSentence + WHERESentence + ORDERSentence;

			DataTable dataTable = DataBase.ExecuteReader(SQLSentence);
			List<Modelo.Solicitud> solicitudes = new List<Modelo.Solicitud>();

			foreach (DataRow fila in dataTable.Rows)
			{
				Modelo.Solicitud solicitud = new Modelo.Solicitud();
				solicitud.FromDataRow(fila);
				solicitudes.Add(solicitud);
			}

			return solicitudes;
		}

		public static Modelo.Solicitud ObtenerSolicitud(int id)
		{

			string SELECTSentence = "SELECT *";
			string FROMSentence = " FROM SolicitudDespacho";
			string WHERESentence = " WHERE SolicitudDespachoId = " + id;
			string ORDERSentence = ";";
			string SQLSentence = SELECTSentence + FROMSentence + WHERESentence + ORDERSentence;

			Modelo.Solicitud solicitud = new Modelo.Solicitud();
			DataTable dataTable = DataBase.ExecuteReader(SQLSentence);

			if (dataTable.Rows.Count > 0)
			{
				DataRow fila = dataTable.Rows[0];
				solicitud.FromDataRow(fila);
			}

			return solicitud;
		}

		public static int Crear(Modelo.Solicitud solicitud)
		{
			string INSERTSentence = "INSERT INTO SolicitudDespacho (NumeroSolicitud, TipoSolicitudId, EstadoSolicitudId, FechaSolicitud, FechaRecepcion, NumeroCliente, NombreCliente, CalleDireccionCliente, NumeroDireccionCliente, RegionClienteId, ComunaClienteId, NumeroTelefonoContacto, NumeroTelefonoContactoAdicional, RutCliente, VRutCliente, PrioridadId, UnidadNegocioId, GerenciaId, ObservacionAof, SolicitanteId)";
			string VALUESSentence = " VALUES({0}, {1}, {2}, '{3}', '{4}', '{5}', '{6}', '{7}', {8}, {9}, {10}, '{11}', '{12}', '{13}', '{14}', {15}, {16}, {17}, '{18}', {19});";
			string SQLSentence = INSERTSentence + VALUESSentence;
			StringBuilder builder = new StringBuilder();
			builder.AppendFormat(SQLSentence, solicitud.NumeroSolicitud, solicitud.TipoSolicitudId, solicitud.EstadoSolicitudId, solicitud.FechaSolicitud, solicitud.FechaRecepcion, solicitud.NumeroCliente, solicitud.NombreCliente, solicitud.CalleDireccionCliente, solicitud.NumeroDireccionCliente, solicitud.RegionClienteId, solicitud.ComunaClienteId, solicitud.NumeroTelefonoContacto, solicitud.NumeroTelefonoContactoAdicional, solicitud.RutCliente, solicitud.VRutCliente, solicitud.PrioridadId, solicitud.UnidadNegocioId, solicitud.GerenciaId, solicitud.ObservacionAof, solicitud.SolicitanteId);
			DataBase.ExecuteNonQuery(builder.ToString());

			return int.Parse(DataBase.ExecuteScalar("SELECT SCOPE_IDENTITY()").ToString());
		}

		public static bool Modificar(Modelo.Solicitud solicitud)
		{
			string UPDATESentence = "UPDATE SolicitudDespacho";
			string SETSentence1 = " SET NumeroSolicitud = {1}, TipoSolicitudId = {2}, EstadoSolicitudId = {3}, FechaSolicitud = '{3}', FechaRecepcion = '{4}',  NumeroCliente = '{5}', NombreCliente = '{6}', CalleDireccionCliente = '{7}', NumeroDireccionCliente = {8},";
			string SETSentence2 = " RegionClienteId = {9}, ComunaClienteId = {10}, NumeroTelefonoContacto = '{11}', NumeroTelefonoContactoAdicional = '{12}', RutCliente = '{13}', VRutCliente = '{14}', PrioridadId = {15}, UnidadNegocioId = {16}, GerenciaId = {17},";
			string SETSentence3 = " ObservacionAof = '{18}', FechaDespacho = '{19}', PatenteCamion = '{20}', LlamadaDiaAnterior = {21}, ComentariosLlamada = '{22}', NumeroDocumento = {23}, NumeroEntrega = {24},";
			string SETSentence4 = " FechaEntregaDocumento = '{25}', FechaRecepcionDocumento = '{26}', Folio = {27}, TipoDocumentoId = CASE WHEN {28} > 0 THEN {28} ELSE NULL END, Concrecion = {29}, NombreConcrecion = '{30}', RUTConcrecion = '{31}', VRUTConcrecion = '{32}', MotivoNoConcrecion = '{33}'";
			string WHERESentence = " WHERE SolicitudDespachoId = {0}";
			string SQLSentence = UPDATESentence + SETSentence1 + SETSentence2 + SETSentence3 + SETSentence4 + WHERESentence;
			StringBuilder builder = new StringBuilder();
			builder.AppendFormat(SQLSentence,
				solicitud.SolicitudDespachoId, solicitud.NumeroSolicitud, solicitud.TipoSolicitudId, solicitud.EstadoSolicitudId, solicitud.FechaSolicitud, solicitud.FechaRecepcion, solicitud.NumeroCliente, solicitud.NombreCliente, solicitud.CalleDireccionCliente, solicitud.NumeroDireccionCliente,
				solicitud.RegionClienteId, solicitud.ComunaClienteId, solicitud.NumeroTelefonoContacto, solicitud.NumeroTelefonoContactoAdicional, solicitud.RutCliente, solicitud.VRutCliente, solicitud.PrioridadId, solicitud.UnidadNegocioId, solicitud.GerenciaId,
				solicitud.ObservacionAof, solicitud.FechaDespacho, solicitud.PatenteCamion, solicitud.LlamadaDiaAnterior ? 1 : 0, solicitud.ComentariosLlamada, solicitud.NumeroDocumento, solicitud.NumeroEntrega,
				solicitud.FechaEntregaDocumento, solicitud.FechaRecepcionDocumento, solicitud.Folio, solicitud.TipoDocumentoId, solicitud.Concrecion ? 1 : 0, solicitud.NombreConcrecion, solicitud.RUTConcrecion, solicitud.VRUTConcrecion, solicitud.MotivoNoConcrecion);

			return DataBase.ExecuteNonQuery(builder.ToString()) > 0;
		}
	}
}
