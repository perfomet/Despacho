using Datos.DB;
using System.Collections.Generic;
using System.Data;
using System.Text;

namespace Datos.Datos
{
	public static class SeccionFormulario
	{
		public static List<Modelo.SeccionFormulario> ObtenerSeccionesFormulario()
		{
			string SELECTSentence = "SELECT *";
			string FROMSentence = " FROM SeccionFormulario";
			string WHERESentence = "";
			string ORDERSentence = ";";
			string SQLSentence = SELECTSentence + FROMSentence + WHERESentence + ORDERSentence;
			DataTable dataTable = DataBase.ExecuteReader(SQLSentence);
			List<Modelo.SeccionFormulario> secciones = new List<Modelo.SeccionFormulario>();
			foreach (DataRow fila in dataTable.Rows)
			{
				Modelo.SeccionFormulario seccion = new Modelo.SeccionFormulario();
				seccion.FromDataRow(fila);
				secciones.Add(seccion);
			}

			return secciones;
		}

		public static Modelo.SeccionFormulario ObtenerSeccionFormulario(int Id)
		{
			string SELECTSentence = "SELECT *";
			string FROMSentence = " FROM SeccionFormulario";
			string WHERESentence = " WHERE SeccionFormularioId = '" + Id.ToString() + "'";
			string ORDERSentence = ";";
			string SQLSentence = SELECTSentence + FROMSentence + WHERESentence + ORDERSentence;
			Modelo.SeccionFormulario seccion = new Modelo.SeccionFormulario();
			DataTable dataTable = DataBase.ExecuteReader(SQLSentence);

			if (dataTable.Rows.Count > 0)
			{
				DataRow fila = dataTable.Rows[0];
				seccion.FromDataRow(fila);
			}

			return seccion;
		}

		public static bool Crear(Modelo.SeccionFormulario seccion)
		{
			string INSERTSentence = "INSERT INTO SeccionFormulario";
			string VALUESSentence = " VALUES('{0}', '{1}', '{2}', 1);";
			string SQLSentence = INSERTSentence + VALUESSentence;
			StringBuilder builder = new StringBuilder();

			builder.AppendFormat(SQLSentence, seccion.Descripcion, seccion.Clase, seccion.Vista);
			return DataBase.ExecuteNonQuery(builder.ToString()) > 0;
		}

		public static bool Modificar(Modelo.SeccionFormulario seccion)
		{
			string UPDATESentence = "UPDATE SeccionFormulario";
			string SETSentence = " SET Descripcion = '{1}', Clase = '{2}', Vista = '{3}'";
			string WHERESentence = " WHERE SeccionFormularioId = {0}";

			string SQLSentence = UPDATESentence + SETSentence + WHERESentence;
			StringBuilder builder = new StringBuilder();
			builder.AppendFormat(SQLSentence, seccion.SeccionFormularioId, seccion.Descripcion, seccion.Clase, seccion.Vista);

			return DataBase.ExecuteNonQuery(builder.ToString()) > 0;
		}

		public static bool EstaActivo(int Id)
		{
			string UPDATESentence = "UPDATE SeccionFormulario";
			string SETSentence = " SET EstaActivo = CASE WHEN EstaActivo = 1 THEN 0 ELSE 1 END";
			string WHERESentence = " WHERE SeccionFormularioId = {0}";
			string SQLSentence = UPDATESentence + SETSentence + WHERESentence;
			StringBuilder builder = new StringBuilder();
			builder.AppendFormat(SQLSentence, Id);
			return DataBase.ExecuteNonQuery(builder.ToString()) > 0;
		}
	}
}
