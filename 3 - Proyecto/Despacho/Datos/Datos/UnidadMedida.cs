using Datos.DB;
using System.Collections.Generic;
using System.Data;
using System.Text;

namespace Datos.Datos
{
	public static class UnidadMedida
	{
		public static List<Modelo.UnidadMedida> ObtenerUnidadesdeMedida()
		{
			string SELECTSentence = "SELECT *";
			string FROMSentence = " FROM UnidadMedida";
			string WHERESentence = "";
			string ORDERSentence = ";";
			string SQLSentence = SELECTSentence + FROMSentence + WHERESentence + ORDERSentence;
			DataTable dataTable = DataBase.ExecuteReader(SQLSentence);
			List<Modelo.UnidadMedida> unidadesdemedidas = new List<Modelo.UnidadMedida>();
			foreach (DataRow fila in dataTable.Rows)
			{
				Modelo.UnidadMedida unidadmedida = new Modelo.UnidadMedida();
				unidadmedida.FromDataRow(fila);
				unidadesdemedidas.Add(unidadmedida);
			}

			return unidadesdemedidas;
		}

		public static Modelo.UnidadMedida ObtenerUnidadMedida(int Id)
		{

			string SELECTSentence = "SELECT *";
			string FROMSentence = " FROM UnidadMedida";
			string WHERESentence = " WHERE TipoSolicitudId = '" + Id.ToString() + "'";
			string ORDERSentence = ";";
			string SQLSentence = SELECTSentence + FROMSentence + WHERESentence + ORDERSentence;
			Modelo.UnidadMedida unidadmedida = new Modelo.UnidadMedida();
			DataTable dataTable = DataBase.ExecuteReader(SQLSentence);

			if (dataTable.Rows.Count > 0)
			{
				DataRow fila = dataTable.Rows[0];
				unidadmedida.FromDataRow(fila);
			}

			return unidadmedida;
		}

		public static bool Crear(Modelo.UnidadMedida unidadmedida)
		{
			string INSERTSentence = "INSERT INTO UnidadMedida";
			string VALUESSentence = " VALUES('{1}');";
			string SQLSentence = INSERTSentence + VALUESSentence;
			StringBuilder builder = new StringBuilder();

			builder.AppendFormat(SQLSentence, unidadmedida.Descripcion);
			return DataBase.ExecuteNonQuery(builder.ToString()) > 0;
		}

		public static bool Modificar(Modelo.UnidadMedida unidadmedida)
		{
			string UPDATESentence = "UPDATE UnidadMedida";
			string SETSentence = " SET Descripcion = '{1}'";
			string WHERESentence = " WHERE UnidadMedidaId = {0}";
			string SQLSentence = UPDATESentence + SETSentence + WHERESentence;
			StringBuilder builder = new StringBuilder();
			builder.AppendFormat(SQLSentence, unidadmedida.UnidadMedidaId, unidadmedida.Descripcion);

			return DataBase.ExecuteNonQuery(builder.ToString()) > 0;
		}

		public static bool Eliminar(int Id)
		{
			string DELETESentence = "DELETE";
			string FROMSentence = " FROM UnidadMedida";
			string WHERESentence = " WHERE UnidadMedidaId = {0}";
			string SQLSentence = DELETESentence + FROMSentence + WHERESentence;
			StringBuilder builder = new StringBuilder();
			builder.AppendFormat(SQLSentence, Id);
			return DataBase.ExecuteNonQuery(builder.ToString()) > 0;
		}
	}
}
