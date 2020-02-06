using System.Data;


namespace Datos.Modelo
{
	public class BinToEstadoEquipo : Base.ModeloBase
	{
		public int Bintoestadoequipoid { get; set; }
		public int Estadoequipoid { get; set; }
		public string Bin { get; set; }

		public void FromDataRow(DataRow fila)
		{
			this.Bintoestadoequipoid = int.Parse(fila[0].ToString());
			this.Estadoequipoid = int.Parse(fila[1].ToString());
			this.Bin = fila[2].ToString();
		}
	}
}