using System;
using System.Data;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Datos.Modelo
{
	public class TipoDocumento
	{
    public int Tipodocumentoid { get; set; }
    public string Descripcion { get; set; }
    public bool EstaActivo { get; set; }
    public void FromDataRow(DataRow fila)
    {
      this.Tipodocumentoid = int.Parse(fila[0].ToString());
      this.Descripcion = fila[1].ToString();
      this.EstaActivo = bool.Parse(fila[2].ToString());
    }
  }
}
