using System;
using System.Data;

namespace Datos.Modelo
{
	public class Existencia : Base.ModeloBase
	{
		public string Bodega { get; set; }
		public string NomBodega { get; set; }
		public string Bin { get; set; }
		public DateTime FechaAlmacenaje { get; set; }
		public string CodArt { get; set; }
		public string Serie { get; set; }
		public string Placa { get; set; }
		public string Modelo { get; set; }
		public string Denominacion { get; set; }
		public string Marca { get; set; }
		public string Referencia { get; set; }

		public void FromDataRow(DataRow fila)
		{
			this.Bodega = fila[0].ToString();
			this.NomBodega = fila[1].ToString();
			this.Bin = fila[2].ToString();
			this.FechaAlmacenaje = DateTime.Parse(fila[3].ToString());
			this.CodArt = fila[4].ToString();
			this.Serie = fila[5].ToString();
			this.Placa = fila[6].ToString();
			this.Modelo = fila[7].ToString();
			this.Denominacion = fila[8].ToString();
			this.Marca = fila[9].ToString();
			this.Referencia = fila[10].ToString();
		}
	}
}
