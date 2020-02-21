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
			string SELECTSentence = "SELECT CargaMasivaDetalleId, CargaMasivaId, TipoSolicitud, FechaSolicitud, FechaRecepcion, BodegaOrigen, NumeroCliente, NombreCliente, DireccionCliente, Comuna, TelefonoContacto, Rut, Proyecto, UnidadNegocio, Gerencia, ObservacionAof, Prioridad";
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
			string SELECTSentence = "SELECT CargaMasivaDetalleId, CargaMasivaId, TipoSolicitud, FechaSolicitud, FechaRecepcion, BodegaOrigen, NumeroCliente, NombreCliente, DireccionCliente, Comuna, TelefonoContacto, Rut, Proyecto, UnidadNegocio, Gerencia, ObservacionAof, Prioridad";
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
			string INSERTSentence = "INSERT INTO CargaMasivaDetalle (CargaMasivaId, TipoSolicitud, FechaSolicitud, FechaRecepcion, BodegaOrigen, NumeroCliente, NombreCliente, DireccionCliente, Comuna, TelefonoContacto, Rut, Proyecto, UnidadNegocio, Gerencia, ObservacionAof, Prioridad)";
			string VALUESSentence = " VALUES({1}, '{2}', '{3}', '{4)', '{5}', '{6}', '{7)', '{8}', '{9}', '{10)', '{11}', '{12}', '{13)', '{14}', '{15}', '{16)');";
			string SQLSentence = INSERTSentence + VALUESSentence;
			StringBuilder builder = new StringBuilder();
			builder.AppendFormat(SQLSentence, cargamasivadetalle.CargaMasivaDetalleId, cargamasivadetalle.CargaMasivaId, cargamasivadetalle.TipoSolicitud, cargamasivadetalle.FechaSolicitud, cargamasivadetalle.FechaRecepcion, cargamasivadetalle.BodegaOrigen, cargamasivadetalle.NumeroCliente, cargamasivadetalle.NombreCliente, cargamasivadetalle.DireccionCliente, cargamasivadetalle.Comuna, cargamasivadetalle.TelefonoContacto, cargamasivadetalle.Rut, cargamasivadetalle.Proyecto, cargamasivadetalle.UnidadNegocio, cargamasivadetalle.Gerencia, cargamasivadetalle.ObservacionAof, cargamasivadetalle.Prioridad);
			DataBase.ExecuteNonQuery(builder.ToString());
			return int.Parse(DataBase.ExecuteScalar("SELECT SCOPE_IDENTITY()").ToString());
		}

		public static bool Crear(List<Modelo.CargaMasivaDetalle> cargamasivadetalles)
		{
			int insertados = 0;

			cargamasivadetalles.ForEach((detalle) =>
			{
				string INSERTSentence = "INSERT INTO CargaMasivaDetalle (CargaMasivaId, TipoSolicitud, FechaSolicitud, FechaRecepcion, BodegaOrigen, NumeroCliente, NombreCliente, DireccionCliente, Comuna, TelefonoContacto, Rut, Proyecto, UnidadNegocio, Gerencia, ObservacionAof, Prioridad)";
				string VALUESSentence = " VALUES({1}, '{2}', '{3}', '{4)', '{5}', '{6}', '{7)', '{8}', '{9}', '{10)', '{11}', '{12}', '{13)', '{14}', '{15}', '{16)');";
				string SQLSentence = INSERTSentence + VALUESSentence;
				StringBuilder builder = new StringBuilder();
				builder.AppendFormat(SQLSentence, detalle.CargaMasivaDetalleId, detalle.CargaMasivaId, detalle.TipoSolicitud, detalle.FechaSolicitud, detalle.FechaRecepcion, detalle.BodegaOrigen, detalle.NumeroCliente, detalle.NombreCliente, detalle.DireccionCliente, detalle.Comuna, detalle.TelefonoContacto, detalle.Rut, detalle.Proyecto, detalle.UnidadNegocio, detalle.Gerencia, detalle.ObservacionAof, detalle.Prioridad);
				insertados += DataBase.ExecuteNonQuery(builder.ToString());
			});

			return cargamasivadetalles.Count == insertados;
		}

		public static bool Modificar(Modelo.CargaMasivaDetalle cargamasivadetalle)
		{
			string UPDATESentence = "UPDATE CargaMasivaDetalle";
			string SETSentence = " SET CargaMasivaId = {1}, TipoSolicitud = '{2}', FechaSolicitud = '{3}', FechaRecepcion = '{4}', BodegaOrigen = '{5}', NumeroCliente = '{6}', NombreCliente = '{7}', DireccionCliente = '{8}', Comuna = '{9}', TelefonoContacto = '{10}', Rut = '{11}', Proyecto = '{12}', UnidadNegocio = '{13}', Gerencia = '{14}', ObservacionAof = '{15}', Prioridad = '{16}'";
			string WHERESentence = " WHERE CargaMasivaId = {0}";
			string SQLSentence = UPDATESentence + SETSentence + WHERESentence;
			StringBuilder builder = new StringBuilder();
			builder.AppendFormat(SQLSentence, cargamasivadetalle.CargaMasivaDetalleId, cargamasivadetalle.CargaMasivaId, cargamasivadetalle.TipoSolicitud, cargamasivadetalle.FechaSolicitud, cargamasivadetalle.FechaRecepcion, cargamasivadetalle.BodegaOrigen, cargamasivadetalle.NumeroCliente, cargamasivadetalle.NombreCliente, cargamasivadetalle.DireccionCliente, cargamasivadetalle.Comuna, cargamasivadetalle.TelefonoContacto, cargamasivadetalle.Rut, cargamasivadetalle.Proyecto, cargamasivadetalle.UnidadNegocio, cargamasivadetalle.Gerencia, cargamasivadetalle.ObservacionAof, cargamasivadetalle.Prioridad);

			return DataBase.ExecuteNonQuery(builder.ToString()) > 0;
		}
	}
}

