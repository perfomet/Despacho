using System.Data;

namespace Datos.Modelo
{
	public class Cliente : Base.ModeloBase
	{
		public int ClienteId { get; set; }
		public string Nombre { get; set; }
		public string Rut { get; set; }
		public string VRut { get; set; }
		public string Sufijo { get; set; }
		public bool EstaActivo { get; set; }

		public void FromDataRow(DataRow fila)
		{
			this.ClienteId = int.Parse(fila[0].ToString());
			this.Nombre = fila[1].ToString();
			this.Rut = fila[2].ToString();
			this.VRut = fila[3].ToString();
			this.Sufijo = fila[4].ToString();
			this.EstaActivo = bool.Parse(fila[5].ToString());
		}
	}
}
