using System.Data;

namespace Datos.Modelo
{
	public class TipoSolicitud : Base.ModeloBase
	{
		public int Tiposolicitudid { get; set; }
		public string Descripcion { get; set; }
		public string Observaciones { get; set; }
		public bool EstaActivo { get; set; }
		public void FromDataRow(DataRow fila)
		{
			this.Tiposolicitudid = int.Parse(fila[0].ToString());
			this.Descripcion = fila[1].ToString();
			this.Observaciones = fila[2].ToString();
			this.EstaActivo = bool.Parse(fila[3].ToString());

		}
	}
}

