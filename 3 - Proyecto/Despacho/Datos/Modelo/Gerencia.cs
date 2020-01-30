using System.Data;


namespace Datos.Modelo
{
	public class Gerencia : Base.ModeloBase
	{
		public int GerenciaId { get; set; }
		public string Descripcion { get; set; }
		public int ClienteId { get; set; }
		public bool EstaActivo { get; set; }
		public void FromDataRow(DataRow fila)
		{
			this.GerenciaId = int.Parse(fila[0].ToString());
			this.Descripcion = fila[1].ToString();
			this.ClienteId = int.Parse(fila[2].ToString());
			this.EstaActivo = bool.Parse(fila[4].ToString());
		}
	}
}
