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
		public string CalleDireccionCliente { get; set; }
		public string NumeroDireccionCliente { get; set; }
		public string RegionCliente { get; set; }
		public string ComunaCliente { get; set; }
		public string NumeroTelefonoContacto { get; set; }
		public string NumeroTelefonoContactoAdicional { get; set; }
		public string RutCliente { get; set; }
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
			this.CalleDireccionCliente = fila[8].ToString();
			this.NumeroDireccionCliente = fila[9].ToString();
			this.RegionCliente = fila[10].ToString();
			this.ComunaCliente = fila[11].ToString();
			this.NumeroTelefonoContacto = fila[12].ToString();
			this.NumeroTelefonoContactoAdicional = fila[13].ToString();
			this.RutCliente = fila[14].ToString();
			this.UnidadNegocio = fila[15].ToString();
			this.Gerencia = fila[16].ToString();
			this.ObservacionAof = fila[17].ToString();
			this.Prioridad = fila[18].ToString();
			
		}
	}
}
