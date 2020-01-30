using Datos.DB;
using System.Collections.Generic;
using System.Data;
using System.Text;
namespace Datos.Datos
{
	public class Gerencia
	{
		public static List<Modelo.Gerencia> ObtenerGerencias()
		{
			string SELECTSentence = "SELECT *";
			string FROMSentence = " FROM Gerencia";
			string WHERESentence = "";
			string ORDERSentence = ";";
			string SQLSentence = SELECTSentence + FROMSentence + WHERESentence + ORDERSentence;
			DataTable dataTable = DataBase.ExecuteReader(SQLSentence);
			List<Modelo.Gerencia> gerencias = new List<Modelo.Gerencia>();
			foreach (DataRow fila in dataTable.Rows)
			{
				Modelo.Gerencia gerencia = new Modelo.Gerencia();
				gerencia.FromDataRow(fila);
				gerencias.Add(gerencia);
			}

			return gerencias;
		}

		public static Modelo.Gerencia ObtenerGerencia(int Id)
		{

			string SELECTSentence = "SELECT *";
			string FROMSentence = " FROM Gerencia";
			string WHERESentence = " WHERE GerenciaId = '" + Id.ToString() + "'";
			string ORDERSentence = ";";
			string SQLSentence = SELECTSentence + FROMSentence + WHERESentence + ORDERSentence;
			Modelo.Gerencia gerencia = new Modelo.Gerencia();
			DataTable dataTable = DataBase.ExecuteReader(SQLSentence);

			if (dataTable.Rows.Count > 0)
			{
				DataRow fila = dataTable.Rows[0];
				gerencia.FromDataRow(fila);
			}

			return gerencia;
		}

		public static bool Crear(Modelo.Gerencia gerencia)
		{
			string INSERTSentence = "INSERT INTO Gerencia";
			string VALUESSentence = " VALUES('{1}',{2});";
			string SQLSentence = INSERTSentence + VALUESSentence;
			StringBuilder builder = new StringBuilder();
			builder.AppendFormat(SQLSentence, gerencia.Descripcion, gerencia.ClienteId);
			return DataBase.ExecuteNonQuery(builder.ToString()) > 0;
		}

		public static bool Modificar(Modelo.Gerencia gerencia)
		{
			string UPDATESentence = "UPDATE Gerencia";
			string SETSentence = " SET Descripcion = '{1}', ClienteId = {2}";
			string WHERESentence = " WHERE GerenciaId = {0}";
			string SQLSentence = UPDATESentence + SETSentence + WHERESentence;
			StringBuilder builder = new StringBuilder();
			builder.AppendFormat(SQLSentence, gerencia.GerenciaId, gerencia.Descripcion, gerencia.ClienteId,gerencia.EstaActivo);

			return DataBase.ExecuteNonQuery(builder.ToString()) > 0;
		}

		public static bool EstaActivo(int Id)
		{
			string UPDATESentence = "UPDATE Gerencia";
			string SETSentence = " SET EstaActivo = CASE WHEN EstaActivo = 1 THEN 0 ELSE 1 END";
			string WHERESentence = " WHERE GerenciaId = {0}";
			string SQLSentence = UPDATESentence + SETSentence + WHERESentence;
			StringBuilder builder = new StringBuilder();
			builder.AppendFormat(SQLSentence, Id);
			return DataBase.ExecuteNonQuery(builder.ToString()) > 0;
		}
	}
}
