using System;
using System.Data;
using Datos.DB;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Datos.Datos
{
	public static class EstadoSolicitud
	{
		public static List<Modelo.EstadoSolicitud> ObtenerEstadosSolicitudes()
		{
			string SELECTSentence = "SELECT *";
			string FROMSentence = " FROM EstadoSolicitud";
			string WHERESentence = "";
			string ORDERSentence = ";";
			string SQLSentence = SELECTSentence + FROMSentence + WHERESentence + ORDERSentence;
			DataTable dataTable = DataBase.ExecuteReader(SQLSentence);
			List<Modelo.EstadoSolicitud> estados = new List<Modelo.EstadoSolicitud>();
			foreach (DataRow fila in dataTable.Rows)
			{
				Modelo.EstadoSolicitud estado = new Modelo.EstadoSolicitud();
				estado.FromDataRow(fila);
				estados.Add(estado);
			}

			return estados;
		}

		public static Modelo.EstadoSolicitud ObtenerEstadoSolicitud(int Id)
		{
			string SELECTSentence = "SELECT *";
			string FROMSentence = " FROM EstadoSolicitud";
			string WHERESentence = " WHERE EstadoSolicitudId = '" + Id.ToString() + "'";
			string ORDERSentence = ";";
			string SQLSentence = SELECTSentence + FROMSentence + WHERESentence + ORDERSentence;
			Modelo.EstadoSolicitud estado = new Modelo.EstadoSolicitud();
			DataTable dataTable = DataBase.ExecuteReader(SQLSentence);

			if (dataTable.Rows.Count > 0)
			{
				DataRow fila = dataTable.Rows[0];
				estado.FromDataRow(fila);
			}

			return estado;
		}

		public static bool Crear(Modelo.EstadoSolicitud estado)
		{
			string INSERTSentence = "INSERT INTO EstadoSolicitud";
			string VALUESSentence = " VALUES('{1}', 1);";
			string SQLSentence = INSERTSentence + VALUESSentence;
			StringBuilder builder = new StringBuilder();

			builder.AppendFormat(SQLSentence, estado.Descripcion);
			return DataBase.ExecuteNonQuery(builder.ToString()) > 0;
		}

		public static bool Modificar(Modelo.EstadoSolicitud estado)
		{
			string UPDATESentence = "UPDATE EstadoSolicitud";
			string SETSentence = " SET Descripcion = '{1}'";
			string WHERESentence = " WHERE EstadoSolicitudId = {0}";

			string SQLSentence = UPDATESentence + SETSentence + WHERESentence;
			StringBuilder builder = new StringBuilder();
			builder.AppendFormat(SQLSentence, estado.EstadoSolicitudId, estado.Descripcion);

			return DataBase.ExecuteNonQuery(builder.ToString()) > 0;
		}

		public static bool EstaActivo(int Id)
		{
			string UPDATESentence = "UPDATE EstadoSolicitud";
			string SETSentence = " SET EstaActivo = CASE WHEN EstaActivo = 1 THEN 0 ELSE 1 END";
			string WHERESentence = " WHERE EstadoSolicitudId = {0}";
			string SQLSentence = UPDATESentence + SETSentence + WHERESentence;
			StringBuilder builder = new StringBuilder();
			builder.AppendFormat(SQLSentence, Id);
			return DataBase.ExecuteNonQuery(builder.ToString()) > 0;
		}
	}
}
