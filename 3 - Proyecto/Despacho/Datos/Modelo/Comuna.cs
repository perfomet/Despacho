using System.Data;

namespace Datos.Modelo
{
	public class Comuna : Base.ModeloBase
	{
		public int ComunaId { get; set; }
		public int RegionId { get; set; }
		public int ProvinciaId { get; set; }
		public string Nombre { get; set; }

		public Region Region
		{
			get { return new Modelo.Region(); }
			set { Region = value; }
		}

		public Provincia Provincia
		{
			get { return new Modelo.Provincia(); }
			set { Provincia = value; }
		}

		public void FromDataRow(DataRow fila)
		{
			this.ComunaId = int.Parse(fila[0].ToString());
			this.RegionId = int.Parse(fila[1].ToString());
			this.ProvinciaId = int.Parse(fila[2].ToString());
			this.Nombre = fila[3].ToString();
		}
	}
}
