using System.Data;


namespace Datos.Modelo
{
	public class Camion : Base.ModeloBase
	{
		public string Patente { get; set; }
		public string Descripcion { get; set; }
		public string Empresatransporteid { get; set; }
		public bool EstaActivo { get; set; }
		public void FromDataRow(DataRow fila)
		{
			this.Patente = fila[0].ToString();
			this.Descripcion = fila[1].ToString();
			this.Empresatransporteid = fila[2].ToString();
			this.EstaActivo = bool.Parse(fila[3].ToString());
		}
	}
}
