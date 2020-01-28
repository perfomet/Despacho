using Datos.DB;
using System.Collections.Generic;
using System.Data;
using System.Text;
namespace Datos.Datos
{
	public class Perfil
	{
		public static List<Modelo.Perfil> ObtenerPerfiles()
		{
			string SELECTSentence = "SELECT *";
			string FROMSentence = " FROM Perfil";
			string WHERESentence = "";
			string ORDERSentence = ";";
			string SQLSentence = SELECTSentence + FROMSentence + WHERESentence + ORDERSentence;
			DataTable dataTable = DataBase.ExecuteReader(SQLSentence);
			List<Modelo.Perfil> perfiles = new List<Modelo.Perfil>();
			foreach (DataRow fila in dataTable.Rows)
			{
				Modelo.Perfil perfil = new Modelo.Perfil();
				perfil.FromDataRow(fila);
				perfiles.Add(perfil);
			}

			return perfiles;
		}

		public static Modelo.Perfil ObtenerPerfil(int Id)
		{

			string SELECTSentence = "SELECT *";
			string FROMSentence = " FROM Perfil";
			string WHERESentence = " WHERE PerfilId = '" + Id.ToString() + "'";
			string ORDERSentence = ";";
			string SQLSentence = SELECTSentence + FROMSentence + WHERESentence + ORDERSentence;
			Modelo.Perfil perfil = new Modelo.Perfil();
			DataTable dataTable = DataBase.ExecuteReader(SQLSentence);

			if (dataTable.Rows.Count > 0)
			{
				DataRow fila = dataTable.Rows[0];
				perfil.FromDataRow(fila);
			}

			return perfil;
		}

		public static bool Crear(Modelo.Perfil perfil)
		{
			string INSERTSentence = "INSERT INTO Perfil";
			string VALUESSentence = " VALUES('{1}');";
			string SQLSentence = INSERTSentence + VALUESSentence;
			StringBuilder builder = new StringBuilder();
			builder.AppendFormat(SQLSentence, perfil.Descripcion, perfil.PerfilId);
			return DataBase.ExecuteNonQuery(builder.ToString()) > 0;
		}

		public static bool Modificar(Modelo.Perfil perfil)
		{
			string UPDATESentence = "UPDATE Perfil";
			string SETSentence = " SET Descripcion = '{1}'";
			string WHERESentence = " WHERE PerfilId = {0}";

			string SQLSentence = UPDATESentence + SETSentence + WHERESentence;
			StringBuilder builder = new StringBuilder();
			builder.AppendFormat(SQLSentence, perfil.Descripcion);

			return DataBase.ExecuteNonQuery(builder.ToString()) > 0;
		}

		public static bool Eliminar(int Id)
		{
			string DELETESentence = "DELETE";
			string FROMSentence = " FROM Perfil";
			string WHERESentence = " WHERE PerfilId = {0}";
			string SQLSentence = DELETESentence + FROMSentence + WHERESentence;
			StringBuilder builder = new StringBuilder();
			builder.AppendFormat(SQLSentence, Id);
			return DataBase.ExecuteNonQuery(builder.ToString()) > 0;
		}
	}
}
