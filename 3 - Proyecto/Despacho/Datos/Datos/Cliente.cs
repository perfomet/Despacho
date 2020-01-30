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
			string SELECTSentence = "SELECT *";
			string FROMSentence = " FROM Cliente";
			string WHERESentence = "";
			string ORDERSentence = ";";
			string SQLSentence = SELECTSentence + FROMSentence + WHERESentence + ORDERSentence;
			DataTable dataTable = DataBase.ExecuteReader(SQLSentence);
			List<Modelo.Cliente> clientes = new List<Modelo.Cliente>();
			foreach (DataRow fila in dataTable.Rows)
			{
				Modelo.Cliente cliente = new Modelo.Cliente();
				cliente.FromDataRow(fila);
				clientes.Add(cliente);
			}

			return clientes;
		}

		public static Modelo.Cliente ObtenerCliente(int Id)
		{

			string SELECTSentence = "SELECT *";
			string FROMSentence = " FROM Cliente";
			string WHERESentence = " WHERE ClienteId = " + Id;
			string ORDERSentence = ";";
			string SQLSentence = SELECTSentence + FROMSentence + WHERESentence + ORDERSentence;
			Modelo.Cliente cliente = new Modelo.Cliente();
			DataTable dataTable = DataBase.ExecuteReader(SQLSentence);

			if (dataTable.Rows.Count > 0)
			{
				DataRow fila = dataTable.Rows[0];
				cliente.FromDataRow(fila);
			}

			return cliente;
		}

		public static bool Crear(Modelo.Cliente cliente)
		{
			string INSERTSentence = "INSERT INTO Cliente";
			//cliente.VRut = Internos.Verirut(cliente.Rut, Internos.schile);
			string VALUESSentence = " VALUES('{0}', '{1}', '{2}', '{3}', '{4}', 1);";
			string SQLSentence = INSERTSentence + VALUESSentence;
			StringBuilder builder = new StringBuilder();
			builder.AppendFormat(SQLSentence, cliente.Codigo, cliente.Nombre, cliente.Rut, cliente.VRut, cliente.Prefijo);
			return DataBase.ExecuteNonQuery(builder.ToString()) > 0;
		}

		public static bool Modificar(Modelo.Cliente cliente)
		{
			string UPDATESentence = "UPDATE Cliente";
			//cliente.VRut = Internos.Verirut(cliente.Rut, Internos.schile);
			string SETSentence = " SET Codigo = '{1}', Nombre = '{2}', RUT = '{3}', VRUT = '{4}', Prefijo = '{5}'";
			string WHERESentence = " WHERE ClienteId = {0}";
			string SQLSentence = UPDATESentence + SETSentence + WHERESentence;
			StringBuilder builder = new StringBuilder();
			builder.AppendFormat(SQLSentence, cliente.ClienteId, cliente.Codigo, cliente.Nombre, cliente.Rut, cliente.VRut, cliente.Prefijo);

			return DataBase.ExecuteNonQuery(builder.ToString()) > 0;
		}

		public static bool EstaActivo(int clienteId)
		{
			string UPDATESentence = "UPDATE Cliente";
			string SETSentence = " SET EstaActivo = CASE WHEN EstaActivo = 1 THEN 0 ELSE 1 END";
			string WHERESentence = " WHERE ClienteId = {0}";
			string SQLSentence = UPDATESentence + SETSentence + WHERESentence;
			StringBuilder builder = new StringBuilder();
			builder.AppendFormat(SQLSentence, clienteId);
			return DataBase.ExecuteNonQuery(builder.ToString()) > 0;
		}
	}
}
