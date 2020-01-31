using System.Data;


namespace Datos.Modelo
{
	public class Camion : Base.ModeloBase
	{
		public string patente { get; set; }
		public string descripcion { get; set; }
		public string empresatransporte { get; set; }
		public string espropia { get; set; }
		public bool estaactivo { get; set; }
		public void FromDataRow(DataRow fila)
		{
			this.patente = fila[0].ToString();
			this.descripcion = fila[1].ToString();
			this.empresatransporte = fila[2].ToString();
			this.espropia = fila[3].ToString();
			this.estaactivo = bool.Parse(fila[4].ToString());
		}
	}
}
