using System.Data;


namespace Datos.Modelo
{
	public class Camion : Base.ModeloBase
	{
		public string Patente { get; set; }
		public string Descripcion { get; set; }
		public int Empresatransporteid { get; set; }
		public string Empresatransporte { get; set; }
		public bool EstaActivo { get; set; }

		
		public void FromDataRow(DataRow fila)
		{
			this.Patente = fila[0].ToString();
			this.Descripcion = fila[1].ToString();
			this.Empresatransporteid = int.Parse(fila[2].ToString());
			this.Empresatransporte = fila[3].ToString();
			this.EstaActivo = bool.Parse(fila[4].ToString());
			
		}
	}
}
