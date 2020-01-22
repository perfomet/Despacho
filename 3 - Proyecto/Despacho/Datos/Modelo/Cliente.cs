using System.Data;

namespace Datos.Modelo
{
    public class Cliente
    {
        public int ClienteId { get; set; }
        public string Nombre { get; set; }
        public string Rut { get; set; }
        public string VRUT { get; set; }

        public void FromDataRow(DataRow fila)
        {
            this.ClienteId = int.Parse(fila[0].ToString());
            this.Nombre = fila[1].ToString();
            this.Rut = fila[2].ToString();
            this.VRUT = fila[3].ToString();
        }
    }
}
