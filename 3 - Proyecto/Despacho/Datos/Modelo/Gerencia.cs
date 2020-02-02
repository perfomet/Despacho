using System.Data;


namespace Datos.Modelo
{
	public class Gerencia : Base.ModeloBase
	{
		public int Gerenciaid { get; set; }
		public string Descripcion { get; set; }
		public int Clienteid { get; set; }
		public bool EstaActivo { get; set; }
		public void FromDataRow(DataRow fila)
		{
			this.Gerenciaid = int.Parse(fila[0].ToString());
			this.Descripcion = fila[1].ToString();
			this.Clienteid = int.Parse(fila[2].ToString());
			this.EstaActivo = bool.Parse(fila[3].ToString());
		}
	}
}
