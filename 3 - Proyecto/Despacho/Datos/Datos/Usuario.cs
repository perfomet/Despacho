using Datos.DB;
using System.Data;

namespace Datos.Datos
{
    public class Usuario
    {
        public static DataTable ObtenerUsuarios()
        {
            DataTable dataTable = DataBase.ExecuteReader("SELECT * FROM Usuario");

            return dataTable;
        }
    }
}
