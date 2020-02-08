using System.Data;


namespace Datos.Modelo
{
	public class TipoPersonal
	{
		public int TipoPersonalId { get; set; }
		public string Descripcion { get; set; }
		
		public void FromDataRow(DataRow fila)
		{
			this.TipoPersonalId = int.Parse(fila[0].ToString());
			this.Descripcion = fila[1].ToString();
		}
	}
}
