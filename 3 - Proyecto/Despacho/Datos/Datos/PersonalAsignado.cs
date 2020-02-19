using Datos.DB;
using System.Collections.Generic;
using System.Data;
using System.Text;

namespace Datos.Datos
{
	public class PersonalAsignado
	{
		public static List<Modelo.PersonalAsignado> ObtenerPersonalAsignado(int solicitudDespachoId)
		{
			string SELECTSentence = "SELECT *";
			string FROMSentence = " FROM PersonalAsignado";
			string WHERESentence = " WHERE SolicitudDespachoId = " + solicitudDespachoId;
			string ORDERSentence = ";";
			string SQLSentence = SELECTSentence + FROMSentence + WHERESentence + ORDERSentence;

			DataTable dataTable = DataBase.ExecuteReader(SQLSentence);
			List<Modelo.PersonalAsignado> personal = new List<Modelo.PersonalAsignado>();

			foreach (DataRow fila in dataTable.Rows)
			{
				Modelo.PersonalAsignado p = new Modelo.PersonalAsignado();
				p.FromDataRow(fila);
				personal.Add(p);
			}

			return personal;
		}

		public static bool Crear(List<Modelo.PersonalAsignado> personal)
		{
			int creados = 0;

			foreach (Modelo.PersonalAsignado p in personal)
			{
				string INSERTSentence = "INSERT INTO PersonalAsignado";
				string VALUESSentence = " VALUES({0}, {1});";
				string SQLSentence = INSERTSentence + VALUESSentence;
				StringBuilder builder = new StringBuilder();
				builder.AppendFormat(SQLSentence, p.SolicitudDespachoId, p.PersonalId);

				creados += DataBase.ExecuteNonQuery(builder.ToString());
			}

			return creados == personal.Count;
		}

		public static bool Crear(Modelo.PersonalAsignado personal)
		{
			string INSERTSentence = "INSERT INTO PersonalAsignado";
			string VALUESSentence = " VALUES({0}, {1});";
			string SQLSentence = INSERTSentence + VALUESSentence;
			StringBuilder builder = new StringBuilder();
			builder.AppendFormat(SQLSentence, personal.SolicitudDespachoId, personal.PersonalId);

			return DataBase.ExecuteNonQuery(builder.ToString()) > 0;
		}

		public static bool EliminarPorSolicitud(int solicitudDespachoId)
		{
			string DELETESentence = "DELETE FROM PersonalAsignado";
			string WHERESentence = " WHERE SolicitudDespachoId = {0}";
			string SQLSentence = DELETESentence + WHERESentence;
			StringBuilder builder = new StringBuilder();
			builder.AppendFormat(SQLSentence, solicitudDespachoId);

			return DataBase.ExecuteNonQuery(builder.ToString()) > 0;
		}

		public static bool Eliminar(int personalId, int solicitudDespachoId)
		{
			string DELETESentence = "DELETE FROM PersonalAsignado";
			string WHERESentence = " WHERE PersonalId = {0} AND SolicitudDespachoId = {1}";
			string SQLSentence = DELETESentence + WHERESentence;
			StringBuilder builder = new StringBuilder();
			builder.AppendFormat(SQLSentence, personalId, solicitudDespachoId);

			return DataBase.ExecuteNonQuery(builder.ToString()) > 0;
		}
	}
}
