using Datos.DB;
using System.Collections.Generic;
using System.Data;
using System.Text;

namespace Datos.Datos
{
	public class CargaMasivaDetalleProducto
	{
		public static List<Modelo.CargaMasivaDetalleProducto> ObtenerProductos(int detalleId)
		{
			string SELECTSentence = "SELECT *";
			string FROMSentence = " FROM CargaMasivaDetalleProducto";
			string WHERESentence = " WHERE CargaMasivaDetalleId = " + detalleId;
			string ORDERSentence = ";";
			string SQLSentence = SELECTSentence + FROMSentence + WHERESentence + ORDERSentence;
			DataTable dataTable = DataBase.ExecuteReader(SQLSentence);
			List<Modelo.CargaMasivaDetalleProducto> productos = new List<Modelo.CargaMasivaDetalleProducto>();
			foreach (DataRow fila in dataTable.Rows)
			{
				Modelo.CargaMasivaDetalleProducto producto = new Modelo.CargaMasivaDetalleProducto();
				producto.FromDataRow(fila);
				productos.Add(producto);
			}

			return productos;
		}

		public static Modelo.CargaMasivaDetalleProducto ObtenerProducto(int detalleId, string numeroPlaca)
		{

			string SELECTSentence = "SELECT *";
			string FROMSentence = " FROM CargaMasivaDetalleProducto";
			string WHERESentence = " WHERE CargaMasivaDetalleId = " + detalleId + " AND NumeroPlaca = '" + numeroPlaca + "'";
			string ORDERSentence = ";";
			string SQLSentence = SELECTSentence + FROMSentence + WHERESentence + ORDERSentence;
			Modelo.CargaMasivaDetalleProducto producto = new Modelo.CargaMasivaDetalleProducto();
			DataTable dataTable = DataBase.ExecuteReader(SQLSentence);

			if (dataTable.Rows.Count > 0)
			{
				DataRow fila = dataTable.Rows[0];
				producto.FromDataRow(fila);
			}

			return producto;
		}

		public static bool Crear(Modelo.CargaMasivaDetalleProducto producto)
		{
			string INSERTSentence = "INSERT INTO CargaMasivaDetalleProducto";
			string VALUESSentence = " VALUES('{0}', {1}, '{2}');";
			string SQLSentence = INSERTSentence + VALUESSentence;
			StringBuilder builder = new StringBuilder();
			builder.AppendFormat(SQLSentence, producto.CargaMasivaDetalleId, producto.NumeroSolicitud, producto.NumeroPlaca);
			return DataBase.ExecuteNonQuery(builder.ToString()) > 0;
		}

		public static bool Eliminar(int detalleId)
		{
			string DELETESentence = "DELETE";
			string FROMSentence = " FROM CargaMasivaDetalleProducto";
			string WHERESentence = " WHERE CargaMasivaDetalleId = {0}";
			string SQLSentence = DELETESentence + FROMSentence + WHERESentence;
			StringBuilder builder = new StringBuilder();
			builder.AppendFormat(SQLSentence, detalleId);
			return DataBase.ExecuteNonQuery(builder.ToString()) > 0;
		}
	}
}
