using Datos.DB;
using System.Collections.Generic;
using System.Data;


namespace Datos.Datos
{
	public class TipoPersonal
	{
		public static List<Modelo.TipoPersonal> ObtenerTiposdePersonal()
		{
			string SELECTSentence = "SELECT TipoPersonal.*";
			string FROMSentence = " FROM TipoPersonal";
			string WHERESentence = "";
			string ORDERSentence = " ORDER BY TipoPersonal.Descripcion;";
			string SQLSentence = SELECTSentence + FROMSentence + WHERESentence + ORDERSentence;

			DataTable dataTable = DataBase.ExecuteReader(SQLSentence);
			List<Modelo.TipoPersonal> listatipopersonal = new List<Modelo.TipoPersonal>();

			foreach (DataRow fila in dataTable.Rows)
			{
				Modelo.TipoPersonal tipopersonal = new Modelo.TipoPersonal();
				tipopersonal.FromDataRow(fila);
				listatipopersonal.Add(tipopersonal);
			}

			return listatipopersonal;
		}

		public static Modelo.TipoPersonal ObtenerTipoPersonal(int id)
		{
			string SELECTSentence = "SELECT TipoPersonal.*";
			string FROMSentence = " FROM TipoPersonal";
			string WHERESentence = " WHERE (TipoPersonal.TipoPersonalId = " + id.ToString() + ")";
			string SQLSentence = SELECTSentence + FROMSentence + WHERESentence;
			Modelo.TipoPersonal tipopersonal = new Modelo.TipoPersonal();
			DataTable dataTable = DataBase.ExecuteReader(SQLSentence);
			if (dataTable.Rows.Count > 0)
			{
				DataRow fila = dataTable.Rows[0];
				tipopersonal.FromDataRow(fila);
			}

			return tipopersonal;
		}
		

		
	}
}
