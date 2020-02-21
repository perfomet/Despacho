using Datos.DB;
using System.Collections.Generic;
using System.Data;
using System.Text;

namespace Datos.Datos
{
	public class CargaMasivaDetalle
	{
		public static List<Modelo.CargaMasivaDetalle> ObtenerCargasMasivasDetalle()
		{
			string SELECTSentence = "SELECT CargaMasivaDetalleId, CargaMasivaId, NumeroSolicitud, TipoSolicitud, FechaSolicitud, FechaRecepcion, NumeroCliente, NombreCliente, DireccionCalleCliente, DireccionNumeroCliente, Region, Comuna, TelefonoContacto, TelefonoContacto2, Rut, UnidadNegocio, Gerencia, ObservacionAof, Prioridad";
			string FROMSentence = " FROM CargaMasivaDetalle";
			string JOINSentence = "";
			string WHERESentence = "";
			string ORDERSentence = ";";
			string SQLSentence = SELECTSentence + FROMSentence + JOINSentence + WHERESentence + ORDERSentence;

			DataTable dataTable = DataBase.ExecuteReader(SQLSentence);
			List<Modelo.CargaMasivaDetalle> cargasmasivasdetalle = new List<Modelo.CargaMasivaDetalle>();

			foreach (DataRow fila in dataTable.Rows)
			{
				Modelo.CargaMasivaDetalle cargamasivadetalle = new Modelo.CargaMasivaDetalle();
				cargamasivadetalle.FromDataRow(fila);
				cargasmasivasdetalle.Add(cargamasivadetalle);
			}

			return cargasmasivasdetalle;
		}

		public static Modelo.CargaMasivaDetalle ObtenerCargaMasivaDetalle(int id)
		{
			string SELECTSentence = "SELECT CargaMasivaDetalleId, CargaMasivaId, TipoSolicitud, FechaSolicitud, FechaRecepcion, NumeroCliente, NombreCliente, DireccionCalleCliente, DireccionNumeroCliente, Region, Comuna, TelefonoContacto, Rut, UnidadNegocio, Gerencia, ObservacionAof, Prioridad";
			string FROMSentence = " FROM CargaMasivaDetalle";
			string JOINSentence = "";
			string WHERESentence = " CargaMasivaDetalleId = " + id.ToString();
			string ORDERSentence = ";";
			string SQLSentence = SELECTSentence + FROMSentence + JOINSentence + WHERESentence + ORDERSentence;

			Modelo.CargaMasivaDetalle cargamasivadetalle = new Modelo.CargaMasivaDetalle();
			DataTable dataTable = DataBase.ExecuteReader(SQLSentence);

			if (dataTable.Rows.Count > 0)
			{
				DataRow fila = dataTable.Rows[0];
				cargamasivadetalle.FromDataRow(fila);
			}

			return cargamasivadetalle;
		}

		public static int Crear(Modelo.CargaMasivaDetalle cargamasivadetalle)
		{
			string INSERTSentence = "INSERT INTO CargaMasivaDetalle (CargaMasivaId, NumeroSolicitud, TipoSolicitud, FechaSolicitud, FechaRecepcion, NumeroCliente, NombreCliente, DireccionCalleCliente, DireccionNumeroCliente, Region, Comuna, TelefonoContacto, TelefonoContacto2, Rut, UnidadNegocio, Gerencia, ObservacionAof, Prioridad)";
			string VALUESSentence = " VALUES({1}, '{2}', '{3}', '{4)', '{5}', '{6}', '{7)', '{8}', '{9}', '{10)', '{11}', '{12}', '{13)', '{14}', '{15}', '{16)');";
			string SQLSentence = INSERTSentence + VALUESSentence;
			StringBuilder builder = new StringBuilder();
			builder.AppendFormat(SQLSentence, cargamasivadetalle.CargaMasivaDetalleId, cargamasivadetalle.CargaMasivaId, cargamasivadetalle.NumeroSolicitud, cargamasivadetalle.TipoSolicitud, cargamasivadetalle.FechaSolicitud, cargamasivadetalle.FechaRecepcion, cargamasivadetalle.NumeroCliente, cargamasivadetalle.NombreCliente, cargamasivadetalle.DireccionCalleCliente, cargamasivadetalle.DireccionNumeroCliente, cargamasivadetalle.Region, cargamasivadetalle.Comuna, cargamasivadetalle.TelefonoContacto, cargamasivadetalle.TelefonoContacto2, cargamasivadetalle.Rut, cargamasivadetalle.UnidadNegocio, cargamasivadetalle.Gerencia, cargamasivadetalle.ObservacionAof, cargamasivadetalle.Prioridad);
			DataBase.ExecuteNonQuery(builder.ToString());
			return int.Parse(DataBase.ExecuteScalar("SELECT SCOPE_IDENTITY()").ToString());
		}

