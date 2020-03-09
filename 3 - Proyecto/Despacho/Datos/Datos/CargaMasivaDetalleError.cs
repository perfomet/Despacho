using Datos.DB;
using System.Collections.Generic;
using System.Data;
using System.Text;

namespace Datos.Datos
{
	public class CargaMasivaDetalleError
	{


		public static List<Modelo.CargaMasivaDetalleError> ObtenerErrores()
		{
			string SELECTSentence = "SELECT *";
			string FROMSentence = " FROM CargaMasivaDetalleError";
			string JOINSentence = "";
			string WHERESentence = "";
			string ORDERSentence = ";";
			string SQLSentence = SELECTSentence + FROMSentence + JOINSentence + WHERESentence + ORDERSentence;

			DataTable dataTable = DataBase.ExecuteReader(SQLSentence);
			List<Modelo.CargaMasivaDetalleError> errores = new List<Modelo.CargaMasivaDetalleError>();

			foreach (DataRow fila in dataTable.Rows)
			{
				Modelo.CargaMasivaDetalleError error = new Modelo.CargaMasivaDetalleError();
				error.FromDataRow(fila);
				errores.Add(error);
			}

			return errores;
		}

		public static Modelo.CargaMasivaDetalleError ObtenerError(int id)
		{
			string SELECTSentence = "SELECT *";
			string FROMSentence = " FROM CargaMasivaDetalleError";
			string JOINSentence = "";
			string WHERESentence = " CargaMasivaDetalleId = " + id.ToString();
			string ORDERSentence = ";";
			string SQLSentence = SELECTSentence + FROMSentence + JOINSentence + WHERESentence + ORDERSentence;

			Modelo.CargaMasivaDetalleError error = new Modelo.CargaMasivaDetalleError();
			DataTable dataTable = DataBase.ExecuteReader(SQLSentence);

			if (dataTable.Rows.Count > 0)
			{
				DataRow fila = dataTable.Rows[0];
				error.FromDataRow(fila);
			}

			return error;
		}

		public static bool Crear(Modelo.CargaMasivaDetalleError error)
		{
			string INSERTSentence = "INSERT INTO CargaMasivaDetalleError (CargaMasivaDetalleErrorId, Title, Clase, Tipo, CargaMasivaDetalleId)";
			string VALUESSentence = " VALUES({0}, '{1}', '{2}', '{3}', {4});";
			string SQLSentence = INSERTSentence + VALUESSentence;
			StringBuilder builder = new StringBuilder();
			builder.AppendFormat(SQLSentence, error.id, error.title, error.clase, error.tipo, error.CargaMasivaDetalleId);
			return DataBase.ExecuteNonQuery(builder.ToString()) > 0;
		}

		public static bool Crear(List<Modelo.CargaMasivaDetalleError> errores)
		{
			int insertados = 0;

			errores.ForEach((error) =>
			{
				string INSERTSentence = "INSERT INTO CargaMasivaDetalleError (CargaMasivaDetalleErrorId, Title, Clase, Tipo, CargaMasivaDetalleId)";
				string VALUESSentence = " VALUES({0}, '{1}', '{2}', '{3}', {4});";
				string SQLSentence = INSERTSentence + VALUESSentence;
				StringBuilder builder = new StringBuilder();
				builder.AppendFormat(SQLSentence, error.id, error.title, error.clase, error.tipo, error.CargaMasivaDetalleId);
				insertados += DataBase.ExecuteNonQuery(builder.ToString());
			});

			return errores.Count == insertados;
		}
	}
}

