using System.Data;

namespace Datos.Modelo
{
	public class UnidadNegocio : Base.ModeloBase
	{
		public int UnidadNegocioId { get; set; }
		public string Descripcion { get; set; }
		public int ClienteId { get; set; }
		public void FromDataRow(DataRow fila)
		{
			this.UnidadNegocioId = int.Parse(fila[0].ToString());
			this.Descripcion = fila[1].ToString();
			this.ClienteId = int.Parse(fila[2].ToString());
		}
	}
}
