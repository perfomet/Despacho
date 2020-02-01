using System.Data;

namespace Datos.Modelo
{
	public class Bodega : Base.ModeloBase
	{
		public string Codigo { get; set; }
		public string Nombre { get; set; }

		public void FromDataRow(DataRow fila)
		{
			this.Codigo = fila[0].ToString();
			this.Nombre = fila[1].ToString();
		}
	}
}
