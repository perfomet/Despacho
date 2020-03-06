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
			string SELECTSentence = "SELECT CargaMasivaDetalleId, CargaMasivaId, NumeroSolicitud, TipoSolicitud, FechaSolicitud, FechaRecepcion, NumeroCliente, NombreCliente, CalleDireccionCliente, NumeroDireccionCliente, RegionCliente, ComunaCliente, NumeroTelefonoContacto, NumeroTelefonoContactoAdicional, RutCliente, UnidadNegocio, Gerencia, ObservacionAof, Prioridad, Placa";
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
			string SELECTSentence = "SELECT CargaMasivaDetalleId, CargaMasivaId, TipoSolicitud, FechaSolicitud, FechaRecepcion, NumeroCliente, NombreCliente, CalleDireccionCliente, NumeroDireccionCliente, RegionCliente, ComunaCliente, NumeroTelefonoContacto, RutCliente, UnidadNegocio, Gerencia, ObservacionAof, Prioridad, NumeroPlaca";
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
			if (cargamasivadetalle.CargaMasivaId > 0)
			{
				string INSERTSentence = "INSERT INTO CargaMasivaDetalle (CargaMasivaId, NumeroSolicitud, TipoSolicitud, FechaSolicitud, FechaRecepcion, NumeroCliente, NombreCliente, CalleDireccionCliente, NumeroDireccionCliente, RegionCliente, ComunaCliente, NumeroTelefonoContacto, NumeroTelefonoContactoAdicional, RutCliente, UnidadNegocio, Gerencia, ObservacionAof, Prioridad, NumeroPlaca)";
				string VALUESSentence = " VALUES({1}, '{2}', '{3}', '{4)', '{5}', '{6}', '{7)', '{8}', '{9}', '{10)', '{11}', '{12}', '{13)', '{14}', '{15}', '{16)', '{17'}, '{18}', '{19}');";
				string SQLSentence = INSERTSentence + VALUESSentence;
				StringBuilder builder = new StringBuilder();
				builder.AppendFormat(SQLSentence, cargamasivadetalle.CargaMasivaDetalleId, cargamasivadetalle.CargaMasivaId, cargamasivadetalle.NumeroSolicitud, cargamasivadetalle.TipoSolicitud, cargamasivadetalle.FechaSolicitud, cargamasivadetalle.FechaRecepcion, cargamasivadetalle.NumeroCliente, cargamasivadetalle.NombreCliente, cargamasivadetalle.CalleDireccionCliente, cargamasivadetalle.NumeroDireccionCliente, cargamasivadetalle.RegionCliente, cargamasivadetalle.ComunaCliente, cargamasivadetalle.NumeroTelefonoContacto, cargamasivadetalle.NumeroTelefonoContactoAdicional, cargamasivadetalle.RutCliente, cargamasivadetalle.UnidadNegocio, cargamasivadetalle.Gerencia, cargamasivadetalle.ObservacionAof, cargamasivadetalle.Prioridad, cargamasivadetalle.NumeroPlaca);
				DataBase.ExecuteNonQuery(builder.ToString());
				return int.Parse(DataBase.ExecuteScalar("SELECT SCOPE_IDENTITY()").ToString());
			}
			else
			{
				return 0;
			}
		}

		public static bool Crear(List<Modelo.CargaMasivaDetalle> cargamasivadetalles)
		{
			int insertados = 0;

			cargamasivadetalles.ForEach((detalle) =>
			{
				string INSERTSentence = "INSERT INTO CargaMasivaDetalle (CargaMasivaId, NumeroSolicitud, TipoSolicitud, FechaSolicitud, FechaRecepcion, NumeroCliente, NombreCliente, CalleDireccionCliente, NumeroDireccionCliente, RegionCliente, ComunaCliente, NumeroTelefonoContacto, NumeroTelefonoContactoAdicional, RutCliente, UnidadNegocio, Gerencia, ObservacionAof, Prioridad, NumeroPlaca)";
				string VALUESSentence = " VALUES({1}, '{2}', '{3}', '{4)', '{5}', '{6}', '{7)', '{8}', '{9}', '{10)', '{11}', '{12}', '{13)', '{14}', '{15}', '{16)', '{17}', '{18}', '{19}');";
				string SQLSentence = INSERTSentence + VALUESSentence;
				StringBuilder builder = new StringBuilder();
				builder.AppendFormat(SQLSentence, detalle.CargaMasivaDetalleId, detalle.CargaMasivaId, detalle.NumeroSolicitud, detalle.TipoSolicitud, detalle.FechaSolicitud, detalle.FechaRecepcion, detalle.NumeroCliente, detalle.NombreCliente, detalle.CalleDireccionCliente, detalle.NumeroDireccionCliente, detalle.RegionCliente, detalle.ComunaCliente, detalle.NumeroTelefonoContacto, detalle.NumeroTelefonoContactoAdicional, detalle.RutCliente, detalle.UnidadNegocio, detalle.Gerencia, detalle.ObservacionAof, detalle.Prioridad);
				insertados += DataBase.ExecuteNonQuery(builder.ToString());
			});

			return cargamasivadetalles.Count == insertados;
		}

		public static bool Modificar(Modelo.CargaMasivaDetalle cargamasivadetalle)
		{
			string UPDATESentence = "UPDATE CargaMasivaDetalle";
			string SETSentence = " SET CargaMasivaId = {1}, NumeroSolicitud = '{2}', TipoSolicitud = '{3}', FechaSolicitud = '{4}', FechaRecepcion = '{5}', NumeroCliente = '{6}', NombreCliente = '{7}', CalleDireccionCliente = '{8}', NumeroDireccionCliente = '{9}', RegionCliente = '{10}', ComunaCliente = '{11}', NumeroTelefonoContacto = '{12}', NumeroTelefonoContactoAdicional = '{13}', RutCliente = '{14}', UnidadNegocio = '{15}', Gerencia = '{16}', ObservacionAof = '{17}', Prioridad = '{18}', NumeroPlaca = '{19}'";
			string WHERESentence = " WHERE CargaMasivaId = {0}";
			string SQLSentence = UPDATESentence + SETSentence + WHERESentence;
			StringBuilder builder = new StringBuilder();
			builder.AppendFormat(SQLSentence, cargamasivadetalle.CargaMasivaDetalleId, cargamasivadetalle.CargaMasivaId, cargamasivadetalle.NumeroSolicitud, cargamasivadetalle.TipoSolicitud, cargamasivadetalle.FechaSolicitud, cargamasivadetalle.FechaRecepcion, cargamasivadetalle.NumeroCliente, cargamasivadetalle.NombreCliente, cargamasivadetalle.CalleDireccionCliente, cargamasivadetalle.NumeroDireccionCliente, cargamasivadetalle.RegionCliente, cargamasivadetalle.ComunaCliente, cargamasivadetalle.NumeroTelefonoContacto, cargamasivadetalle.NumeroTelefonoContactoAdicional, cargamasivadetalle.RutCliente, cargamasivadetalle.UnidadNegocio, cargamasivadetalle.Gerencia, cargamasivadetalle.ObservacionAof, cargamasivadetalle.Prioridad, cargamasivadetalle.NumeroPlaca);

			return DataBase.ExecuteNonQuery(builder.ToString()) > 0;
		}
	}
}

