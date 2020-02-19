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
			string WHERESentence = "";
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
			string INSERTSentence = "INSERT INTO SolicitudDespacho (TipoSolicitudId, EstadoSolicitudId, FechaSolicitud, FechaRecepcion, BodegaOrigen, NumeroCliente, NombreCliente, DireccionCliente, ComunaClienteId, NumeroTelefonoContacto, RutCliente, VRutCliente, Proyecto, PrioridadId, UnidadNegocioId, GerenciaId, ObservacionAof, SolicitanteId)";
			string VALUESSentence = " VALUES({0}, {1}, '{2}', '{3}', '{4}', '{5}', '{6}', '{7}', {8}, '{9}', '{10}', '{11}', '{12}', {13}, {14}, {15}, '{16}', {17});";
			string SQLSentence = INSERTSentence + VALUESSentence;
			StringBuilder builder = new StringBuilder();
			builder.AppendFormat(SQLSentence, solicitud.TipoSolicitudId, solicitud.EstadoSolicitudId, solicitud.FechaSolicitud, solicitud.FechaRecepcion, solicitud.BodegaOrigen, solicitud.NumeroCliente, solicitud.NombreCliente, solicitud.DireccionCliente, solicitud.ComunaClienteId, solicitud.NumeroTelefonoContacto, solicitud.RutCliente, solicitud.VRutCliente, solicitud.Proyecto, solicitud.PrioridadId, solicitud.UnidadNegocioId, solicitud.GerenciaId, solicitud.ObservacionAof, solicitud.SolicitanteId);
			DataBase.ExecuteNonQuery(builder.ToString());

			return int.Parse(DataBase.ExecuteScalar("SELECT SCOPE_IDENTITY()").ToString());
		}

		public static bool Modificar(Modelo.Solicitud solicitud)
		{
			string UPDATESentence = "UPDATE SolicitudDespacho";
			string SETSentence1 = " SET TipoSolicitudId = {1}, EstadoSolicitudId = {2}, FechaSolicitud = '{3}', FechaRecepcion = '{4}', BodegaOrigen = '{5}', NumeroCliente = '{6}', NombreCliente = '{7}', DireccionCliente = '{8}',";
			string SETSentence2 = " ComunaClienteId = {9}, NumeroTelefonoContacto = '{10}', RutCliente = '{11}', VRutCliente = '{12}', Proyecto = '{13}', PrioridadId = {14}, UnidadNegocioId = {15}, GerenciaId = {16},";
			string SETSentence3 = " ObservacionAof = '{17}', FechaDespacho = '{18}', PatenteCamion = '{19}', LlamadaDiaAnterior = {20}, ComentariosLlamada = '{21}', NumeroDocumento = {22}, NumeroEntrega = {23},";
			string SETSentence4 = " FechaEntregaDocumento = '{24}', FechaRecepcionDocumento = '{25}', Folio = {26}, TipoDocumentoId = CASE WHEN {27} > 0 THEN {27} ELSE NULL END, Concrecion = {28}, NombreConcrecion = '{29}', RUTConcrecion = '{30}', VRUTConcrecion = '{31}', MotivoNoConcrecion = '{32}'";
			string WHERESentence = " WHERE SolicitudDespachoId = {0}";
			string SQLSentence = UPDATESentence + SETSentence1 + SETSentence2 + SETSentence3 + SETSentence4 + WHERESentence;
			StringBuilder builder = new StringBuilder();
			builder.AppendFormat(SQLSentence,
				solicitud.SolicitudDespachoId, solicitud.TipoSolicitudId, solicitud.EstadoSolicitudId, solicitud.FechaSolicitud, solicitud.FechaRecepcion, solicitud.BodegaOrigen, solicitud.NumeroCliente, solicitud.NombreCliente, solicitud.DireccionCliente,
				solicitud.ComunaClienteId, solicitud.NumeroTelefonoContacto, solicitud.RutCliente, solicitud.VRutCliente, solicitud.Proyecto, solicitud.PrioridadId, solicitud.UnidadNegocioId, solicitud.GerenciaId,
				solicitud.ObservacionAof, solicitud.FechaDespacho, solicitud.PatenteCamion, solicitud.LlamadaDiaAnterior ? 1 : 0, solicitud.ComentariosLlamada, solicitud.NumeroDocumento, solicitud.NumeroEntrega,
				solicitud.FechaEntregaDocumento, solicitud.FechaRecepcionDocumento, solicitud.Folio, solicitud.TipoDocumentoId, solicitud.Concrecion ? 1 : 0, solicitud.NombreConcrecion, solicitud.RUTConcrecion, solicitud.VRUTConcrecion, solicitud.MotivoNoConcrecion);

			return DataBase.ExecuteNonQuery(builder.ToString()) > 0;
		}
	}
}
