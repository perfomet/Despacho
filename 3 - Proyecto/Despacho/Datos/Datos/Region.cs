using Datos.DB;
using System.Collections.Generic;
using System.Data;

namespace Datos.Datos
{
    public class Region
    {
        public static List<Modelo.Region> ObtenerRegiones()
        {
            DataTable dataTable = DataBase.ExecuteReader("SELECT * FROM Region");
            List<Modelo.Region> regiones = new List<Modelo.Region>();

            foreach (DataRow fila in dataTable.Rows)
            {
                Modelo.Region region = new Modelo.Region();

                region.FromDataRow(fila);

                regiones.Add(region);
            }

            return regiones;
        }

        public static Modelo.Region ObtenerRegion(int regionId)
        {
            Modelo.Region region = new Modelo.Region();

            DataTable dataTable = DataBase.ExecuteReader("SELECT * FROM Region WHERE RegionId = " + regionId);

            if (dataTable.Rows.Count > 0)
            {
                DataRow fila = dataTable.Rows[0];
                region.FromDataRow(fila);
            }

            return region;
        }
    }
}
