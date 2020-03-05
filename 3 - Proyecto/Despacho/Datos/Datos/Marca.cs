using Datos.DB;
using System.Collections.Generic;
using System.Data;

namespace Datos.Datos
{
	public class Marca
	{
		public static List<Modelo.Marca> ObtenerMarcas(int clienteId)
		{
			string SELECTSentence = "SELECT DISTINCT E.Marca";
			string FROMSentence = " FROM " + _librerias.LibConfig.DbExistencias + ".dbo.Existencia E INNER JOIN Cliente C ON C.Codigo = E.Propietario";
			string WHERESentence = clienteId > 0 ? (" AND C.ClienteId = " + clienteId) : "";
			string ORDERSentence = " ORDER BY E.Marca;";
			string SQLSentence = SELECTSentence + FROMSentence + WHERESentence + ORDERSentence;
			DataTable dataTable = DataBase.ExecuteReader(SQLSentence);
			List<Modelo.Marca> marcas = new List<Modelo.Marca>();

			foreach (DataRow fila in dataTable.Rows)
			{
				Modelo.Marca marca = new Modelo.Marca();
				marca.FromDataRow(fila);
				marcas.Add(marca);
			}

			return marcas;
		}

		public static List<Modelo.Marca> ObtenerMarcas(string codigo)
		{
			string SELECTSentence = "SELECT DISTINCT Marca";
			string FROMSentence = " FROM " + _librerias.LibConfig.DbExistencias + ".dbo.Existencia";
			string WHERESentence = " WHERE Marca LIKE '" + codigo + "'";
			string ORDERSentence = ";";
			string SQLSentence = SELECTSentence + FROMSentence + WHERESentence + ORDERSentence;

			DataTable dataTable = DataBase.ExecuteReader(SQLSentence);
			List<Modelo.Marca> marcas = new List<Modelo.Marca>();

			foreach (DataRow fila in dataTable.Rows)
			{
				Modelo.Marca marca = new Modelo.Marca();
				marca.FromDataRow(fila);
				marcas.Add(marca);
			}

			return marcas;
		}

		public static Modelo.Marca ObtenerMarca(string codigo)
		{

			string SELECTSentence = "SELECT Marca";
			string FROMSentence = " FROM " + _librerias.LibConfig.DbExistencias + ".dbo.Existencia";
			string WHERESentence = " WHERE Marca = '" + codigo + "'";
			string ORDERSentence = ";";
			string SQLSentence = SELECTSentence + FROMSentence + WHERESentence + ORDERSentence;

			Modelo.Marca marca = new Modelo.Marca();
			DataTable dataTable = DataBase.ExecuteReader(SQLSentence);

			if (dataTable.Rows.Count > 0)
			{
				DataRow fila = dataTable.Rows[0];
				marca.FromDataRow(fila);
			}

			return marca;
		}
	}
}
