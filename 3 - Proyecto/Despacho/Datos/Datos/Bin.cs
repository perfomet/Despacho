using Datos.DB;
using System.Collections.Generic;
using System.Data;

namespace Datos.Datos
{
	public class Bin
	{
		public static List<Modelo.Bin> ObtenerBins(int clienteId)
		{
			string SELECTSentence = "SELECT DISTINCT E.Bin";
			string FROMSentence = " FROM Existencia E INNER JOIN Cliente C ON C.Codigo = E.Propietario";
			string WHERESentence = clienteId > 0 ? (" WHERE C.ClienteId = " + clienteId) : "";
			string ORDERSentence = " ORDER BY E.Bin;";
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

		public static List<Modelo.Bin> ObtenerBins(string codigoBodega, int clienteId)
		{
			string SELECTSentence = "SELECT DISTINCT E.Bin";
			string FROMSentence = " FROM Existencia E INNER JOIN Cliente C ON C.Codigo = E.Propietario";
			string WHERESentence = " WHERE E.Bodega = '" + codigoBodega + "'" + (clienteId > 0 ? (" AND C.ClienteId = " + clienteId) : "");
			string ORDERSentence = " ORDER BY E.Bin;";
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
