using System.Data;

namespace Datos.Modelo
{
	public class EquipoSolicitado : Base.ModeloBase
	{
		public int EquipoSolicitadoId { get; set; }
		public string NumeroPlaca { get; set; }
		public int EstadoEquipoId { get; set; }
		public string Modelo { get; set; }
		public int SolicitudDespachoId { get; set; }
		public EstadoEquipo EstadoEquipo { get { return Datos.EstadoEquipo.ObtenerEstadoEquipo(this.EstadoEquipoId); } }

		public void FromDataRow(DataRow fila)
		{
			this.EquipoSolicitadoId = int.Parse(fila[0].ToString());
			this.NumeroPlaca = fila[1].ToString();
			this.Modelo = fila[2].ToString();
			this.EstadoEquipoId = int.Parse(fila[3].ToString());
			this.SolicitudDespachoId = int.Parse(fila[4].ToString());
		}
	}
}
