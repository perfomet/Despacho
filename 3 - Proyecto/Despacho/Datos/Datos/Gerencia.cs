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
			string SELECTSentence = "SELECT Gerencia.GerenciaId, Gerencia.Descripcion, Gerencia.ClienteId, Cliente.Nombre AS Nombrecliente, Gerencia.EstaActivo";
			string FROMSentence = " FROM Cliente INNER JOIN Gerencia ON Cliente.ClienteId = Gerencia.ClienteId";
			string WHERESentence = "";
			string ORDERSentence = ";";
			string SQLSentence = SELECTSentence + FROMSentence + WHERESentence + ORDERSentence;
			DataTable dataTable = DataBase.ExecuteReader(SQLSentence);
			List<Modelo.Gerencia> listagerencias = new List<Modelo.Gerencia>();
			foreach (DataRow fila in dataTable.Rows)
			{
				Modelo.Gerencia gerencia = new Modelo.Gerencia();
				gerencia.FromDataRow(fila);
				listagerencias.Add(gerencia);
			}

			return listagerencias;
		}

		public static Modelo.Gerencia ObtenerGerencia(int id)
		{

			string SELECTSentence = "SELECT Gerencia.GerenciaId, Gerencia.Descripcion, Gerencia.ClienteId, Cliente.Nombre AS Clientenombre, Gerencia.EstaActivo";
			string FROMSentence = " FROM Cliente INNER JOIN Gerencia ON Cliente.ClienteId = Gerencia.ClienteId";
			string WHERESentence = " WHERE Gerencia.GerenciaId = '" + id.ToString() + "'";
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
			string VALUESSentence = " VALUES('{1}',{2}, 1);";
			string SQLSentence = INSERTSentence + VALUESSentence;
			StringBuilder builder = new StringBuilder();
			builder.AppendFormat(SQLSentence, gerencia.Descripcion, gerencia.Clienteid);
			return DataBase.ExecuteNonQuery(builder.ToString()) > 0;
		}

		public static bool Modificar(Modelo.Gerencia gerencia)
		{
			string UPDATESentence = "UPDATE Gerencia";
			string SETSentence = " SET Gerencia.Descripcion = '{1}', Gerencia.ClienteId = {2}";
			string WHERESentence = " WHERE Gerencia.GerenciaId = {0}";
			string SQLSentence = UPDATESentence + SETSentence + WHERESentence;
			StringBuilder builder = new StringBuilder();
			builder.AppendFormat(SQLSentence, gerencia.Gerenciaid, gerencia.Descripcion, gerencia.Clienteid, gerencia.EstaActivo);

			return DataBase.ExecuteNonQuery(builder.ToString()) > 0;
		}

		public static bool EstaActivo(int Id)
		{
			string UPDATESentence = "UPDATE Gerencia";
			string SETSentence = " SET Gerencia.EstaActivo = CASE WHEN Gerencia.EstaActivo = 1 THEN 0 ELSE 1 END";
			string WHERESentence = " WHERE Gerencia.GerenciaId = {0}";
			string SQLSentence = UPDATESentence + SETSentence + WHERESentence;
			StringBuilder builder = new StringBuilder();
			builder.AppendFormat(SQLSentence, Id);
			return DataBase.ExecuteNonQuery(builder.ToString()) > 0;
		}
	}
}
