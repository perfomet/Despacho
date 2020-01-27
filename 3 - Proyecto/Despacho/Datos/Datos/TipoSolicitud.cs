using Datos.DB;
using System.Collections.Generic;
using System.Data;
using System.Text;

namespace Datos.Datos
{
	public class TipoSolicitud
	{
		public static List<Modelo.TipoSolicitud> ObtenerTiposSolicitudes()
		{
			string SELECTSentence = "SELECT *";
			string FROMSentence = " FROM TipoSolicitud";
			string WHERESentence = "";
			string ORDERSentence = ";";
			string SQLSentence = SELECTSentence + FROMSentence + WHERESentence + ORDERSentence;
			DataTable dataTable = DataBase.ExecuteReader(SQLSentence);
			List<Modelo.TipoSolicitud> tipossolicitudes = new List<Modelo.TipoSolicitud>();
			foreach (DataRow fila in dataTable.Rows)
			{
				Modelo.TipoSolicitud tiposolicitud = new Modelo.TipoSolicitud();
				tiposolicitud.FromDataRow(fila);
				tipossolicitudes.Add(tiposolicitud);
			}

			return tipossolicitudes;
		}

		public static Modelo.TipoSolicitud ObtenerTipoSolicitud(int tiposolicitudId)
		{

			string SELECTSentence = "SELECT *";
			string FROMSentence = " FROM TipoSolicitud";
			string WHERESentence = " WHERE TipoSolicitudId = '" + tiposolicitudId.ToString() + "'";
			string ORDERSentence = ";";
			string SQLSentence = SELECTSentence + FROMSentence + WHERESentence + ORDERSentence;
			Modelo.TipoSolicitud tiposolicitud = new Modelo.TipoSolicitud();
			DataTable dataTable = DataBase.ExecuteReader(SQLSentence);

			if (dataTable.Rows.Count > 0)
			{
				DataRow fila = dataTable.Rows[0];
				tiposolicitud.FromDataRow(fila);
			}

			return tiposolicitud;
		}

		public static bool Crear(Modelo.TipoSolicitud tiposolicitud)
		{
			string INSERTSentence = "INSERT INTO TipoSolicitud";
			string VALUESSentence = " VALUES('{1}', '{2}');";
			string SQLSentence = INSERTSentence + VALUESSentence;
			StringBuilder builder = new StringBuilder();

			builder.AppendFormat(SQLSentence, tiposolicitud.Descripcion, tiposolicitud.Observaciones);
			return DataBase.ExecuteNonQuery(builder.ToString()) > 0;
		}

		public static bool Modificar(Modelo.TipoSolicitud tiposolicitud)
		{
			string UPDATESentence = "UPDATE TipoSolicitud";
			string SETSentence = " SET Descripcion = '{1}', Observaciones = '{2}'";
			string WHERESentence = " WHERE TipoSolicitudId = {0}";
			string SQLSentence = UPDATESentence + SETSentence + WHERESentence;
			StringBuilder builder = new StringBuilder();
			builder.AppendFormat(SQLSentence, tiposolicitud.TipoSolicitudId, tiposolicitud.Descripcion, tiposolicitud.Observaciones);

			return DataBase.ExecuteNonQuery(builder.ToString()) > 0;
		}

		public static bool Eliminar(int tiposolicitudId)
		{
			string DELETESentence = "DELETE";
			string FROMSentence = " FROM TipoSolicitud";
			string WHERESentence = " WHERE TipoSolicitudId = {0}";
			string SQLSentence = DELETESentence + FROMSentence + WHERESentence;
			StringBuilder builder = new StringBuilder();
			builder.AppendFormat(SQLSentence, tiposolicitudId);
			return DataBase.ExecuteNonQuery(builder.ToString()) > 0;
		}
	}
}
