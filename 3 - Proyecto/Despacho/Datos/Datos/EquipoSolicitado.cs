using Datos.DB;
using System.Collections.Generic;
using System.Data;
using System.Text;

namespace Datos.Datos
{
	public class EquipoSolicitado
	{
		public static List<Modelo.EquipoSolicitado> ObtenerEquiposSolicitados(int solicitudDespachoId)
		{
			string SELECTSentence = "SELECT ES.*, EE.Descripcion";
			string FROMSentence = " FROM EquiposSolicitados ES INNER JOIN EstadoEquipo EE ON EE.EstadoEquipoId = ES.EstadoEquipoId";
			string WHERESentence = " WHERE SolicitudDespachoId = " + solicitudDespachoId;
			string ORDERSentence = ";";
			string SQLSentence = SELECTSentence + FROMSentence + WHERESentence + ORDERSentence;

			DataTable dataTable = DataBase.ExecuteReader(SQLSentence);
			List<Modelo.EquipoSolicitado> equipos = new List<Modelo.EquipoSolicitado>();

			foreach (DataRow fila in dataTable.Rows)
			{
				Modelo.EquipoSolicitado equipo = new Modelo.EquipoSolicitado();
				equipo.FromDataRow(fila);
				equipos.Add(equipo);
			}

			return equipos;
		}

		public static bool Crear(List<Modelo.EquipoSolicitado> equipos)
		{
			int creados = 0;

			foreach (Modelo.EquipoSolicitado equipo in equipos)
			{
				string INSERTSentence = "INSERT INTO EquiposSolicitados";
				string VALUESSentence = " VALUES('{0}', '{1}', '{2}', {3}, {4});";
				string SQLSentence = INSERTSentence + VALUESSentence;
				StringBuilder builder = new StringBuilder();
				builder.AppendFormat(SQLSentence, equipo.NumeroPlaca, equipo.Marca, equipo.Modelo, equipo.EstadoEquipoId, equipo.SolicitudDespachoId);

				creados += DataBase.ExecuteNonQuery(builder.ToString());
			}

			return creados == equipos.Count;
		}

		public static bool Crear(Modelo.EquipoSolicitado equipo)
		{
			string INSERTSentence = "INSERT INTO EquiposSolicitados";
			string VALUESSentence = " VALUES('{0}', '{1}', '{2}', {3}, {4});";
			string SQLSentence = INSERTSentence + VALUESSentence;
			StringBuilder builder = new StringBuilder();
			builder.AppendFormat(SQLSentence, equipo.NumeroPlaca, equipo.Marca, equipo.Modelo, equipo.EstadoEquipoId, equipo.SolicitudDespachoId);

			return DataBase.ExecuteNonQuery(builder.ToString()) > 0;
		}

		public static bool EliminarPorSolicitud(int solicitudDespachoId)
		{
			string DELETESentence = "DELETE FROM EquiposSolicitados";
			string WHERESentence = " WHERE SolicitudDespachoId = {0}";
			string SQLSentence = DELETESentence + WHERESentence;
			StringBuilder builder = new StringBuilder();
			builder.AppendFormat(SQLSentence, solicitudDespachoId);

			return DataBase.ExecuteNonQuery(builder.ToString()) > 0;
		}

		public static bool Eliminar(int equipoId)
		{
			string DELETESentence = "DELETE FROM EquiposSolicitados";
			string WHERESentence = " WHERE EquipoSolicitadoId = {0}";
			string SQLSentence = DELETESentence + WHERESentence;
			StringBuilder builder = new StringBuilder();
			builder.AppendFormat(SQLSentence, equipoId);

			return DataBase.ExecuteNonQuery(builder.ToString()) > 0;
		}
	}
}
