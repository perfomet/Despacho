using System.Data;

namespace Datos.Modelo
{
	public class EquipoSolicitado : Base.ModeloBase
	{
		public int EquipoSolicitadoId { get; set; }
		public string NumeroPlaca { get; set; }
		public int EstadoEquipoId { get; set; }
		public string Marca { get; set; }
		public string Modelo { get; set; }
		public int SolicitudDespachoId { get; set; }

		public void FromDataRow(DataRow fila)
		{
			this.EquipoSolicitadoId = int.Parse(fila[0].ToString());
			this.NumeroPlaca = fila[1].ToString();
			this.Marca = fila[2].ToString();
			this.Modelo = fila[3].ToString();
			this.EstadoEquipoId = int.Parse(fila[4].ToString());
			this.SolicitudDespachoId = int.Parse(fila[5].ToString());
		}
	}
}
