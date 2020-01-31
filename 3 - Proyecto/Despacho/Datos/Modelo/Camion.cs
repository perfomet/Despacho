using System.Data;


namespace Datos.Modelo
{
	public class Camion : Base.ModeloBase
	{
		public string patente { get; set; }
		public string descripcion { get; set; }
		public string empresatransporteid { get; set; }
		public bool EstaActivo { get; set; }
		public void FromDataRow(DataRow fila)
		{
			this.patente = fila[0].ToString();
			this.descripcion = fila[1].ToString();
			this.empresatransporteid = fila[2].ToString();
			this.EstaActivo = bool.Parse(fila[3].ToString());
		}
	}
}
