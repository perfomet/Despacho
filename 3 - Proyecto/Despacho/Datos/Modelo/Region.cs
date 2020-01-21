using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Datos.Modelo
{
    public class Region
    {
        public int RegionId { get; set; }
        public string Nombre { get; set; }

        public void FromDataRow(DataRow fila)
        {
            this.RegionId = int.Parse(fila[0].ToString());
            this.Nombre = fila[1].ToString();
        }
    }
}
