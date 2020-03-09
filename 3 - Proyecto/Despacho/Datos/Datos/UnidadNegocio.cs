using Datos.DB;
using System.Collections.Generic;
using System.Data;
using System.Text;

namespace Datos.Datos
{
	public class UnidadNegocio
	{

		public static List<Modelo.UnidadNegocio> ObtenerUnidadesNegocio(int clienteId)
		{
			string SELECTSentence = "SELECT UnidadNegocio.UnidadNegocioId, UnidadNegocio.Descripcion, UnidadNegocio.ClienteId, Cliente.Nombre As Clientenombre, UnidadNegocio.EstaActivo";
			string FROMSentence = " FROM Cliente INNER JOIN UnidadNegocio ON Cliente.ClienteId = UnidadNegocio.ClienteId";
			string WHERESentence = clienteId > 0 ? (" WHERE Cliente.ClienteId = " + clienteId) : "";
			string ORDERSentence = ";";
			string SQLSentence = SELECTSentence + FROMSentence + WHERESentence + ORDERSentence;
			DataTable dataTable = DataBase.ExecuteReader(SQLSentence);
			List<Modelo.UnidadNegocio> unidadesdenegocios = new List<Modelo.UnidadNegocio>();
			foreach (DataRow fila in dataTable.Rows)
			{
				Modelo.UnidadNegocio unidadnegocio = new Modelo.UnidadNegocio();
				unidadnegocio.FromDataRow(fila);
				unidadesdenegocios.Add(unidadnegocio);
			}

			return unidadesdenegocios;
		}

		public static Modelo.UnidadNegocio ObtenerUnidadNegocio(int unidadnegocioId)
		{

			string SELECTSentence = "SELECT UnidadNegocio.UnidadNegocioId, UnidadNegocio.Descripcion, UnidadNegocio.ClienteId, Cliente.Nombre As Clientenombre, UnidadNegocio.EstaActivo";
			string FROMSentence = " FROM Cliente INNER JOIN UnidadNegocio ON Cliente.ClienteId = UnidadNegocio.ClienteId";
			string WHERESentence = " WHERE UnidadNegocioId = " + unidadnegocioId.ToString();
			string ORDERSentence = ";";
			string SQLSentence = SELECTSentence + FROMSentence + WHERESentence + ORDERSentence;
			Modelo.UnidadNegocio unidadnegocio = new Modelo.UnidadNegocio();
			DataTable dataTable = DataBase.ExecuteReader(SQLSentence);

			if (dataTable.Rows.Count > 0)
			{
				DataRow fila = dataTable.Rows[0];
				unidadnegocio.FromDataRow(fila);
			}

			return unidadnegocio;
		}

		public static Modelo.UnidadNegocio ObtenerUnidadNegocio(string nombreUnidadNegocio)
		{

			string SELECTSentence = "SELECT UnidadNegocio.UnidadNegocioId, UnidadNegocio.Descripcion, UnidadNegocio.ClienteId, Cliente.Nombre As Clientenombre, UnidadNegocio.EstaActivo";
			string FROMSentence = " FROM Cliente INNER JOIN UnidadNegocio ON Cliente.ClienteId = UnidadNegocio.ClienteId";
			string WHERESentence = " WHERE UnidadNegocio.Descripcion = '" + nombreUnidadNegocio + "'";
			string ORDERSentence = ";";
			string SQLSentence = SELECTSentence + FROMSentence + WHERESentence + ORDERSentence;
			Modelo.UnidadNegocio unidadnegocio = new Modelo.UnidadNegocio();
			DataTable dataTable = DataBase.ExecuteReader(SQLSentence);

			if (dataTable.Rows.Count > 0)
			{
				DataRow fila = dataTable.Rows[0];
				unidadnegocio.FromDataRow(fila);
			}

			return unidadnegocio;
		}

		public static bool Crear(Modelo.UnidadNegocio unidadnegocio)
		{
			string INSERTSentence = "INSERT INTO UnidadNegocio";
			string VALUESSentence = " VALUES('{0}', {1}, 1);";
			string SQLSentence = INSERTSentence + VALUESSentence;
			StringBuilder builder = new StringBuilder();
			builder.AppendFormat(SQLSentence, unidadnegocio.Descripcion, unidadnegocio.ClienteId);
			return DataBase.ExecuteNonQuery(builder.ToString()) > 0;
		}

		public static bool Modificar(Modelo.UnidadNegocio unidadnegocio)
		{
			string UPDATESentence = "UPDATE UnidadNegocio";
			string SETSentence = " SET Descripcion = '{1}', ClienteId = {2}";
			string WHERESentence = " WHERE UnidadNegocioId = {0}";

			string SQLSentence = UPDATESentence + SETSentence + WHERESentence;
			StringBuilder builder = new StringBuilder();
			builder.AppendFormat(SQLSentence, unidadnegocio.UnidadNegocioId, unidadnegocio.Descripcion, unidadnegocio.ClienteId);

			return DataBase.ExecuteNonQuery(builder.ToString()) > 0;
		}

		public static bool EstaActivo(int clienteId)
		{
			string UPDATESentence = "UPDATE UnidadNegocio";
			string SETSentence = " SET EstaActivo = CASE WHEN EstaActivo = 1 THEN 0 ELSE 1 END";
			string WHERESentence = " WHERE UnidadNegocioId = {0}";
			string SQLSentence = UPDATESentence + SETSentence + WHERESentence;
			StringBuilder builder = new StringBuilder();
			builder.AppendFormat(SQLSentence, clienteId);
			return DataBase.ExecuteNonQuery(builder.ToString()) > 0;
		}


	}
}
