using Datos.DB;
using System.Collections.Generic;
using System.Data;
using System.Text;

namespace Datos.Datos
{
	public class Camion
	{
		
		
		public static List<Modelo.Camion> ObtenerCamiones()
		{
			string SELECTSentence = "SELECT Camion.Patente AS Patente, Camion.Descripcion AS Descripcion, EmpresaTransporte.Nombre AS EmpresaTransporte, EmpresaTransporte.EsPropia AS EsPropia, Camion.EstaActivo AS EstaActivo";
			string FROMSentence = " FROM Camion INNER JOIN EmpresaTransporte ON Camion.EmpresaTransporteId = EmpresaTransporte.EmpresaTransporteId";
			string WHERESentence = "";
			string SQLSentence = SELECTSentence + FROMSentence + WHERESentence;

			DataTable dataTable = DataBase.ExecuteReader(SQLSentence);
			List<Modelo.Camion> listacamiones = new List<Modelo.Camion>();

			foreach (DataRow fila in dataTable.Rows)
			{
				Modelo.Camion camion = new Modelo.Camion();
				camion.FromDataRow(fila);
				listacamiones.Add(camion);
			}

			return listacamiones;
		}

		public static Modelo.Camion ObtenerCamion(string Patente)
		{
			string SELECTSentence = "SELECT Camion.Patente AS Patente, Camion.Descripcion AS Descripcion, EmpresaTransporte.Nombre AS EmpresaTransporte, EmpresaTransporte.EsPropia AS EsPropia, Camion.EsTaActivo AS EstaActivo";
			string FROMSentence = " FROM Camion INNER JOIN EmpresaTransporte ON Camion.EmpresaTransporteId = EmpresaTransporte.EmpresaTransporteId";
			
			string WHERESentence = " WHERE (Camion.Patente LIKE '" + Patente + "')";
			string SQLSentence = SELECTSentence + FROMSentence + WHERESentence;
			Modelo.Camion camion = new Modelo.Camion();
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
			string INSERTSentence = "INSERT INTO Camion";
			string VALUESSentence = " VALUES ('{0}', '{1}', '{2}', 1)";
			string SQLSentence = INSERTSentence + VALUESSentence;
			builder.AppendFormat(SQLSentence, camion.patente, camion.descripcion, camion.empresatransporte);

			return DataBase.ExecuteNonQuery(builder.ToString()) > 0;
		}

		public static bool Modificar(Modelo.Camion camion)
		{
			StringBuilder builder = new StringBuilder();
			string UPDATESentence = "UPDATE Camion";
			string SETSentence = " SET patente = '{0}', Descripcion = '{1}',  EmpresaTransporteId = {2}";
			string WHERESentence = " WHERE Patente LIKE '{0}'";
			string SQLSentence = UPDATESentence + SETSentence + WHERESentence;
			builder.AppendFormat(SQLSentence  , camion.patente, camion.descripcion, camion.empresatransporte);

			return DataBase.ExecuteNonQuery(builder.ToString()) > 0;
		}

		public static bool EstaActivo(int Id)
		{
			string UPDATESentence = "UPDATE Camion";
			string SETSentence = " SET EstaActivo = CASE WHEN EstaActivo = 1 THEN 0 ELSE 1 END";
			string WHERESentence = " WHERE CamionId = {0}";
			string SQLSentence = UPDATESentence + SETSentence + WHERESentence;
			StringBuilder builder = new StringBuilder();
			builder.AppendFormat(SQLSentence, Id);
			return DataBase.ExecuteNonQuery(builder.ToString()) > 0;
		}
		
	}
}

