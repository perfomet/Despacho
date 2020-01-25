using Datos.DB;
using System.Collections.Generic;
using System.Data;
using System.Text;

namespace Datos.Datos
{
	public class Camion
	{
		static string SELECTSentence = "SELECT Camion.Patente AS patente, Camion.Descripcion AS descripcion, EmpresaTransporte.Nombre AS empresatransporte, EmpresaTransporte.EsPropia AS espropia";
		static string FROMSentence = " FROM Camion INNER JOIN EmpresaTransporte ON Camion.EmpresaTransporteId = EmpresaTransporte.EmpresaTransporteId";
		static string WHERESentence = "";
		static string SQLSentence = SELECTSentence + FROMSentence + WHERESentence;

		public static List<Modelo.Camion> ObtenerCamiones()
		{
			DataTable dataTable = DataBase.ExecuteReader(SQLSentence);
			List<Modelo.Camion> camiones = new List<Modelo.Camion>();

			foreach (DataRow fila in dataTable.Rows)
			{
				Modelo.Camion camion = new Modelo.Camion();
				camion.FromDataRow(fila);
				camiones.Add(camion);
			}

			return camiones;
		}

		public static Modelo.Camion ObtenerCamion(string Patente)
		{
			Modelo.Camion camion = new Modelo.Camion();
			WHERESentence = " WHERE (patente LIKE '" + Patente + "')";
			SQLSentence = SELECTSentence + FROMSentence + WHERESentence;
			DataTable dataTable = DataBase.ExecuteReader(SQLSentence);
			if (dataTable.Rows.Count > 0)
			{
				DataRow fila = dataTable.Rows[0];
				camion.FromDataRow(fila);
			}

			return camion;
		}
		public static bool Crear(Modelo.Camion camion)
		{
			StringBuilder builder = new StringBuilder();

			builder.AppendFormat("INSERT INTO Camion VALUES ('{0}', '{1}', '{2}')", camion.patente, camion.descripcion, camion.empresatransporte);

			return DataBase.ExecuteNonQuery(builder.ToString()) > 0;
		}

		public static bool Modificar(Modelo.Camion camion)
		{
			StringBuilder builder = new StringBuilder();

			builder.AppendFormat("UPDATE Camion SET patente = '{0}', Descripcion = '{1}',  EmpresaTransporteId = {2} WHERE Patente LIKE '{0}'", camion.patente, camion.descripcion, camion.empresatransporte);

			return DataBase.ExecuteNonQuery(builder.ToString()) > 0;
		}

		public static bool Eliminar(string patente)
		{
			StringBuilder builder = new StringBuilder();
			builder.AppendFormat("DELETE FROM Camion WHERE Patente LIKE '{0}'", patente);

			return DataBase.ExecuteNonQuery(builder.ToString()) > 0;
		}
	}
}

