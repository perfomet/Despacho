using System.Data;

namespace Datos.Modelo
{
	public class EquipoRetirado : Base.ModeloBase
	{
		public int EquipoRetiradoId { get; set; }
		public string NumeroPlaca { get; set; }
		public int SolicitudDespachoId { get; set; }

		public void FromDataRow(DataRow fila)
		{
			this.EquipoRetiradoId = int.Parse(fila[0].ToString());
			this.NumeroPlaca = fila[1].ToString();
			this.SolicitudDespachoId = int.Parse(fila[2].ToString());
		}
	}
}
