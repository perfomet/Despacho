using Datos.DB;
using System.Collections.Generic;
using System.Data;
using System.Text;

namespace Datos.Datos
{
	public class Region
	{
		public static List<Modelo.Region> ObtenerRegiones()
		{
			string SELECTSentence = "SELECT *";
			string FROMSentence = " FROM Region";
			string SQLSentence = SELECTSentence + FROMSentence;
			DataTable dataTable = DataBase.ExecuteReader(SQLSentence);
			List<Modelo.Region> regiones = new List<Modelo.Region>();

			foreach (DataRow fila in dataTable.Rows)
			{
				Modelo.Region region = new Modelo.Region();

				region.FromDataRow(fila);

				regiones.Add(region);
			}

			return regiones;
		}

		public static Modelo.Region ObtenerRegion(int regionId)
		{

			Modelo.Region region = new Modelo.Region();
			string SELECTSentence = "SELECT *";
			string FROMSentence = " FROM Region";
			string WHERESentence = " WHERE RegionId = " + regionId.ToString();
			string SQLSentence = SELECTSentence + FROMSentence + WHERESentence;
			DataTable dataTable = DataBase.ExecuteReader(SQLSentence);

			if (dataTable.Rows.Count > 0)
			{
				DataRow fila = dataTable.Rows[0];
				region.FromDataRow(fila);
			}

			return region;
		}
		public static bool EstaActivo(int Id)
		{
			string UPDATESentence = "UPDATE Region";
			string SETSentence = " SET EstaActivo = CASE WHEN EstaActivo = 1 THEN 0 ELSE 1 END";
			string WHERESentence = " WHERE RegionId = {0}";
			string SQLSentence = UPDATESentence + SETSentence + WHERESentence;
			StringBuilder builder = new StringBuilder();
			builder.AppendFormat(SQLSentence, Id);
			return DataBase.ExecuteNonQuery(builder.ToString()) > 0;
		}
	}
}
