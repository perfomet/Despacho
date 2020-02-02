using System;
using System.Data;

namespace Datos.Modelo
{
	public class Existencia : Base.ModeloBase
	{
		public string CodBodega { get; set; }
		public string NomBodega { get; set; }
		public string Bin { get; set; }
		public string Propietario { get; set; }
		public string FechaAlmacenaje { get; set; }
		public string CodArt { get; set; }
		public string Serie { get; set; }
		public string Placa { get; set; }
		public string Modelo { get; set; }
		public string Denominacion { get; set; }
		public string Marca { get; set; }
		public string Referencia { get; set; }
		public string Cliente { get; set; }

		public void FromDataRow(DataRow fila)
		{
			this.CodBodega = fila[0].ToString();
			this.NomBodega = fila[1].ToString();
			this.Bin = fila[2].ToString();
			this.Propietario = fila[3].ToString();
			this.FechaAlmacenaje = DateTime.Parse(fila[4].ToString()).ToString("dd/MM/yyyy");
			this.CodArt = fila[5].ToString();
			this.Serie = fila[6].ToString();
			this.Placa = fila[7].ToString();
			this.Modelo = fila[8].ToString();
			this.Denominacion = fila[9].ToString();
			this.Marca = fila[10].ToString();
			this.Referencia = fila[11].ToString();
			this.Cliente = fila[12].ToString();
		}
	}
}
