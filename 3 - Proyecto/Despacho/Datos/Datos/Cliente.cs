using Datos.DB;
using System.Collections.Generic;
using System.Data;
using System.Text;

namespace Datos.Datos
{
    public class Cliente
    {
        public static List<Modelo.Cliente> ObtenerClientes()
        {
            DataTable dataTable = DataBase.ExecuteReader("SELECT * FROM Cliente");
            List<Modelo.Cliente> clientes = new List<Modelo.Cliente>();

            foreach (DataRow fila in dataTable.Rows)
            {
                Modelo.Cliente cliente = new Modelo.Cliente();

                cliente.FromDataRow(fila);

                clientes.Add(cliente);
            }

            return clientes;
        }

        public static Modelo.Cliente ObtenerCliente(int clienteId)
        {
            Modelo.Cliente cliente = new Modelo.Cliente();

            DataTable dataTable = DataBase.ExecuteReader("SELECT * FROM Cliente WHERE ClienteId = '" + clienteId + "'");

            if (dataTable.Rows.Count > 0)
            {
                DataRow fila = dataTable.Rows[0];
                cliente.FromDataRow(fila);
            }

            return cliente;
        }

        public static bool Crear(Modelo.Cliente cliente)
        {
            StringBuilder builder = new StringBuilder();
            builder.AppendFormat("INSERT INTO Cliente VALUES ('{0}', '{1}')", cliente.Nombre, cliente.Rut, cliente.VRUT);

            return DataBase.ExecuteNonQuery(builder.ToString()) > 0;
        }

        public static bool Modificar(Modelo.Cliente cliente)
        {
            StringBuilder builder = new StringBuilder();
            builder.AppendFormat("UPDATE Cliente SET Nombre = '{0}', RUT = '{1}' WHERE ClienteId = {2}", cliente.Nombre, cliente.Rut, cliente.VRUT, cliente.ClienteId);

            return DataBase.ExecuteNonQuery(builder.ToString()) > 0;
        }

        public static bool Eliminar(int clienteId)
        {
            StringBuilder builder = new StringBuilder();
            builder.AppendFormat("DELETE FROM Cliente WHERE ClienteId = {0}", clienteId);

            return DataBase.ExecuteNonQuery(builder.ToString()) > 0;
        }
    }
}
