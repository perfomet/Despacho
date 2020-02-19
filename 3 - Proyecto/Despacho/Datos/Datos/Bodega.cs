using Datos.DB;
using System.Collections.Generic;
using System.Data;

namespace Datos.Datos
{
	public class Bodega
	{

		public static List<Modelo.Bodega> ObtenerBodegas(int clienteId)
		{
			string SELECTSentence = "SELECT DISTINCT E.Bodega, E.NomBodega";
			string FROMSentence = " FROM Existencia E INNER JOIN Cliente C ON C.Codigo = E.Propietario";
			string WHERESentence = clienteId > 0 ? (" WHERE C.ClienteId = " + clienteId) : "";
			string ORDERSentence = " ORDER BY E.NomBodega;";
			string SQLSentence = SELECTSentence + FROMSentence + WHERESentence + ORDERSentence;

			DataTable dataTable = DataBase.ExecuteReader(SQLSentence);
			List<Modelo.Bodega> bodegas = new List<Modelo.Bodega>();

			foreach (DataRow fila in dataTable.Rows)
			{
				Modelo.Bodega bodega = new Modelo.Bodega();
				bodega.FromDataRow(fila);
				bodegas.Add(bodega);
			}

			return bodegas;
		}

		public static Modelo.Bodega ObtenerBodega(string codigo)
		{

			string SELECTSentence = "SELECT DISTINCT Bodega, NomBodega";
			string FROMSentence = " FROM Existencia";
			string WHERESentence = " WHERE Bodega = '" + codigo + "'";
			string ORDERSentence = ";";
			string SQLSentence = SELECTSentence + FROMSentence + WHERESentence + ORDERSentence;

			Modelo.Bodega bodega = new Modelo.Bodega();
			DataTable dataTable = DataBase.ExecuteReader(SQLSentence);

			if (dataTable.Rows.Count > 0)
			{
				DataRow fila = dataTable.Rows[0];
				bodega.FromDataRow(fila);
			}

			return bodega;
		}
	}
}
