using System.Data;

namespace Datos.Modelo
{
	public class EstadoSolicitud : Base.ModeloBase
	{
		public int EstadoSolicitudId { get; set; }
		public string Descripcion { get; set; }
		public bool EstaActivo { get; set; }
		public void FromDataRow(DataRow fila)
		{
			this.EstadoSolicitudId = int.Parse(fila[0].ToString());
			this.Descripcion = fila[1].ToString();
			this.EstaActivo = bool.Parse(fila[2].ToString());
		}
	}
}
