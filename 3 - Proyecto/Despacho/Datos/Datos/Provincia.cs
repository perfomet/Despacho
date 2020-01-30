using Datos.DB;
using System.Collections.Generic;
using System.Data;
using System.Text;

namespace Datos.Datos
{
	public class Provincia
	{
		public static List<Modelo.Provincia> ObtenerProvincias()
		{
			DataTable dataTable = DataBase.ExecuteReader("SELECT * FROM Provincia");
			List<Modelo.Provincia> provincias = new List<Modelo.Provincia>();

			foreach (DataRow fila in dataTable.Rows)
			{
				Modelo.Provincia provincia = new Modelo.Provincia();

				provincia.FromDataRow(fila);

				provincias.Add(provincia);
			}

			return provincias;
		}

		public static List<Modelo.Provincia> ObtenerProvincias(int regionId)
		{
			DataTable dataTable = DataBase.ExecuteReader("SELECT * FROM Provincia WHERE RegionId = " + regionId);
			List<Modelo.Provincia> provincias = new List<Modelo.Provincia>();

			foreach (DataRow fila in dataTable.Rows)
			{
				Modelo.Provincia provincia = new Modelo.Provincia();

				provincia.FromDataRow(fila);

				provincias.Add(provincia);
			}

			return provincias;
		}

		public static bool EstaActivo(int Id)
		{
			string UPDATESentence = "UPDATE Provincia";
			string SETSentence = " SET EstaActivo = CASE WHEN EstaActivo = 1 THEN 0 ELSE 1 END";
			string WHERESentence = " WHERE ProvinciaId = {0}";
			string SQLSentence = UPDATESentence + SETSentence + WHERESentence;
			StringBuilder builder = new StringBuilder();
			builder.AppendFormat(SQLSentence, Id);
			return DataBase.ExecuteNonQuery(builder.ToString()) > 0;
		}
		public static Modelo.Provincia ObtenerProvincia(int provinciaId)
		{
			Modelo.Provincia provincia = new Modelo.Provincia();

			DataTable dataTable = DataBase.ExecuteReader("SELECT * FROM Provincia WHERE ProvinciaId = " + provinciaId);

			if (dataTable.Rows.Count > 0)
			{
				DataRow fila = dataTable.Rows[0];
				provincia.FromDataRow(fila);
			}

			return provincia;
		}
	}
}
