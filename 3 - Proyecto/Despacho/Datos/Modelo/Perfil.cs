using System.Data;

namespace Datos.Modelo
{
	public class Perfil : Base.ModeloBase
	{
		public int PerfilId { get; set; }
		public string Descripcion { get; set; }
		public void FromDataRow(DataRow fila)
		{
			this.PerfilId = int.Parse(fila[0].ToString());
			this.Descripcion = fila[1].ToString();
			throw new System.NotImplementedException();
		}
	}
}
