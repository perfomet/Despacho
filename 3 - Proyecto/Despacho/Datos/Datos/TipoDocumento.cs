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
			string SELECTSentence = "SELECT TipoDocumento.TipoDocumentoId, TipoDocumento.Descripcion, TipoDocumento.EstaActivo";
			string FROMSentence = " FROM TipoDocumento";
			string WHERESentence = "";
			string ORDERSentence = ";";
			string SQLSentence = SELECTSentence + FROMSentence + WHERESentence + ORDERSentence;
			DataTable dataTable = DataBase.ExecuteReader(SQLSentence);
			List<Modelo.TipoDocumento> listadocumentos = new List<Modelo.TipoDocumento>();
			foreach (DataRow fila in dataTable.Rows)
			{
				Modelo.TipoDocumento tipodocumento = new Modelo.TipoDocumento();
				tipodocumento.FromDataRow(fila);
				listadocumentos.Add(tipodocumento);
			}

			return listadocumentos;
		}

		public static Modelo.TipoDocumento ObtenerTipoDocumento(int tipodocumentoId)
		{

			string SELECTSentence = "SELECT TipoDocumento.TipoDocumentoId, TipoDocumento.Descripcion, TipoDocumento.EstaActivo";
			string FROMSentence = " FROM TipoDocumento";
			string WHERESentence = " WHERE TipoDocumento.TipoDocumentoId = " + tipodocumentoId.ToString() ;
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
			string SETSentence = " SET TipoDocumento.Descripcion = '{1}'";
			string WHERESentence = " WHERE TipoDocumento.TipoDocumentoId = {0}";

			string SQLSentence = UPDATESentence + SETSentence + WHERESentence;
			StringBuilder builder = new StringBuilder();
			builder.AppendFormat(SQLSentence, tipodocumento.Tipodocumentoid, tipodocumento.Descripcion);

			return DataBase.ExecuteNonQuery(builder.ToString()) > 0;
		}

		public static bool EstaActivo(int Id)
		{
			string UPDATESentence = "UPDATE TipoDocumento";
			string SETSentence = " SET TipoDocumento.EstaActivo = CASE WHEN TipoDocumento.EstaActivo = 1 THEN 0 ELSE 1 END";
			string WHERESentence = " WHERE TipoDocumento.TipoDocumentoId = {0}";
			string SQLSentence = UPDATESentence + SETSentence + WHERESentence;
			StringBuilder builder = new StringBuilder();
			builder.AppendFormat(SQLSentence, Id);
			return DataBase.ExecuteNonQuery(builder.ToString()) > 0;
		}


	}
}
