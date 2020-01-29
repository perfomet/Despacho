using System.Data;

namespace Datos.Modelo
{
	public class Prioridad : Base.ModeloBase
	{
		public int PrioridadId { get; set; }
		public string Descripcion { get; set; }
		public void FromDataRow(DataRow fila)
		{
			this.PrioridadId = int.Parse(fila[0].ToString());
			this.Descripcion = fila[1].ToString();

		}
	}
}
