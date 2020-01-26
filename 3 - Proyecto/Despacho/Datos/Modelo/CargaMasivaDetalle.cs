using System.Collections.Generic;
using System.Data;

namespace Datos.Modelo
{
	public class CargaMasivaDetalle : Base.ModeloBase
	{
		public int CargaMasivaDetalleId { get; set; }
		public int CargaMasivaId { get; set; }
		public string TipoSolicitud { get; set; }
		public string FechaSolicitud { get; set; }
		public string FechaRecepcion { get; set; }
		public string BodegaOrigen { get; set; }
		public string EstadoEquipo { get; set; }
		public string Clasificacion { get; set; }
		public string NumeroCliente { get; set; }
		public string NombreCliente { get; set; }
		public string DireccionCliente { get; set; }
		public string Comuna { get; set; }
		public string TelefonoContacto { get; set; }
		public string Rut { get; set; }
		public string Proyecto { get; set; }
		public string Prioridad { get; set; }
		public string UnidadNegocio { get; set; }
		public string Gerencia { get; set; }
		public string ObservacionAof { get; set; }

		public CargaMasiva CargaMasiva
		{
			get { return new CargaMasiva(); }
		}

		public List<CargaMasivaDetalleProducto> Productos
		{
			get { return new List<CargaMasivaDetalleProducto>(); }
		}

		public void FromDataRow(DataRow fila)
		{
			//CAMBIO DE PRUEBA
			this.CargaMasivaDetalleId = int.Parse(fila[0].ToString());
			this.CargaMasivaId = int.Parse(fila[1].ToString());
			this.TipoSolicitud = fila[2].ToString();
			this.FechaSolicitud = fila[3].ToString();
			this.FechaRecepcion = fila[4].ToString();
			this.BodegaOrigen = fila[5].ToString();
			this.EstadoEquipo = fila[6].ToString();
			this.Clasificacion = fila[7].ToString();
			this.NumeroCliente = fila[8].ToString();
			this.NombreCliente = fila[9].ToString();
			this.DireccionCliente = fila[10].ToString();
			this.Comuna = fila[11].ToString();
			this.TelefonoContacto = fila[12].ToString();
			this.Rut = fila[13].ToString();
			this.Proyecto = fila[14].ToString();
			this.Prioridad = fila[15].ToString();
			this.UnidadNegocio = fila[16].ToString();
			this.Gerencia = fila[17].ToString();
			this.ObservacionAof = fila[18].ToString();
		}
	}
}
