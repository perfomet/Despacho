using System.Data;

namespace Datos.Modelo
{
	public class EstadoEquipo : Base.ModeloBase
	{
		public int EstadoEquipoId { get; set; }
		public string Descripcion { get; set; }

		public void FromDataRow(DataRow fila)
		{
			this.EstadoEquipoId = int.Parse(fila[0].ToString());
			this.Descripcion = fila[1].ToString();
		}
	}
}
