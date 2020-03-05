using Datos._librerias;
using System;
using System.Data;
using System.Data.Common;
using System.Data.Odbc;
using System.Threading.Tasks;

namespace Datos.DB
{
    class DataBase
    {
        #region Conexión

        static OdbcConnection Con;

        static OdbcConnection Conectar()
        {
            try
            {
                if (Con != null)
                {
                    Con.Close();
                    Con.Dispose();
                }

                Con = new OdbcConnection(LibConfig.ObtenerConnectionString());

                Con.Open();

                return Con;
            }
            catch (Exception ex) { }

            return null;
        }

        public static void CambiarBaseDatos(string DB)
        {
            Con.ChangeDatabase(DB);
        }

        public static OdbcConnection ObtenerConexion()
        {
            if (Con == null || Con.State != ConnectionState.Open)
                return Conectar();
            else
                return Con;
        }

        #endregion

        #region Métodos de Ejecución

        /// <summary>
        /// Se puede utilizar para ejecutar sentencias sin retorno como Insert, Update o Delete, esta se realizará de manera sincrónica
        /// </summary>
        /// <param name="query"></param>
        /// <returns int></returns>
        public static int ExecuteNonQuery(string query)
        {
            try
            {
                OdbcCommand cmd;

                cmd = new OdbcCommand(query, ObtenerConexion());

                return cmd.ExecuteNonQuery();
            }
            catch (Exception ex) { }

            return -1;
        }

        /// <summary>
        /// Se puede utilizar para ejecutar sentencias sin retorno como Insert, Update o Delete, esta se realizará de manera asincrónica
        /// </summary>
        /// <param name="query"></param>
        public static async Task<int> ExecuteNonQueryAsync(string query)
        {
            try
            {
                OdbcCommand cmd;

                cmd = new OdbcCommand(query, ObtenerConexion());

                return await cmd.ExecuteNonQueryAsync();
            }
            catch (Exception ex) { }

            return -1;
        }

        /// <summary>
        /// Se puede utilizar para ejecutar sentencias de retorno múltiple o tipo tabla como procedimientos almacenados o select con múltiples clumnas de manera sincrónica
        /// </summary>
        /// <param name="query"></param>
        public static DataTable ExecuteReader(string query)
        {
            DataTable tabla = new DataTable();
            tabla.TableName = "Resultado";

            try
            {
                OdbcCommand cmd;

                cmd = new OdbcCommand(query, ObtenerConexion());

                OdbcDataReader reader = cmd.ExecuteReader();

                tabla.Load(reader);
            }
            catch (Exception ex) { }

            return tabla;
        }

        /// <summary>
        /// Se puede utilizar para ejecutar sentencias de retorno múltiple o tipo tabla como procedimientos almacenados, funciones de tipo tabla o select con múltiples clumnas de manera asincrónica
        /// </summary>
        /// <param name="query"></param>
        public static async Task<DataTable> ExecuteReaderAsync(string query)
        {
            DataTable tabla = new DataTable();
            tabla.TableName = "Resultado";

            try
            {
                OdbcCommand cmd;

                cmd = new OdbcCommand(query, ObtenerConexion());

                DbDataReader reader = await cmd.ExecuteReaderAsync();

                tabla.Load(reader);
            }
            catch (Exception ex) { }

            return tabla;
        }

        /// <summary>
        /// Se puede utilizar para ejecutar sentencias de retorno simple como funciones escalares o select con solo una clumna de manera sincrónica
        /// </summary>
        /// <param name="query"></param>
        public static object ExecuteScalar(string query)
        {
            try
            {
                OdbcCommand cmd;

                cmd = new OdbcCommand(query, ObtenerConexion());

                return cmd.ExecuteScalar();
            }
            catch (Exception ex) { }

            return null;
        }

        /// <summary>
        /// Se puede utilizar para ejecutar sentencias de retorno simple como funciones escalares o select con solo una clumna de manera asincrónica
        /// </summary>
        /// <param name="query"></param>
        public static async Task<object> ExecuteScalarAsync(string query)
        {
            try
            {
                OdbcCommand cmd;

                cmd = new OdbcCommand(query, ObtenerConexion());

                return await cmd.ExecuteScalarAsync();
            }
            catch (Exception ex) { }

            return null;
        }

        #endregion
    }
}
