using Datos.DB;
using System.Collections.Generic;
using System.Data;

namespace Datos.Datos
{
    public class Usuario
    {
        public static List<Modelo.Usuario> ObtenerUsuarios()
        {
            DataTable dataTable = DataBase.ExecuteReader("SELECT * FROM Usuario");
            List<Modelo.Usuario> usuarios = new List<Modelo.Usuario>();

            foreach (DataRow fila in dataTable.Rows)
            {
                Modelo.Usuario usuario = new Modelo.Usuario();

                usuario.FromDataRow(fila);

                usuarios.Add(usuario);
            }

            return usuarios;
        }

        public static Modelo.Usuario ObtenerUsuario(string username)
        {
            Modelo.Usuario usuario = new Modelo.Usuario();

            DataTable dataTable = DataBase.ExecuteReader("SELECT * FROM Usuario WHERE Username = '" + username + "'");

            if (dataTable.Rows.Count > 0)
            {
                DataRow fila = dataTable.Rows[0];
                usuario.FromDataRow(fila);
            }

            return usuario;
        }
    }
}
