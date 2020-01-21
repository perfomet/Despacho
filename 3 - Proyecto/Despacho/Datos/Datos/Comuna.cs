using Datos.DB;
using System.Collections.Generic;
using System.Data;

namespace Datos.Datos
{
    public class Comuna
    {
        public static List<Modelo.Comuna> ObtenerComunas()
        {
            DataTable dataTable = DataBase.ExecuteReader("SELECT * FROM Comuna");
            List<Modelo.Comuna> comunas = new List<Modelo.Comuna>();

            foreach (DataRow fila in dataTable.Rows)
            {
                Modelo.Comuna comuna = new Modelo.Comuna();

                comuna.FromDataRow(fila);

                comunas.Add(comuna);
            }

            return comunas;
        }

        public static List<Modelo.Comuna> ObtenerComunas(int id, TipoFiltroComuna tipo)
        {
            string tipoFiltro = "RegionId";

            switch (tipo)
            {
                case TipoFiltroComuna.ProvinciaId:
                    tipoFiltro = "ProvinciaId";
                    break;
                case TipoFiltroComuna.RegionId:
                    tipoFiltro = "RegionId";
                    break;
            }

            DataTable dataTable = DataBase.ExecuteReader("SELECT * FROM Comuna WHERE " + tipoFiltro + " = " + id);
            List<Modelo.Comuna> comunas = new List<Modelo.Comuna>();

            foreach (DataRow fila in dataTable.Rows)
            {
                Modelo.Comuna comuna = new Modelo.Comuna();

                comuna.FromDataRow(fila);

                comunas.Add(comuna);
            }

            return comunas;
        }

        public static Modelo.Comuna ObtenerComuna(int comunaId)
        {
            Modelo.Comuna comuna = new Modelo.Comuna();

            DataTable dataTable = DataBase.ExecuteReader("SELECT * FROM Comuna WHERE ComunaId = " + comunaId);

            if (dataTable.Rows.Count > 0)
            {
                DataRow fila = dataTable.Rows[0];
                comuna.FromDataRow(fila);
            }

            return comuna;
        }
    }

    public enum TipoFiltroComuna
    {
        RegionId = 1,
        ProvinciaId = 2
    }
}
