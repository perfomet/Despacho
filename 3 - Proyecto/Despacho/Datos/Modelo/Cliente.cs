using System.Data;

namespace Datos.Modelo
{
	public class Cliente : Base.ModeloBase
	{
		public int ClienteId { get; set; }
		public string Codigo { get; set; }
		public string Nombre { get; set; }
		public string Rut { get; set; }
		public string VRut { get; set; }
		public string Prefijo { get; set; }
		public bool EstaActivo { get; set; }

		public void FromDataRow(DataRow fila)
		{
			
			this.ClienteId = int.Parse(fila[0].ToString());
			this.Codigo = fila[1].ToString();
			this.Nombre = fila[2].ToString();
			this.Rut = fila[3].ToString();
			//this.VRut = Datos.Internos.Verirut(this.Rut, Datos.Internos.schile);
			this.VRut = fila[4].ToString();
			this.Prefijo = fila[5].ToString();
			this.EstaActivo = bool.Parse(fila[6].ToString());
		}
	}
}
