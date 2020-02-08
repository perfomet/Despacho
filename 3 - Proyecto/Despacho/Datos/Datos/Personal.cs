using System;
using System.Data;
using Datos.DB;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Datos.Datos
{
	public static class Personal
	{
		public static List<Modelo.Personal> ObtenerPersonal()
		{
			string SELECTSentence = "SELECT PersonalId, Personal.RUT, Personal.DV, Personal.Nombre, Personal.PrimerApellido, Personal.SegundoApellido, Personal.Email, Personal.TipoPersonalId, TipoPersonal.Descripcion, Personal.EstaActivo";
			string FROMSentence = " FROM Personal INNER JOIN TipoPersonal ON Personal.TipoPersonalId = TipoPersonal.TipoPersonalId";
			string WHERESentence = "";
			string ORDERSentence = ";";
			string SQLSentence = SELECTSentence + FROMSentence + WHERESentence + ORDERSentence;
			DataTable dataTable = DataBase.ExecuteReader(SQLSentence);
			List<Modelo.Personal> listaenlaces = new List<Modelo.Personal>();
			foreach (DataRow fila in dataTable.Rows)
			{
				Modelo.Personal enlaces = new Modelo.Personal();
				enlaces.FromDataRow(fila);
				listaenlaces.Add(enlaces);
			}

			return listaenlaces;
		}
		public static Modelo.Personal ObtenerPersonal(int Id)
		{
			string SELECTSentence = "SELECT PersonalId, Personal.RUT, Personal.DV, Personal.Nombre, Personal.PrimerApellido, Personal.SegundoApellido, Personal.Email, Personal.TipoPersonalId, TipoPersonal.Descripcion, Personal.EstaActivo";
			string FROMSentence = " FROM Personal INNER JOIN TipoPersonal ON Personal.TipoPersonalId = TipoPersonal.TipoPersonalId";
			string WHERESentence = " WHERE PersonalId = " + Id.ToString();
			string ORDERSentence = ";";
			string SQLSentence = SELECTSentence + FROMSentence + WHERESentence + ORDERSentence;
			Modelo.Personal enlace = new Modelo.Personal();
			DataTable dataTable = DataBase.ExecuteReader(SQLSentence);

			if (dataTable.Rows.Count > 0)
			{
				DataRow fila = dataTable.Rows[0];
				enlace.FromDataRow(fila);
			}

			return enlace;
		}

		public static bool Crear(Modelo.Personal personal)
		{
			string INSERTSentence = "INSERT INTO Personal";
			//personal.DV = Internos.Verirut(Convert.ToString(personal.RUT), Internos.schile);
			string VALUESSentence = " VALUES({0}, '{1}', '{2}', '{3}', '{4}', '{5}', {6}, 1);";
			string SQLSentence = INSERTSentence + VALUESSentence;
			StringBuilder builder = new StringBuilder();

			builder.AppendFormat(SQLSentence, personal.RUT, personal.DV, personal.Nombre, personal.Primerapellido, personal.Segundoapellido, personal.Email, personal.Tipopersonalid, personal.EstaActivo);
			return DataBase.ExecuteNonQuery(builder.ToString()) > 0;
		}

		public static bool Modificar(Modelo.Personal personal)
		{
			string UPDATESentence = "UPDATE Personal";
			string SETSentence = " SET RUT = {1}, DV = '{2}', Nombre = '{3}', PrimerApellido = '{4}', SegundoApellido = '{5}', Email = '{6}', TipoPersonalId = '{7}'";
			string WHERESentence = " WHERE PersonalId = {0}";

			string SQLSentence = UPDATESentence + SETSentence + WHERESentence;
			StringBuilder builder = new StringBuilder();
			builder.AppendFormat(SQLSentence, personal.Personalid, personal.RUT, personal.DV, personal.Nombre, personal.Primerapellido, personal.Segundoapellido, personal.Email, personal.Tipopersonalid);

			return DataBase.ExecuteNonQuery(builder.ToString()) > 0;
		}

		public static bool EstaActivo(int Id)
		{
			string UPDATESentence = "UPDATE Personal";
			string SETSentence = " SET EstaActivo = CASE WHEN EstaActivo = 1 THEN 0 ELSE 1 END";
			string WHERESentence = " WHERE PersonalId = {0}";
			string SQLSentence = UPDATESentence + SETSentence + WHERESentence;
			StringBuilder builder = new StringBuilder();
			builder.AppendFormat(SQLSentence, Id);
			return DataBase.ExecuteNonQuery(builder.ToString()) > 0;
		}
	}
}
