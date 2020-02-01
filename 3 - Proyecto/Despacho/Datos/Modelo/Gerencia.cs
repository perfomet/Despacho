using System.Data;


namespace Datos.Modelo
{
	public class Gerencia : Base.ModeloBase
	{
		public int gerenciaid { get; set; }
		public string descripcion { get; set; }
		public int clienteid { get; set; }
		public bool EstaActivo { get; set; }
		public void FromDataRow(DataRow fila)
		{
			this.gerenciaid = int.Parse(fila[0].ToString());
			this.descripcion = fila[1].ToString();
			this.clienteid = int.Parse(fila[2].ToString());
			this.EstaActivo = bool.Parse(fila[3].ToString());
		}
	}
}
