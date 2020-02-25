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
			string SELECTSentence = "SELECT SD.*, TS.Descripcion, ES.Descripcion, P.Descripcion, U.Nombres + ISNULL(' ' + U.ApellidoPaterno, '') + ISNULL(' ' + U.ApellidoMaterno, ''), C.Nombre";
			string FROMSentence = " FROM SolicitudDespacho SD";
			string JOINSentence = " INNER JOIN TipoSolicitud TS ON TS.TipoSolicitudId = SD.TipoSolicitudId";
			JOINSentence += " INNER JOIN EstadoSolicitud ES ON ES.EstadoSolicitudId = SD.EstadoSolicitudId";
			JOINSentence += " INNER JOIN Prioridad P ON P.PrioridadId = SD.PrioridadId";
			JOINSentence += " INNER JOIN Usuario U ON U.UsuarioId = SD.SolicitanteId";
			JOINSentence += " LEFT JOIN Cliente C ON C.ClienteId = U.ClienteId";
			string WHERESentence = clienteId > 0 ? (" WHERE C.ClienteId = " + clienteId) : "";
			string ORDERSentence = ";";
			string SQLSentence = SELECTSentence + FROMSentence + JOINSentence + WHERESentence + ORDERSentence;

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

			string SELECTSentence = "SELECT SD.*, TS.Descripcion, ES.Descripcion, P.Descripcion, U.Nombres + ISNULL(' ' + U.ApellidoPaterno, '') + ISNULL(' ' + U.ApellidoMaterno, ''), C.Nombre";
			string FROMSentence = " FROM SolicitudDespacho SD";
			string JOINSentence = " INNER JOIN TipoSolicitud TS ON TS.TipoSolicitudId = SD.TipoSolicitudId";
			JOINSentence += " INNER JOIN EstadoSolicitud ES ON ES.EstadoSolicitudId = SD.EstadoSolicitudId";
			JOINSentence += " INNER JOIN Prioridad P ON P.PrioridadId = SD.PrioridadId";
			JOINSentence += " INNER JOIN Usuario U ON U.UsuarioId = SD.SolicitanteId";
			JOINSentence += " LEFT JOIN Cliente C ON C.ClienteId = U.ClienteId";
			string WHERESentence = " WHERE SolicitudDespachoId = " + id;
			string ORDERSentence = ";";
			string SQLSentence = SELECTSentence + FROMSentence + JOINSentence + WHERESentence + ORDERSentence;

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

			int id = 0;
			int.TryParse(DataBase.ExecuteScalar("SELECT SCOPE_IDENTITY()").ToString(), out id);
			return id;
		}

		public static bool Modificar(Modelo.Solicitud solicitud)
		{
			string UPDATESentence = "UPDATE SolicitudDespacho";
			string SETSentence1 = " SET NumeroSolicitud = {1}, TipoSolicitudId = {2}, EstadoSolicitudId = {3}, FechaSolicitud = '{4}', FechaRecepcion = '{5}',  NumeroCliente = '{6}', NombreCliente = '{7}', CalleDireccionCliente = '{8}', NumeroDireccionCliente = {9},";
			string SETSentence2 = " RegionClienteId = {10}, ComunaClienteId = {11}, NumeroTelefonoContacto = '{12}', NumeroTelefonoContactoAdicional = '{13}', RutCliente = '{14}', VRutCliente = '{15}', PrioridadId = {16}, UnidadNegocioId = {17}, GerenciaId = {18},";
			string SETSentence3 = " ObservacionAof = '{19}', FechaDespacho = '{20}', PatenteCamion = CASE '{21}' WHEN '' THEN NULL ELSE '{21}' END, LlamadaDiaAnterior = {22}, ComentariosLlamada = '{23}', NumeroDocumento = {24}, NumeroEntrega = {25},";
			string SETSentence4 = " FechaEntregaDocumento = '{26}', FechaRecepcionDocumento = '{27}', Folio = {28}, TipoDocumentoId = CASE WHEN {29} > 0 THEN {29} ELSE NULL END, Concrecion = {30}, NombreConcrecion = '{31}', RUTConcrecion = '{32}', VRUTConcrecion = '{33}', MotivoNoConcrecion = '{34}'";
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
