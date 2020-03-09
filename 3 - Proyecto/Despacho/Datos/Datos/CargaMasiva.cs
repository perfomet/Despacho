using Datos.DB;
using System.Collections.Generic;
using System.Data;
using System.Text;

namespace Datos.Datos
{
	public class CargaMasiva
	{

		public static List<Modelo.CargaMasiva> ObtenerCargasMasivas()
		{
			string SELECTSentence = "SELECT *";
			string FROMSentence = " FROM CargaMasiva";
			string WHERESentence = "";
			string ORDERSentence = ";";
			string SQLSentence = SELECTSentence + FROMSentence + WHERESentence + ORDERSentence;

			DataTable dataTable = DataBase.ExecuteReader(SQLSentence);
			List<Modelo.CargaMasiva> listacargasmasivas = new List<Modelo.CargaMasiva>();

			foreach (DataRow fila in dataTable.Rows)
			{
				Modelo.CargaMasiva cargamasiva = new Modelo.CargaMasiva();
				cargamasiva.FromDataRow(fila);
				listacargasmasivas.Add(cargamasiva);
			}

			return listacargasmasivas;
		}

		public static Modelo.CargaMasiva ObtenerCargaMasiva(int id)
		{

			string SELECTSentence = "SELECT *";
			string FROMSentence = " FROM CargaMasiva";
			string WHERESentence = " WHERE CargaMasivaId = " + id.ToString();
			string ORDERSentence = ";";
			string SQLSentence = SELECTSentence + FROMSentence + WHERESentence + ORDERSentence;

			Modelo.CargaMasiva cargamasiva = new Modelo.CargaMasiva();
			DataTable dataTable = DataBase.ExecuteReader(SQLSentence);

			if (dataTable.Rows.Count > 0)
			{
				DataRow fila = dataTable.Rows[0];
				cargamasiva.FromDataRow(fila);
			}

			return cargamasiva;
		}

		public static int Crear(Modelo.CargaMasiva cargamasiva)
		{
			if (cargamasiva.UsuarioId > 0)
			{

				string INSERTSentence = "INSERT INTO CargaMasiva (UsuarioId, FechaHora, Archivo)";
				string VALUESSentence = " VALUES({0}, CONVERT(DATETIME, '{1}', 103), '{2}');";
				string SQLSentence = INSERTSentence + VALUESSentence;
				StringBuilder builder = new StringBuilder();
				builder.AppendFormat(SQLSentence, cargamasiva.UsuarioId, cargamasiva.FechaHora, cargamasiva.Archivo);
				return DataBase.ExecuteNonQueryId(builder.ToString());
			}
			else
			{
				return 0;
			}

		}

		public static bool Modificar(Modelo.CargaMasiva cargamasiva)
		{
			string UPDATESentence = "UPDATE CargaMasiva";
			string SETSentence = " SET UsuarioId = {1}, FechaHora = '{2}', Archivo = '{3}'";
			string WHERESentence = " WHERE CargaMasivaId = {0}";
			string SQLSentence = UPDATESentence + SETSentence + WHERESentence;
			StringBuilder builder = new StringBuilder();
			builder.AppendFormat(SQLSentence, cargamasiva.CargaMasivaId, cargamasiva.UsuarioId, cargamasiva.FechaHora, cargamasiva.Archivo);

			return DataBase.ExecuteNonQuery(builder.ToString()) > 0;
		}
	}
}

