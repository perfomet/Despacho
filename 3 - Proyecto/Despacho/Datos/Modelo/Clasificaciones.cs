using System.Data;


namespace Datos.Modelo
{
	public class Clasificacion : Base.ModeloBase
	{
		public int Clasificacionid { get; set; }
		public decimal Cantidad { get; set; }
		public int Unidadmedidaid { get; set; }
		public bool EstaActivo { get; set; }

		public string UnidadMedida { get; set; }

		public void FromDataRow(DataRow fila)
		{
			this.Clasificacionid = int.Parse(fila[0].ToString());
			this.Cantidad = decimal.Parse(fila[1].ToString());
			this.Unidadmedidaid = int.Parse(fila[2].ToString());
			this.EstaActivo = bool.Parse(fila[3].ToString());
			this.UnidadMedida = fila[4].ToString();
		}
	}
}
