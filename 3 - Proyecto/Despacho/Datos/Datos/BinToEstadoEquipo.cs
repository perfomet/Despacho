using Datos.DB;
using System.Collections.Generic;
using System.Data;
using System.Text;

namespace Datos.Datos
{
	public class BinToEstadoEquipo
	{


		public static List<Modelo.BinToEstadoEquipo> ObtenerlistaBinToEstadoEquipo()
		{
			string SELECTSentence = "SELECT *";
			string FROMSentence = " FROM BinToEstadoEquipo";
			string WHERESentence = ";";
			string SQLSentence = SELECTSentence + FROMSentence + WHERESentence;
			DataTable dataTable = DataBase.ExecuteReader(SQLSentence);
			List<Modelo.BinToEstadoEquipo> listabintoestadoequipo = new List<Modelo.BinToEstadoEquipo>();

			foreach (DataRow fila in dataTable.Rows)
			{
				Modelo.BinToEstadoEquipo bintoestadoequipo = new Modelo.BinToEstadoEquipo();
				bintoestadoequipo.FromDataRow(fila);
				listabintoestadoequipo.Add(bintoestadoequipo);
			}

			return listabintoestadoequipo;
		}

		public static List<Modelo.BinToEstadoEquipo> ObtenerlistaBinToEstadoEquipo(int estadoEquipoId)
		{
			string SELECTSentence = "SELECT *";
			string FROMSentence = " FROM BinToEstadoEquipo";
			string WHERESentence = " WHERE Estadoequipoid = " + estadoEquipoId;
			string SQLSentence = SELECTSentence + FROMSentence + WHERESentence;
			DataTable dataTable = DataBase.ExecuteReader(SQLSentence);
			List<Modelo.BinToEstadoEquipo> listabintoestadoequipo = new List<Modelo.BinToEstadoEquipo>();

			foreach (DataRow fila in dataTable.Rows)
			{
				Modelo.BinToEstadoEquipo bintoestadoequipo = new Modelo.BinToEstadoEquipo();
				bintoestadoequipo.FromDataRow(fila);
				listabintoestadoequipo.Add(bintoestadoequipo);
			}

			return listabintoestadoequipo;
		}

		public static Modelo.BinToEstadoEquipo ObtenerBinToEstadoEquipo(int id)
		{
			string SELECTSentence = "SELECT *";
			string FROMSentence = " FROM BinToEstadoEquipo";
			string WHERESentence = " WHERE BinToEstadoEquipo.Bintoestadoequipoid = " + id;
			string SQLSentence = SELECTSentence + FROMSentence + WHERESentence;
			Modelo.BinToEstadoEquipo bintoestadoequipo = new Modelo.BinToEstadoEquipo();
			DataTable dataTable = DataBase.ExecuteReader(SQLSentence);
			if (dataTable.Rows.Count > 0)
			{
				DataRow fila = dataTable.Rows[0];
				bintoestadoequipo.FromDataRow(fila);
			}

			return bintoestadoequipo;
		}
		public static bool Crear(Modelo.BinToEstadoEquipo bintoestadoequipo)
		{
			StringBuilder builder = new StringBuilder();
			string INSERTSentence = "INSERT INTO BinToEstadoEquipo";
			string VALUESSentence = " VALUES ({0}, '{1}')";
			string SQLSentence = INSERTSentence + VALUESSentence;
			builder.AppendFormat(SQLSentence, bintoestadoequipo.Estadoequipoid, bintoestadoequipo.Bin);

			return DataBase.ExecuteNonQuery(builder.ToString()) > 0;
		}

		public static bool Crear(List<Modelo.BinToEstadoEquipo> binstoestadoequipo)
		{
			int insertados = 0;

			foreach (Modelo.BinToEstadoEquipo bin in binstoestadoequipo)
			{
				StringBuilder builder = new StringBuilder();
				string INSERTSentence = "INSERT INTO BinToEstadoEquipo";
				string VALUESSentence = " VALUES ({0}, '{1}')";
				string SQLSentence = INSERTSentence + VALUESSentence;
				builder.AppendFormat(SQLSentence, bin.Estadoequipoid, bin.Bin);

				insertados += DataBase.ExecuteNonQuery(builder.ToString());
			}

			return insertados == binstoestadoequipo.Count;
		}

		public static bool Modificar(Modelo.BinToEstadoEquipo bintoestadoequipo)
		{
			StringBuilder builder = new StringBuilder();
			string UPDATESentence = "UPDATE BinToEstadoEquipo";
			string SETSentence = " SET Estadoequipoid = {1},  Bin = '{2}'";
			string WHERESentence = " WHERE Bintoestadoequipoid = {0}";
			string SQLSentence = UPDATESentence + SETSentence + WHERESentence;
			builder.AppendFormat(SQLSentence, bintoestadoequipo.Bintoestadoequipoid, bintoestadoequipo.Estadoequipoid, bintoestadoequipo.Bin);

			return DataBase.ExecuteNonQuery(builder.ToString()) > 0;
		}

		public static bool Eliminar(int estadoEquipoId)
		{
			string DELETESentence = "DELETE FROM BinToEstadoEquipo";
			string WHERESentence = " WHERE Estadoequipoid = {0}";
			string SQLSentence = DELETESentence + WHERESentence;
			StringBuilder builder = new StringBuilder();
			builder.AppendFormat(SQLSentence, estadoEquipoId);

			return DataBase.ExecuteNonQuery(builder.ToString()) > 0;
		}

	}
}

