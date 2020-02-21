using System.Collections.Generic;
using System.Data;

namespace Datos.Modelo
{
	public class CargaMasivaDetalle : Base.ModeloBase
	{
		public int CargaMasivaDetalleId { get; set; }
		public int CargaMasivaId { get; set; }
		public int NumeroSolicitud { get; set; }
		public string TipoSolicitud { get; set; }
		public string FechaSolicitud { get; set; }
		public string FechaRecepcion { get; set; }
		public string NumeroCliente { get; set; }
		public string NombreCliente { get; set; }
		public string DireccionCalleCliente { get; set; }
		public string DireccionNumeroCliente { get; set; }
		public string Region { get; set; }
		public string Comuna { get; set; }
		public string TelefonoContacto { get; set; }
		public string TelefonoContacto2 { get; set; }
		public string Rut { get; set; }
		public string UnidadNegocio { get; set; }
		public string Gerencia { get; set; }
		public string ObservacionAof { get; set; }
		public string Prioridad { get; set; }
		

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
			this.NumeroSolicitud = int.Parse(fila[2].ToString());
			this.TipoSolicitud = fila[3].ToString();
			this.FechaSolicitud = fila[4].ToString();
			this.FechaRecepcion = fila[5].ToString();
			this.NumeroCliente = fila[6].ToString();
			this.NombreCliente = fila[7].ToString();
			this.DireccionCalleCliente = fila[8].ToString();
			this.DireccionNumeroCliente = fila[9].ToString();
			this.Region = fila[10].ToString();
			this.Comuna = fila[11].ToString();
			this.TelefonoContacto = fila[12].ToString();
			this.TelefonoContacto2 = fila[13].ToString();
			this.Rut = fila[14].ToString();
			this.UnidadNegocio = fila[15].ToString();
			this.Gerencia = fila[16].ToString();
			this.ObservacionAof = fila[17].ToString();
			this.Prioridad = fila[18].ToString();
				
		}
	}
}
