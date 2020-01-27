using Datos.DB;
using System.Collections.Generic;
using System.Data;
using System.Text;

namespace Datos.Datos
{
	public class EmpresaTransporte
	{
		public static List<Modelo.EmpresaTransporte> ObtenerEmpresasdeTransportes()
		{
			string SELECTSentence = "SELECT EmpresaTransporteId, Nombre, EsPropia";
			string FROMSentence = " FROM EmpresaTransporte";
			string WHERESentence = "";
			string SQLSentence = SELECTSentence + FROMSentence + WHERESentence;

			DataTable dataTable = DataBase.ExecuteReader(SQLSentence);
			List<Modelo.EmpresaTransporte> empresastransportes = new List<Modelo.EmpresaTransporte>();

			foreach (DataRow fila in dataTable.Rows)
			{
				Modelo.EmpresaTransporte empresatransporte = new Modelo.EmpresaTransporte();
				empresatransporte.FromDataRow(fila);
				empresastransportes.Add(empresatransporte);
			}

			return empresastransportes;
		}

		public static Modelo.EmpresaTransporte ObtenerEmpresaTransporte(int id)
		{
			string SELECTSentence = "SELECT EmpresaTransporteId, Nombre, EsPropia";
			string FROMSentence = " FROM EmpresaTransporte";
			string WHERESentence = " WHERE (EmpresaTransporteId = " + id.ToString() + ")";
			string SQLSentence = SELECTSentence + FROMSentence + WHERESentence;
			Modelo.EmpresaTransporte empresatransporte = new Modelo.EmpresaTransporte();
			DataTable dataTable = DataBase.ExecuteReader(SQLSentence);
			if (dataTable.Rows.Count > 0)
			{
				DataRow fila = dataTable.Rows[0];
				empresatransporte.FromDataRow(fila);
			}

			return empresatransporte;
		}
		public static bool Crear(Modelo.EmpresaTransporte empresatransporte)
		{
			StringBuilder builder = new StringBuilder();
			string INSERTSentence = "INSERT INTO EmpresaTransporte";
			string VALUESSentence = " VALUES ('{0}', '{1}', '{2}')";
			string SQLSentence = INSERTSentence + VALUESSentence;
			builder.AppendFormat(SQLSentence, empresatransporte.EmpresaTransporteId, empresatransporte.Nombre, empresatransporte.EsPropia);

			return DataBase.ExecuteNonQuery(builder.ToString()) > 0;
		}
		
		 public static bool Modificar(Modelo.EmpresaTransporte empresatransporte)
		{ 			
			string UPDATESentence = "UPDATE EmpresaTransporte";
			string SETSentence = " SET Nombre = '{1}', EsPropia = {2}";
			string WHERESentence = " WHERE EmpresaTransporteId = {0}";
			string SQLSentence = UPDATESentence + SETSentence + WHERESentence;
			StringBuilder builder = new StringBuilder();
			builder.AppendFormat(SQLSentence, empresatransporte.EmpresaTransporteId, empresatransporte.Nombre, empresatransporte.EsPropia);

			return DataBase.ExecuteNonQuery(builder.ToString()) > 0;
		}

		public static bool Eliminar(int id)
		{
			StringBuilder builder = new StringBuilder();
			string DELETESentence = "DELETE";
			string FROMSentence = " FROM EmpresaTransporte";
			string WHERESentence = " WHERE EmpresaTransporteId = {0}";
			string SQLSentence = DELETESentence + FROMSentence + WHERESentence;
			builder.AppendFormat(SQLSentence, id);

			return DataBase.ExecuteNonQuery(builder.ToString()) > 0;
		}
	}
}
