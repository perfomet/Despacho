using System.Data;

namespace Datos.Modelo
{
	public class EstadoEquipo : Base.ModeloBase
	{
		public int estadoequipoid { get; set; }
		public string descripcion { get; set; }
		public bool EstaActivo { get; set; }
		public void FromDataRow(DataRow fila)
		{
			this.estadoequipoid = int.Parse(fila[0].ToString());
			this.descripcion = fila[1].ToString();
			this.EstaActivo = bool.Parse(fila[2].ToString());
		}
	}
}
