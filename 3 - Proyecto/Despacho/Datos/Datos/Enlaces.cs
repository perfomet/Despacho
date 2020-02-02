using System;
using System.Data;
using Datos.DB;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Datos.Datos
{
	public static class Enlaces
	{
		public static List<Modelo.Enlaces> ObtenerEnlaces()
		{
			string SELECTSentence = "SELECT Enlaces.EnlaceId, Enlaces.Descripcion,  Enlaces.EstaActivo";
			string FROMSentence = " FROM Enlaces";
			string WHERESentence = "";
			string ORDERSentence = ";";
			string SQLSentence = SELECTSentence + FROMSentence + WHERESentence + ORDERSentence;
			DataTable dataTable = DataBase.ExecuteReader(SQLSentence);
			List<Modelo.Enlaces> listaenlaces = new List<Modelo.Enlaces>();
			foreach (DataRow fila in dataTable.Rows)
			{
				Modelo.Enlaces enlaces = new Modelo.Enlaces();
				enlaces.FromDataRow(fila);
				listaenlaces.Add(enlaces);
			}

			return listaenlaces;
		}
		public static Modelo.Enlaces ObtenerEnlace(int Id)
		{
			string SELECTSentence = "SELECT Enlaces.EnlaceId, Enlaces.Descripcion,  Enlaces.EstaActivo";
			string FROMSentence = " FROM Enlaces";
			string WHERESentence = " WHERE Enlaces.EnlaceId = " + Id.ToString();
			string ORDERSentence = ";";
			string SQLSentence = SELECTSentence + FROMSentence + WHERESentence + ORDERSentence;
			Modelo.Enlaces enlace = new Modelo.Enlaces();
			DataTable dataTable = DataBase.ExecuteReader(SQLSentence);

			if (dataTable.Rows.Count > 0)
			{
				DataRow fila = dataTable.Rows[0];
				enlace.FromDataRow(fila);
			}

			return enlace;
		}

		public static bool Crear(Modelo.Enlaces enlaces)
		{
			string INSERTSentence = "INSERT INTO Enlaces";
			string VALUESSentence = " VALUES('{1}', 1);";
			string SQLSentence = INSERTSentence + VALUESSentence;
			StringBuilder builder = new StringBuilder();

			builder.AppendFormat(SQLSentence,enlaces.Descripcion);
			return DataBase.ExecuteNonQuery(builder.ToString()) > 0;
		}

		public static bool Modificar(Modelo.Enlaces enlaces)
		{
			string UPDATESentence = "UPDATE Enlaces";
			string SETSentence = " SET Enlaces.Descripcion = '{1}'";
			string WHERESentence = " WHERE Enlaces.EnlaceId = {0}";

			string SQLSentence = UPDATESentence + SETSentence + WHERESentence;
			StringBuilder builder = new StringBuilder();
			builder.AppendFormat(SQLSentence, enlaces.Descripcion);

			return DataBase.ExecuteNonQuery(builder.ToString()) > 0;
		}

		public static bool EstaActivo(int Id)
		{
			string UPDATESentence = "UPDATE Enlaces";
			string SETSentence = " SET Enlaces.EstaActivo = CASE WHEN Enlaces.EstaActivo = 1 THEN 0 ELSE 1 END";
			string WHERESentence = " WHERE Enlaces.EstadoEquipoId = {0}";
			string SQLSentence = UPDATESentence + SETSentence + WHERESentence;
			StringBuilder builder = new StringBuilder();
			builder.AppendFormat(SQLSentence, Id);
			return DataBase.ExecuteNonQuery(builder.ToString()) > 0;
		}
	}
}
