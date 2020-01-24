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
    public int TipoDocumentoId { get; set; }
    public string Descripcion { get; set; }
 
    public void FromDataRow(DataRow fila)
    {
      this.TipoDocumentoId = int.Parse(fila[0].ToString());
      this.Descripcion = fila[1].ToString();
    }
  }
}
