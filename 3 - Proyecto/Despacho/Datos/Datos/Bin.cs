using Datos.DB;
using System.Collections.Generic;
using System.Data;

namespace Datos.Datos
{
	public class Bin
	{

		public static List<Modelo.Bin> ObtenerBins(string codigoBodega)
		{
			string SELECTSentence = "SELECT DISTINCT Bin";
			string FROMSentence = " FROM Existencia";
			string WHERESentence = " WHERE Bodega = '" + codigoBodega + "'";
			string ORDERSentence = ";";
			string SQLSentence = SELECTSentence + FROMSentence + WHERESentence + ORDERSentence;

			DataTable dataTable = DataBase.ExecuteReader(SQLSentence);
			List<Modelo.Bin> bins = new List<Modelo.Bin>();

			foreach (DataRow fila in dataTable.Rows)
			{
				Modelo.Bin bin = new Modelo.Bin();
				bin.FromDataRow(fila);
				bins.Add(bin);
			}

			return bins;
		}

		public static Modelo.Bin ObtenerBin(string codigo)
		{

			string SELECTSentence = "SELECT Bin";
			string FROMSentence = " FROM Existencia";
			string WHERESentence = " WHERE Bin = '" + codigo + "'";
			string ORDERSentence = ";";
			string SQLSentence = SELECTSentence + FROMSentence + WHERESentence + ORDERSentence;

			Modelo.Bin bin = new Modelo.Bin();
			DataTable dataTable = DataBase.ExecuteReader(SQLSentence);

			if (dataTable.Rows.Count > 0)
			{
				DataRow fila = dataTable.Rows[0];
				bin.FromDataRow(fila);
			}

			return bin;
		}
	}
}
