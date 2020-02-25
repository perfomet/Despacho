using System.Data;

namespace Datos.Modelo
{
	public class SeccionFormulario : Base.ModeloBase
	{
		public int SeccionFormularioId { get; set; }
		public string Descripcion { get; set; }
		public string Clase { get; set; }
		public string Vista { get; set; }
		public bool EstaActivo { get; set; }
		public void FromDataRow(DataRow fila)
		{
			this.SeccionFormularioId = int.Parse(fila[0].ToString());
			this.Descripcion = fila[1].ToString();
			this.Clase = fila[2].ToString();
			this.Vista = fila[3].ToString();
			this.EstaActivo = bool.Parse(fila[4].ToString());
		}
	}

	public enum SeccionesFormulario
	{
		Solicitud = 1,
		Planificacion = 2,
		Documentacion = 3,
		Concrecion = 4,
		Aprobacion = 5
	}
}
