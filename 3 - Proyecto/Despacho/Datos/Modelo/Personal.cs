using System.Data;

namespace Datos.Modelo
{
	public class Personal : Base.ModeloBase
	{
		public int Personalid { get; set; }
		public int RUT { get; set; }
		public string DV { get; set; }
		public string Nombre { get; set; }
		public string Primerapellido { get; set; }
		public string Segundoapellido { get; set; }
		public string Email { get; set; }
		public int Tipopersonalid { get; set; }
		public string TipoPersonal { get; set; }
		public bool EstaActivo { get; set; }
		public void FromDataRow(DataRow fila)
		{
			this.Personalid = int.Parse(fila[0].ToString());
			this.RUT = int.Parse(fila[1].ToString());
			this.DV = fila[2].ToString();
			this.Nombre = fila[3].ToString();
			this.Primerapellido = fila[4].ToString();
			this.Segundoapellido = fila[5].ToString();
			this.Email = fila[6].ToString();
			this.Tipopersonalid = int.Parse(fila[7].ToString());
			this.TipoPersonal = fila[8].ToString();
			this.EstaActivo = bool.Parse(fila[9].ToString());
		}
	}
}