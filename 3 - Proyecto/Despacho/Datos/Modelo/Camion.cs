using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Datos.Modelo
{
	public class Camion
	{
		public string Patente { get; set; }
		public string Descripcion { get; set; }
		public int EmpresaTransporteId { get; set; }
		public void FromDataRow(DataRow fila)
		{
			this.Patente = fila[0].ToString();
			this.Descripcion = fila[1].ToString();
			this.EmpresaTransporteId = int.Parse(fila[2].ToString());
		}
	}
}
