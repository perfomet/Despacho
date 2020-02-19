using System.Data;

namespace Datos.Modelo
{
	public class PersonalAsignado : Base.ModeloBase
	{
		public int PersonalId { get; set; }
		public int SolicitudDespachoId { get; set; }

		public Modelo.Personal Personal { get { return Datos.Personal.ObtenerPersonal(PersonalId); } }

		public void FromDataRow(DataRow fila)
		{
			this.SolicitudDespachoId = int.Parse(fila[0].ToString());
			this.PersonalId = int.Parse(fila[1].ToString());
		}
	}
}
