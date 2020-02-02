using System;
using System.Data;
using Datos.DB;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Datos.Datos
{
	public static class EstadoEquipo
	{
		public static List<Modelo.EstadoEquipo> ObtenerEstadosEquipos()
		{
			string SELECTSentence = "SELECT EstadoEquipo.EstadoEquipoId, EstadoEquipo.Descripcion,  EstadoEquipo.EstaActivo";
			string FROMSentence = " FROM EstadoEquipo";
			string WHERESentence = "";
			string ORDERSentence = ";";
			string SQLSentence = SELECTSentence + FROMSentence + WHERESentence + ORDERSentence;
			DataTable dataTable = DataBase.ExecuteReader(SQLSentence);
			List<Modelo.EstadoEquipo> estadoequipos = new List<Modelo.EstadoEquipo>();
			foreach (DataRow fila in dataTable.Rows)
			{
				Modelo.EstadoEquipo estadoequipo = new Modelo.EstadoEquipo();
				estadoequipo.FromDataRow(fila);
				estadoequipos.Add(estadoequipo);
			}

			return estadoequipos;
		}
		public static Modelo.EstadoEquipo ObtenerEstadoEquipo(int Id)
		{
			string SELECTSentence = "SELECT EstadoEquipo.EstadoEquipoId, EstadoEquipo.Descripcion,  EstadoEquipo.EstaActivo";
			string FROMSentence = " FROM EstadoEquipo";
			string WHERESentence = " WHERE EstadoEquipo.EstadoEquipoId = '" + Id.ToString() + "'";
			string ORDERSentence = ";";
			string SQLSentence = SELECTSentence + FROMSentence + WHERESentence + ORDERSentence;
			Modelo.EstadoEquipo estadoequipo = new Modelo.EstadoEquipo();
			DataTable dataTable = DataBase.ExecuteReader(SQLSentence);

			if (dataTable.Rows.Count > 0)
			{
				DataRow fila = dataTable.Rows[0];
				estadoequipo.FromDataRow(fila);
			}

			return estadoequipo;
		}

		public static bool Crear(Modelo.EstadoEquipo estadoequipo)
		{
			string INSERTSentence = "INSERT INTO EstadoEquipo";
			string VALUESSentence = " VALUES('{1}', 1);";
			string SQLSentence = INSERTSentence + VALUESSentence;
			StringBuilder builder = new StringBuilder();

			builder.AppendFormat(SQLSentence, estadoequipo.Descripcion);
			return DataBase.ExecuteNonQuery(builder.ToString()) > 0;
		}

		public static bool Modificar(Modelo.EstadoEquipo estadoequipo)
		{
			string UPDATESentence = "UPDATE EstadoEquipo";
			string SETSentence = " SET EstadoEquipo.Descripcion = '{1}'";
			string WHERESentence = " WHERE EstadoEquipo.EstadoEquipoId = {0}";

			string SQLSentence = UPDATESentence + SETSentence + WHERESentence;
			StringBuilder builder = new StringBuilder();
			builder.AppendFormat(SQLSentence, estadoequipo.Estadoequipoid, estadoequipo.Descripcion);

			return DataBase.ExecuteNonQuery(builder.ToString()) > 0;
		}

		public static bool EstaActivo(int Id)
		{
			string UPDATESentence = "UPDATE EstadoEquipo";
			string SETSentence = " SET EstadoEquipo.EstaActivo = CASE WHEN EstadoEquipo.EstaActivo = 1 THEN 0 ELSE 1 END";
			string WHERESentence = " WHERE EstadoEquipo.EstadoEquipoId = {0}";
			string SQLSentence = UPDATESentence + SETSentence + WHERESentence;
			StringBuilder builder = new StringBuilder();
			builder.AppendFormat(SQLSentence, Id);
			return DataBase.ExecuteNonQuery(builder.ToString()) > 0;
		}
	}
}
