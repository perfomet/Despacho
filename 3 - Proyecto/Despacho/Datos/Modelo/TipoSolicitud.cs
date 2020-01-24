using System.Data;

namespace Datos.Modelo
{
	public class TipoSolicitud
	{
    public int TipoSolicitudId { get; set; }
    public string Descripcion { get; set; }
    public string Observaciones { get; set; }
    public void FromDataRow(DataRow fila)
    {
      this.TipoSolicitudId = int.Parse(fila[0].ToString());
      this.Descripcion = fila[1].ToString();
      this.Observaciones = fila[2].ToString();
     }
  }
}

