using Datos.DB;
using System.Collections.Generic;
using System.Data;

namespace Datos.Datos
{
	public class Existencia
	{

		public static List<Modelo.Existencia> ObtenerExistencias(int clienteId)
		{
			string SELECTSentence = "SELECT E.*, C.Nombre";
			string FROMSentence = " FROM " + _librerias.LibConfig.DbExistencias + ".dbo.Existencia E INNER JOIN Cliente C ON C.Codigo = E.Propietario";
			string WHERESentence = clienteId > 0 ? (" WHERE C.ClienteId = " + clienteId) : "";
			string ORDERSentence = ";";
			string SQLSentence = SELECTSentence + FROMSentence + WHERESentence + ORDERSentence;
			DataTable dataTable = DataBase.ExecuteReader(SQLSentence);
			List<Modelo.Existencia> existencias = new List<Modelo.Existencia>();
			foreach (DataRow fila in dataTable.Rows)
			{
				Modelo.Existencia existencia = new Modelo.Existencia();
				existencia.FromDataRow(fila);
				existencias.Add(existencia);
			}

			return existencias;
		}

		public static Modelo.Existencia ObtenerExistencia(string serie)
		{
			string SELECTSentence = "SELECT E.*, C.Nombre";
			string FROMSentence = " FROM " + _librerias.LibConfig.DbExistencias + ".dbo.Existencia E INNER JOIN Cliente C ON C.Codigo = E.Propietario";
			string WHERESentence = " WHERE E.Serie = '" + serie + "'";
			string ORDERSentence = ";";
			string SQLSentence = SELECTSentence + FROMSentence + WHERESentence + ORDERSentence;
			Modelo.Existencia existencia = new Modelo.Existencia();
			DataTable dataTable = DataBase.ExecuteReader(SQLSentence);

			if (dataTable.Rows.Count > 0)
			{
				DataRow fila = dataTable.Rows[0];
				existencia.FromDataRow(fila);
			}

			return existencia;
		}
	}
}
