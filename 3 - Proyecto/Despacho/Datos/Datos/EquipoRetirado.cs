using Datos.DB;
using System.Collections.Generic;
using System.Data;
using System.Text;

namespace Datos.Datos
{
	public class EquipoRetirado
	{
		public static List<Modelo.EquipoRetirado> ObtenerEquiposRetirados(int solicitudDespachoId)
		{
			string SELECTSentence = "SELECT *";
			string FROMSentence = " FROM EquiposRetirados";
			string WHERESentence = " WHERE SolicitudDespachoId = " + solicitudDespachoId;
			string ORDERSentence = ";";
			string SQLSentence = SELECTSentence + FROMSentence + WHERESentence + ORDERSentence;

			DataTable dataTable = DataBase.ExecuteReader(SQLSentence);
			List<Modelo.EquipoRetirado> equipos = new List<Modelo.EquipoRetirado>();

			foreach (DataRow fila in dataTable.Rows)
			{
				Modelo.EquipoRetirado equipo = new Modelo.EquipoRetirado();
				equipo.FromDataRow(fila);
				equipos.Add(equipo);
			}

			return equipos;
		}

		public static bool Crear(List<Modelo.EquipoRetirado> equipos)
		{
			int creados = 0;

			foreach (Modelo.EquipoRetirado equipo in equipos)
			{
				string INSERTSentence = "INSERT INTO EquiposRetirados";
				string VALUESSentence = " VALUES('{0}', {1});";
				string SQLSentence = INSERTSentence + VALUESSentence;
				StringBuilder builder = new StringBuilder();
				builder.AppendFormat(SQLSentence, equipo.NumeroPlaca, equipo.SolicitudDespachoId);

				creados += DataBase.ExecuteNonQuery(builder.ToString());
			}

			return creados == equipos.Count;
		}

		public static bool Crear(Modelo.EquipoRetirado equipo)
		{
			string INSERTSentence = "INSERT INTO EquiposRetirados";
			string VALUESSentence = " VALUES('{0}', {1});";
			string SQLSentence = INSERTSentence + VALUESSentence;
			StringBuilder builder = new StringBuilder();
			builder.AppendFormat(SQLSentence, equipo.NumeroPlaca, equipo.SolicitudDespachoId);

			return DataBase.ExecuteNonQuery(builder.ToString()) > 0;
		}

		public static bool EliminarPorSolicitud(int solicitudDespachoId)
		{
			string DELETESentence = "DELETE FROM EquiposRetirados";
			string WHERESentence = " WHERE SolicitudDespachoId = {0}";
			string SQLSentence = DELETESentence + WHERESentence;
			StringBuilder builder = new StringBuilder();
			builder.AppendFormat(SQLSentence, solicitudDespachoId);

			return DataBase.ExecuteNonQuery(builder.ToString()) > 0;
		}

		public static bool Eliminar(int equipoId)
		{
			string DELETESentence = "DELETE FROM EquiposRetirados";
			string WHERESentence = " WHERE EquipoRetiradoId = {0}";
			string SQLSentence = DELETESentence + WHERESentence;
			StringBuilder builder = new StringBuilder();
			builder.AppendFormat(SQLSentence, equipoId);

			return DataBase.ExecuteNonQuery(builder.ToString()) > 0;
		}
	}
}
