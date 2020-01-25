using System.Data;

namespace Datos.Modelo
{
	public class Provincia : Base.ModeloBase
	{
		public int ProvinciaId { get; set; }
		public int RegionId { get; set; }
		public int Orden { get; set; }
		public string Nombre { get; set; }

		public Region Region
		{
			get { return new Modelo.Region(); }
			set { Region = value; }
		}

		public void FromDataRow(DataRow fila)
		{
			this.ProvinciaId = int.Parse(fila[0].ToString());
			this.RegionId = int.Parse(fila[1].ToString());
			this.Orden = int.Parse(fila[2].ToString());
			this.Nombre = fila[3].ToString();
		}
	}
}