		public static bool Crear(List<Modelo.CargaMasivaDetalle> cargamasivadetalles)
		{
			int insertados = 0;

			cargamasivadetalles.ForEach((detalle) =>
			{
				string INSERTSentence = "INSERT INTO CargaMasivaDetalle (CargaMasivaId, NumeroSolicitud, TipoSolicitud, FechaSolicitud, FechaRecepcion, NumeroCliente, NombreCliente, DireccionCalleCliente, DireccionNumeroCliente, Region, Comuna, TelefonoContacto, TelefonoContacto2, Rut, UnidadNegocio, Gerencia, ObservacionAof, Prioridad)";
				string VALUESSentence = " VALUES({1}, '{2}', '{3}', '{4)', '{5}', '{6}', '{7)', '{8}', '{9}', '{10)', '{11}', '{12}', '{13)', '{14}', '{15}', '{16)');";
				string SQLSentence = INSERTSentence + VALUESSentence;
				StringBuilder builder = new StringBuilder();
				builder.AppendFormat(SQLSentence, detalle.CargaMasivaDetalleId, detalle.CargaMasivaId, detalle.NumeroSolicitud, detalle.TipoSolicitud, detalle.FechaSolicitud, detalle.FechaRecepcion, detalle.NumeroCliente, detalle.NombreCliente, detalle.DireccionCalleCliente, detalle.DireccionNumeroCliente, detalle.Region, detalle.Comuna, detalle.TelefonoContacto, detalle.TelefonoContacto2, detalle.Rut, detalle.UnidadNegocio, detalle.Gerencia, detalle.ObservacionAof, detalle.Prioridad);
				insertados += DataBase.ExecuteNonQuery(builder.ToString());
			});

			return cargamasivadetalles.Count == insertados;
		}

		public static bool Modificar(Modelo.CargaMasivaDetalle cargamasivadetalle)
		{
			string UPDATESentence = "UPDATE CargaMasivaDetalle";
			string SETSentence = " SET CargaMasivaId = {1}, NumeroSolicitud = '{2}', TipoSolicitud = '{3}', FechaSolicitud = '{4}', FechaRecepcion = '{5}', NumeroCliente = '{6}', NombreCliente = '{7}', DireccionCalleCliente = '{8}', DireccionNumeroCliente = '{9}', Region = '{10}', Comuna = '{11}', TelefonoContacto = '{12}', TelefonoContacto2 = '{13}', Rut = '{14}', UnidadNegocio = '{15}', Gerencia = '{16}', ObservacionAof = '{17}', Prioridad = '{18}'";
			string WHERESentence = " WHERE CargaMasivaId = {0}";
			string SQLSentence = UPDATESentence + SETSentence + WHERESentence;
			StringBuilder builder = new StringBuilder();
			builder.AppendFormat(SQLSentence, cargamasivadetalle.CargaMasivaDetalleId, cargamasivadetalle.CargaMasivaId, cargamasivadetalle.NumeroSolicitud, cargamasivadetalle.TipoSolicitud, cargamasivadetalle.FechaSolicitud, cargamasivadetalle.FechaRecepcion, cargamasivadetalle.NumeroCliente, cargamasivadetalle.NombreCliente, cargamasivadetalle.DireccionCalleCliente, cargamasivadetalle.DireccionNumeroCliente, cargamasivadetalle.Region, cargamasivadetalle.Comuna, cargamasivadetalle.TelefonoContacto, cargamasivadetalle.TelefonoContacto2, cargamasivadetalle.Rut, cargamasivadetalle.UnidadNegocio, cargamasivadetalle.Gerencia, cargamasivadetalle.ObservacionAof, cargamasivadetalle.Prioridad);

			return DataBase.ExecuteNonQuery(builder.ToString()) > 0;
		}
	}
}

