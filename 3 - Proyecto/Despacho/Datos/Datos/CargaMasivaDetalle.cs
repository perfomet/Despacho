using Datos.DB;
using System.Collections.Generic;
using System.Data;
using System.Text;

namespace Datos.Datos
{
	public class CargaMasivaDetalle
	{
		public static List<Modelo.CargaMasivaDetalle> ObtenerCargasMasivasDetalle()
		{
			string SELECTSentence = "SELECT CargaMasiva.CargaMasivaId, CargaMasiva.UsuarioId, CargaMasiva.FechaHora, CargaMasiva.Archivo, Usuario.Username, Usuario.Nombres + ISNULL(' ' + Usuario.ApellidoPaterno, '') + ISNULL(' ' + Usuario.ApellidoMaterno, '') AS NombreUsuario, Perfil.Descripcion, Cliente.Codigo, Cliente.NombreCliente, Cliente.Prefijo";
			string FROMSentence = " FROM Usuario";
			string JOINSentence = " INNER JOIN CargaMasiva ON Usuario.UsuarioId = CargaMasiva.UsuarioId INNER JOIN Perfil ON Usuario.PerfilId = Perfil.PerfilId INNER JOIN Cliente ON Usuario.ClienteId = Cliente.ClienteId";
			string WHERESentence = "";
			string ORDERSentence = ";";
			string SQLSentence = SELECTSentence + FROMSentence + JOINSentence + WHERESentence + ORDERSentence;

			DataTable dataTable = DataBase.ExecuteReader(SQLSentence);
			List<Modelo.CargaMasivaDetalle> cargasmasivasdetalle = new List<Modelo.CargaMasivaDetalle>();

				foreach (DataRow fila in dataTable.Rows)
				{
					Modelo.CargaMasivaDetalle cargamasivadetalle = new Modelo.CargaMasivaDetalle();
					cargamasivadetalle.FromDataRow(fila);
					cargasmasivasdetalle.Add(cargamasivadetalle);
				}

				return cargasmasivasdetalle;
		}

	public static Modelo.CargaMasiva ObtenerCargaMasiva(int id)
	{
		string SELECTSentence = "SELECT CargaMasiva.CargaMasivaId, CargaMasiva.UsuarioId, CargaMasiva.FechaHora, CargaMasiva.Archivo, Usuario.Username, Usuario.Nombres + ISNULL(' ' + Usuario.ApellidoPaterno, '') + ISNULL(' ' + Usuario.ApellidoMaterno, '') AS NombreUsuario, Perfil.Descripcion, Cliente.Codigo, Cliente.NombreCliente, Cliente.Prefijo";
		string FROMSentence = " FROM Usuario";
		string JOINSentence = " INNER JOIN CargaMasiva ON Usuario.UsuarioId = CargaMasiva.UsuarioId INNER JOIN Perfil ON Usuario.PerfilId = Perfil.PerfilId INNER JOIN Cliente ON Usuario.ClienteId = Cliente.ClienteId";
		string WHERESentence = "";
		string ORDERSentence = ";";
		string SQLSentence = SELECTSentence + FROMSentence + JOINSentence + WHERESentence + ORDERSentence;

		Modelo.CargaMasiva cargamasiva = new Modelo.CargaMasiva();
		DataTable dataTable = DataBase.ExecuteReader(SQLSentence);

		if (dataTable.Rows.Count > 0)
		{
			DataRow fila = dataTable.Rows[0];
			cargamasiva.FromDataRow(fila);
		}

		return cargamasiva;
	}

	public static bool Crear(Modelo.CargaMasiva cargamasiva)
	{
		string INSERTSentence = "INSERT INTO CargaMasiva (UsuarioId, FechaHora, Archivo)";
		string VALUESSentence = " VALUES({1}, '{2}', '{3}');";
		string SQLSentence = INSERTSentence + VALUESSentence;
		StringBuilder builder = new StringBuilder();
		builder.AppendFormat(SQLSentence, cargamasiva.UsuarioId, cargamasiva.FechaHora, cargamasiva.Archivo);
		return DataBase.ExecuteNonQuery(builder.ToString()) > 0;
	}

		public static bool Modificar(Modelo.CargaMasiva cargamasiva)
		{
			string UPDATESentence = "UPDATE CargaMasiva";
			string SETSentence = " SET UsuarioId = {1}, FechaHora = '{2}', Archivo = '{3}'";
			string WHERESentence = " WHERE CargaMasivaId = {0}";
			string SQLSentence = UPDATESentence + SETSentence + WHERESentence;
			StringBuilder builder = new StringBuilder();
			builder.AppendFormat(SQLSentence, cargamasiva.CargaMasivaId, cargamasiva.UsuarioId, cargamasiva.FechaHora, cargamasiva.Archivo);

			return DataBase.ExecuteNonQuery(builder.ToString()) > 0;
		}
	}
}

