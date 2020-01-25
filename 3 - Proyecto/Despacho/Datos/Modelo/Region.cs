using System.Data;

namespace Datos.Modelo
{
	public class Region : Base.ModeloBase
	{
		public int RegionId { get; set; }
		public string Nombre { get; set; }

		public void FromDataRow(DataRow fila)
		{
			this.RegionId = int.Parse(fila[0].ToString());
			this.Nombre = fila[1].ToString();
		}
	}
}
