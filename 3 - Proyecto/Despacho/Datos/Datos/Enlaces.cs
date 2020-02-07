using System;
using System.Data;
using Datos.DB;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Datos.Datos
{
	public static class Enlaces
	{
		public static List<Modelo.Enlaces> ObtenerEnlaces()
		{
			string SELECTSentence = "SELECT Enlace.EnlaceId, Enlace.RUT, Enlace.DV, Enlace.Nombre, Enlace.PrimerApellido, Enlace.SegundoApellido, Enlace.Email, Enlace.TipoPersonalId, TipoPersonal.Descripcion, Enlace.EstaActivo";
			string FROMSentence = " FROM Enlace INNER JOIN TipoPersonal ON Enlace.TipoPersonalId = TipoPersonal.TipoPersonalId";
			string WHERESentence = "";
			string ORDERSentence = ";";
			string SQLSentence = SELECTSentence + FROMSentence + WHERESentence + ORDERSentence;
			DataTable dataTable = DataBase.ExecuteReader(SQLSentence);
			List<Modelo.Enlaces> listaenlaces = new List<Modelo.Enlaces>();
			foreach (DataRow fila in dataTable.Rows)
			{
				Modelo.Enlaces enlaces = new Modelo.Enlaces();
				enlaces.FromDataRow(fila);
				listaenlaces.Add(enlaces);
			}

			return listaenlaces;
		}
		public static Modelo.Enlaces ObtenerEnlace(int Id)
		{
			string SELECTSentence = "SELECT Enlace.*";
			string FROMSentence = " FROM Enlace";
			string WHERESentence = " WHERE Enlace.EnlaceId = " + Id.ToString();
			string ORDERSentence = ";";
			string SQLSentence = SELECTSentence + FROMSentence + WHERESentence + ORDERSentence;
			Modelo.Enlaces enlace = new Modelo.Enlaces();
			DataTable dataTable = DataBase.ExecuteReader(SQLSentence);

			if (dataTable.Rows.Count > 0)
			{
				DataRow fila = dataTable.Rows[0];
				enlace.FromDataRow(fila);
			}

			return enlace;
		}

		public static bool Crear(Modelo.Enlaces enlaces)
		{
			string INSERTSentence = "INSERT INTO Enlace";
			enlaces.DV = Internos.Verirut(Convert.ToString(enlaces.RUT), Internos.schile);
			string VALUESSentence = " VALUES({0}, '{1}', '{2}', '{3}', '{4}', '{5}', {6}, 1);";
			string SQLSentence = INSERTSentence + VALUESSentence;
			StringBuilder builder = new StringBuilder();

			builder.AppendFormat(SQLSentence, enlaces.RUT, enlaces.DV, enlaces.Nombre, enlaces.Primerapellido, enlaces.Segundoapellido, enlaces.Email, enlaces.Tipopersonalid, enlaces.EstaActivo);
			return DataBase.ExecuteNonQuery(builder.ToString()) > 0;
		}

		public static bool Modificar(Modelo.Enlaces enlaces)
		{
			string UPDATESentence = "UPDATE Enlace";
			string SETSentence = " SET RUT = {1}, DV = '{2}', Nombre = '{3}', PrimerApellido = '{4}', SegundoApellido = '{5}', Email = '{6}', TipoPersonalId = '{7}'";
			string WHERESentence = " WHERE Enlace.EnlaceId = {0}";

			string SQLSentence = UPDATESentence + SETSentence + WHERESentence;
			StringBuilder builder = new StringBuilder();
			builder.AppendFormat(SQLSentence, enlaces.Enlaceid, enlaces.RUT,enlaces.DV,enlaces.Nombre,enlaces.Primerapellido,enlaces.Segundoapellido,enlaces.Email,enlaces.Tipopersonalid);

			return DataBase.ExecuteNonQuery(builder.ToString()) > 0;
		}

		public static bool EstaActivo(int Id)
		{
			string UPDATESentence = "UPDATE Enlace";
			string SETSentence = " SET Enlace.EstaActivo = CASE WHEN Enlace.EstaActivo = 1 THEN 0 ELSE 1 END";
			string WHERESentence = " WHERE Enlace.EnlaceId = {0}";
			string SQLSentence = UPDATESentence + SETSentence + WHERESentence;
			StringBuilder builder = new StringBuilder();
			builder.AppendFormat(SQLSentence, Id);
			return DataBase.ExecuteNonQuery(builder.ToString()) > 0;
		}
	}
}
