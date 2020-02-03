using System.Data;

namespace Datos.Modelo
{
	public class EmpresaTransporte: Base.ModeloBase
	{
		public int EmpresaTransporteId { get; set; }
		public string Nombre { get; set; }
		public bool EsPropia { get; set; }
		public bool EstaActivo { get; set; }
		public void FromDataRow(DataRow fila)
		{
			this.EmpresaTransporteId = int.Parse(fila[0].ToString());
			this.Nombre = fila[1].ToString();
			this.EsPropia = bool.Parse(fila[2].ToString());
			this.EstaActivo = bool.Parse(fila[3].ToString());
		}
	}
}
