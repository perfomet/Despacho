using Datos.DB;
using System.Collections.Generic;
using System.Data;

namespace Datos.Datos
{
	public class ModeloExistencia
	{

		public static List<Modelo.ModeloExistencia> ObtenerModelosExistencias(string marca, int clienteId)
		{
			string SELECTSentence = "SELECT DISTINCT E.Modelo";
			string FROMSentence = " FROM Existencia E INNER JOIN Cliente C ON C.Codigo = E.Propietario";
			string WHERESentence = (marca.Equals(string.Empty) ? "" : (" WHERE E.Marca = '" + marca + "'")) + (clienteId > 0 ? (" AND C.ClienteId = " + clienteId) : "");
			string ORDERSentence = " ORDER BY E.Modelo;";
			string SQLSentence = SELECTSentence + FROMSentence + WHERESentence + ORDERSentence;

			DataTable dataTable = DataBase.ExecuteReader(SQLSentence);
			List<Modelo.ModeloExistencia> modelos = new List<Modelo.ModeloExistencia>();

			foreach (DataRow fila in dataTable.Rows)
			{
				Modelo.ModeloExistencia modelo = new Modelo.ModeloExistencia();
				modelo.FromDataRow(fila);
				modelos.Add(modelo);
			}

			return modelos;
		}

		public static Modelo.ModeloExistencia ObtenerModeloExistencia(string codigo)
		{

			string SELECTSentence = "SELECT Modelo";
			string FROMSentence = " FROM Existencia";
			string WHERESentence = " WHERE Modelo = '" + codigo + "'";
			string ORDERSentence = " ORDER BY Modelo;";
			string SQLSentence = SELECTSentence + FROMSentence + WHERESentence + ORDERSentence;

			Modelo.ModeloExistencia modelo = new Modelo.ModeloExistencia();
			DataTable dataTable = DataBase.ExecuteReader(SQLSentence);

			if (dataTable.Rows.Count > 0)
			{
				DataRow fila = dataTable.Rows[0];
				modelo.FromDataRow(fila);
			}

			return modelo;
		}
	}
}
