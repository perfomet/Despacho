using Datos.DB;
using System.Collections.Generic;
using System.Data;
using System.Text;
namespace Datos.Datos
{
	public class Prioridad
{
	public static List<Modelo.Prioridad> ObtenerPrioridades()
	{
		string SELECTSentence = "SELECT *";
		string FROMSentence = " FROM Prioridad";
		string WHERESentence = "";
		string ORDERSentence = ";";
		string SQLSentence = SELECTSentence + FROMSentence + WHERESentence + ORDERSentence;
		DataTable dataTable = DataBase.ExecuteReader(SQLSentence);
		List<Modelo.Prioridad> prioridades = new List<Modelo.Prioridad>();
		foreach (DataRow fila in dataTable.Rows)
		{
			Modelo.Prioridad prioridad = new Modelo.Prioridad();
			prioridad.FromDataRow(fila);
			prioridades.Add(prioridad);
		}

		return prioridades;
	}

	public static Modelo.Prioridad ObtenerPrioridad(int Id)
	{

		string SELECTSentence = "SELECT *";
		string FROMSentence = " FROM Prioridad";
		string WHERESentence = " WHERE Prioridad.PrioridadId = '" + Id.ToString() + "'";
		string ORDERSentence = ";";
		string SQLSentence = SELECTSentence + FROMSentence + WHERESentence + ORDERSentence;
		Modelo.Prioridad prioridad = new Modelo.Prioridad();
		DataTable dataTable = DataBase.ExecuteReader(SQLSentence);

		if (dataTable.Rows.Count > 0)
		{
			DataRow fila = dataTable.Rows[0];
			prioridad.FromDataRow(fila);
		}

		return prioridad;
	}

	public static bool Crear(Modelo.Prioridad prioridad)
	{
		string INSERTSentence = "INSERT INTO Prioridad.Prioridad";
		string VALUESSentence = " VALUES('{1}', 1});";
		string SQLSentence = INSERTSentence + VALUESSentence;
		StringBuilder builder = new StringBuilder();
		builder.AppendFormat(SQLSentence, prioridad.descripcion);
		return DataBase.ExecuteNonQuery(builder.ToString()) > 0;
	}

	public static bool Modificar(Modelo.Prioridad prioridad)
	{
		string UPDATESentence = "UPDATE Prioridad";
		string SETSentence = " SET Prioridad.Descripcion = '{1}'";
		string WHERESentence = " WHERE Prioridad.PrioridadId = {0}";

		string SQLSentence = UPDATESentence + SETSentence + WHERESentence;
		StringBuilder builder = new StringBuilder();
		builder.AppendFormat(SQLSentence, prioridad.prioridadid, prioridad.descripcion);

		return DataBase.ExecuteNonQuery(builder.ToString()) > 0;
	}

		public static bool EstaActivo(int Id)
		{
			string UPDATESentence = "UPDATE Prioridad";
			string SETSentence = " SET Prioridad.EstaActivo = CASE WHEN Prioridad.EstaActivo = 1 THEN 0 ELSE 1 END";
			string WHERESentence = " WHERE Prioridad.PrioridadId = {0}";
			string SQLSentence = UPDATESentence + SETSentence + WHERESentence;
			StringBuilder builder = new StringBuilder();
			builder.AppendFormat(SQLSentence, Id);
			return DataBase.ExecuteNonQuery(builder.ToString()) > 0;
		}
	}
}
