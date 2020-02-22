using System.Data;

namespace Datos.Modelo
{
	public class CargaMasivaDetalleProducto : Base.ModeloBase
	{
		public int CargaMasivaDetalleId { get; set; }
		public int NumeroSolicitud { get; set; }
		public string NumeroPlaca { get; set; }

		public CargaMasivaDetalle CargaMasivaDetalle
		{
			get { return new CargaMasivaDetalle(); }
		}

		public void FromDataRow(DataRow fila)
		{
			this.CargaMasivaDetalleId = int.Parse(fila[0].ToString());
			this.NumeroSolicitud = int.Parse(fila[1].ToString());
			this.NumeroPlaca = fila[2].ToString();
		}
	}
}
