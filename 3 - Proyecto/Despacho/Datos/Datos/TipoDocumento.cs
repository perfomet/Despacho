using Datos.DB;
using System.Collections.Generic;
using System.Data;
using System.Text;

namespace Datos.Datos
{
	public class TipoDocumento
	{
		public static List<Modelo.TipoDocumento> ObtenerTiposdeDocumentos()
		{
			string SELECTSentence = "SELECT *";
			string FROMSentence = " FROM TipoDocumento";
			string WHERESentence = "";
			string ORDERSentence = ";";
			string SQLSentence = SELECTSentence + FROMSentence + WHERESentence + ORDERSentence;
			DataTable dataTable = DataBase.ExecuteReader(SQLSentence);
			List<Modelo.TipoDocumento> tiposdedocumentos = new List<Modelo.TipoDocumento>();
			foreach (DataRow fila in dataTable.Rows)
			{
				Modelo.TipoDocumento tipodocumento = new Modelo.TipoDocumento();
				tipodocumento.FromDataRow(fila);
				tiposdedocumentos.Add(tipodocumento);
			}

			return tiposdedocumentos;
		}

		public static Modelo.TipoDocumento ObtenerTipoDocumento(int tipodocumentoId)
		{

			string SELECTSentence = "SELECT *";
			string FROMSentence = " FROM TipoDocumento";
			string WHERESentence = " WHERE TipoDocumentoId = '" + tipodocumentoId.ToString() + "'";
			string ORDERSentence = ";";
			string SQLSentence = SELECTSentence + FROMSentence + WHERESentence + ORDERSentence;
			Modelo.TipoDocumento tipodocumento = new Modelo.TipoDocumento();
			DataTable dataTable = DataBase.ExecuteReader(SQLSentence);

			if (dataTable.Rows.Count > 0)
			{
				DataRow fila = dataTable.Rows[0];
				tipodocumento.FromDataRow(fila);
			}

			return tipodocumento;
		}

		public static bool Crear(Modelo.TipoDocumento tipodocumento)
		{
			string INSERTSentence = "INSERT INTO TipoDocumento";
			string VALUESSentence = " VALUES('{1}', 1);";
			string SQLSentence = INSERTSentence + VALUESSentence;
			StringBuilder builder = new StringBuilder();

			builder.AppendFormat(SQLSentence, tipodocumento.Descripcion);
			return DataBase.ExecuteNonQuery(builder.ToString()) > 0;
		}

		public static bool Modificar(Modelo.TipoDocumento tipodocumento)
		{
			string UPDATESentence = "UPDATE TipoDocumento";
			string SETSentence = " SET Descripcion = '{1}'";
			string WHERESentence = " WHERE TipoDocumentoId = {0}";

			string SQLSentence = UPDATESentence + SETSentence + WHERESentence;
			StringBuilder builder = new StringBuilder();
			builder.AppendFormat(SQLSentence, tipodocumento.TipoDocumentoId, tipodocumento.Descripcion);

			return DataBase.ExecuteNonQuery(builder.ToString()) > 0;
		}

		public static bool EstaActivo(int Id)
		{
			string UPDATESentence = "UPDATE TipoDocumento";
			string SETSentence = " SET EstaActivo = CASE WHEN EstaActivo = 1 THEN 0 ELSE 1 END";
			string WHERESentence = " WHERE TipoDocumentoId = {0}";
			string SQLSentence = UPDATESentence + SETSentence + WHERESentence;
			StringBuilder builder = new StringBuilder();
			builder.AppendFormat(SQLSentence, Id);
			return DataBase.ExecuteNonQuery(builder.ToString()) > 0;
		}


	}
}
