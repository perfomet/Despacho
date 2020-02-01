using System.Data;

namespace Datos.Modelo
{
	public class Bin : Base.ModeloBase
	{
		public string Codigo { get; set; }

		public void FromDataRow(DataRow fila)
		{
			this.Codigo = fila[0].ToString();
		}
	}
}
