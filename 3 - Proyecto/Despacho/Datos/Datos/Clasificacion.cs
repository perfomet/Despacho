using Datos.DB;
using System.Collections.Generic;
using System.Data;
using System.Text;
namespace Datos.Datos
{
	public class Clasificacion
	{
		public static List<Modelo.Clasificacion> ObtenerClasificaciones()
		{
			string SELECTSentence = "SELECT Clasificacion.ClasificacionId, Clasificacion.Cantidad, Clasificacion.UnidadMedidaId, Clasificacion.EstaActivo, UnidadMedida.Descripcion";
			string FROMSentence = " FROM Clasificacion INNER JOIN UnidadMedida ON UnidadMedida.UnidadMedidaId = Clasificacion.UnidadMedidaId";
			string WHERESentence = "";
			string ORDERSentence = ";";
			string SQLSentence = SELECTSentence + FROMSentence + WHERESentence + ORDERSentence;
			DataTable dataTable = DataBase.ExecuteReader(SQLSentence);
			List<Modelo.Clasificacion> listaclasificacion = new List<Modelo.Clasificacion>();
			foreach (DataRow fila in dataTable.Rows)
			{
				Modelo.Clasificacion clasificacion = new Modelo.Clasificacion();
				clasificacion.FromDataRow(fila);
				listaclasificacion.Add(clasificacion);
			}

			return listaclasificacion;
		}

		public static Modelo.Clasificacion ObtenerClasificacion(int id)
		{

			string SELECTSentence = "SELECT Clasificacion.ClasificacionId, Clasificacion.Cantidad, Clasificacion.UnidadMedidaId, Clasificacion.EstaActivo, UnidadMedida.Descripcion";
			string FROMSentence = " FROM Clasificacion INNER JOIN UnidadMedida ON UnidadMedida.UnidadMedidaId = Clasificacion.UnidadMedidaId";
			string WHERESentence = " WHERE Clasificacion.ClasificacionId = '" + id.ToString() + "'";
			string ORDERSentence = ";";
			string SQLSentence = SELECTSentence + FROMSentence + WHERESentence + ORDERSentence;
			Modelo.Clasificacion clasificacion = new Modelo.Clasificacion();
			DataTable dataTable = DataBase.ExecuteReader(SQLSentence);

			if (dataTable.Rows.Count > 0)
			{
				DataRow fila = dataTable.Rows[0];
				clasificacion.FromDataRow(fila);
			}

			return clasificacion;
		}

		public static bool Crear(Modelo.Clasificacion clasificacion)
		{
			string INSERTSentence = "INSERT INTO Clasificacion";
			string VALUESSentence = " VALUES({0}, {1}, 1);";
			string SQLSentence = INSERTSentence + VALUESSentence;
			StringBuilder builder = new StringBuilder();
			builder.AppendFormat(SQLSentence, clasificacion.Cantidad, clasificacion.Unidadmedidaid);
			return DataBase.ExecuteNonQuery(builder.ToString()) > 0;
		}

		public static bool Modificar(Modelo.Clasificacion clasificacion)
		{
			string UPDATESentence = "UPDATE Clasificacion";
			string SETSentence = " SET Clasificacion.Cantidad = {1}, Clasificacion.UnidadMedidaId = {2}";
			string WHERESentence = " WHERE Clasificacion.ClasificacionId = {0}";
			string SQLSentence = UPDATESentence + SETSentence + WHERESentence;
			StringBuilder builder = new StringBuilder();
			builder.AppendFormat(SQLSentence, clasificacion.Clasificacionid, clasificacion.Cantidad, clasificacion.Unidadmedidaid);

			return DataBase.ExecuteNonQuery(builder.ToString()) > 0;
		}

		public static bool EstaActivo(int Id)
		{
			string UPDATESentence = "UPDATE Clasificacion";
			string SETSentence = " SET Clasificacion.EstaActivo = CASE WHEN Clasificacion.EstaActivo = 1 THEN 0 ELSE 1 END";
			string WHERESentence = " WHERE Clasificacion.ClasificacionId = {0}";
			string SQLSentence = UPDATESentence + SETSentence + WHERESentence;
			StringBuilder builder = new StringBuilder();
			builder.AppendFormat(SQLSentence, Id);
			return DataBase.ExecuteNonQuery(builder.ToString()) > 0;
		}
	}
}
