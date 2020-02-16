using Datos.DB;
using System.Collections.Generic;
using System.Data;
using System.Text;

namespace Datos.Datos
{
	public class Usuario
	{
		
		public static List<Modelo.Usuario> ObtenerUsuarios()
		{
			string SELECTSentence = "SELECT Usuario.UsuarioId, Usuario.Username, Usuario.Password, Usuario.Nombres, Usuario.ApellidoPaterno, Usuario.ApellidoMaterno, Usuario.Email, Usuario.PerfilId, Perfil.Descripcion AS Rol, Usuario.ClienteId, Cliente.Nombre AS Clientenombre, Usuario.EstaActivo";
			string FROMSentence = " FROM Perfil INNER JOIN Usuario ON Perfil.PerfilId = Usuario.PerfilId LEFT OUTER JOIN Cliente ON Usuario.ClienteId = Cliente.ClienteId";
			string SQLSentence = SELECTSentence + FROMSentence;
			DataTable dataTable = DataBase.ExecuteReader(SQLSentence);
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
			string SELECTSentence = "SELECT Usuario.UsuarioId, Usuario.Username, Usuario.Password, Usuario.Nombres, Usuario.ApellidoPaterno, Usuario.ApellidoMaterno, Usuario.Email, Usuario.PerfilId, Perfil.Descripcion AS Rol, Usuario.ClienteId, Cliente.Nombre AS Clientenombre, Usuario.EstaActivo";
			string FROMSentence = " FROM Perfil INNER JOIN Usuario ON Perfil.PerfilId = Usuario.PerfilId LEFT OUTER JOIN Cliente ON Usuario.ClienteId = Cliente.ClienteId";
			string WHERESentence = " WHERE Usuario.Username = '" + username + "'";
			string SQLSentence = SELECTSentence + FROMSentence + WHERESentence;
			DataTable dataTable = DataBase.ExecuteReader(SQLSentence);

			if (dataTable.Rows.Count > 0)
			{
				DataRow fila = dataTable.Rows[0];
				usuario.FromDataRow(fila);
			}

			return usuario;
		}
		public static bool Crear(Modelo.Usuario usuario)
		{
			StringBuilder builder = new StringBuilder();
			string INSERTSentence = "INSERT INTO Usuario";
			string VALUESSentence = " VALUES ({0}, '{1}', '{2}', '{3}', '{4}', '{5}', '{6}', '{7}', {8}, {9}, {10}, {11}, 1)";
			string SQLSentence = INSERTSentence + VALUESSentence;
			builder.AppendFormat(SQLSentence, usuario.UsuarioId, usuario.Username, usuario.Password, usuario.Nombres, usuario.ApellidoPaterno, usuario.ApellidoMaterno, usuario.NombreCompleto, usuario.Email, usuario.PerfilId, usuario.ClienteId, usuario.Perfil, usuario.Cliente);

			return DataBase.ExecuteNonQuery(builder.ToString()) > 0;
		}
		public static bool Modificar(Modelo.Usuario usuario)
		{
			StringBuilder builder = new StringBuilder();
			string UPDATESentence = "UPDATE Usuario";
			string SETSentence = " SET Usuario.Username = '{1}',  Password = '{2}', Nombres = '{3}', ApellidoPaterno = '{4}', ApellidoMaterno = '{5}', Email = '{6}', PerfilId = {7}, ClienteId = {8}";
			string WHERESentence = " WHERE Usuario.UsuarioId = {0}";
			string SQLSentence = UPDATESentence + SETSentence + WHERESentence;
			builder.AppendFormat(SQLSentence, usuario.UsuarioId, usuario.Username, usuario.Password, usuario.Nombres, usuario.ApellidoPaterno, usuario.ApellidoMaterno, usuario.Email, usuario.PerfilId, usuario.ClienteId);

			return DataBase.ExecuteNonQuery(builder.ToString()) > 0;
		}
		public static Modelo.Usuario ObtenerUsuario(int usuarioId)
		{
			Modelo.Usuario usuario = new Modelo.Usuario();

			string SELECTSentence = "SELECT Usuario.UsuarioId, Usuario.Username, Usuario.Password, Usuario.Nombres, Usuario.ApellidoPaterno, Usuario.ApellidoMaterno, Usuario.Email, Usuario.PerfilId, Perfil.Descripcion AS Rol, Usuario.ClienteId, Cliente.Nombre AS Clientenombre, Usuario.EstaActivo";
			string FROMSentence = " FROM Perfil INNER JOIN Usuario ON Perfil.PerfilId = Usuario.PerfilId LEFT OUTER JOIN Cliente ON Usuario.ClienteId = Cliente.ClienteId";
			string WHERESentence = " WHERE Usuario.UsuarioId = " + usuarioId.ToString();
			string SQLSentence = SELECTSentence + FROMSentence + WHERESentence;
			DataTable dataTable = DataBase.ExecuteReader(SQLSentence);

			if (dataTable.Rows.Count > 0)
			{
				DataRow fila = dataTable.Rows[0];
				usuario.FromDataRow(fila);
			}

			return usuario;
		}
		public static bool EstaActivo(int Id)
		{
			string UPDATESentence = "UPDATE Usuario";
			string SETSentence = " SET Usuario.EstaActivo = CASE WHEN Usuario.EstaActivo = 1 THEN 0 ELSE 1 END";
			string WHERESentence = " WHERE Usuario.UsuarioId = {0}";
			string SQLSentence = UPDATESentence + SETSentence + WHERESentence;
			StringBuilder builder = new StringBuilder();
			builder.AppendFormat(SQLSentence, Id);
			return DataBase.ExecuteNonQuery(builder.ToString()) > 0;
		}
		public static Modelo.Usuario ObtenerCargaMasiva(int idcargamasiva)
		{
			Modelo.Usuario usuario = new Modelo.Usuario();
			string SELECTSentence = "SELECT Usuario.*";
			string FROMSentence = " FROM Usuario INNER JOIN CargaMasiva ON CargaMasiva.UsuarioId = Usuario.UsuarioId";
			string WHERESentence = " WHERE CargaMasiva.IdCargaMasiva = " + idcargamasiva.ToString();
			string SQLSentence = SELECTSentence + FROMSentence + WHERESentence;
			DataTable dataTable = DataBase.ExecuteReader(SQLSentence);

			if (dataTable.Rows.Count > 0)
			{
				DataRow fila = dataTable.Rows[0];
				usuario.FromDataRow(fila);
			}

			return usuario;
		}
		public static List<Modelo.Usuario> ObtenerUsuariosCargaMasiva()
		{
			string SELECTSentence = "SELECT Usuario.UsuarioId, Usuario.Username, Usuario.Password, Usuario.Nombres, Usuario.ApellidoPaterno, Usuario.ApellidoMaterno, Usuario.Email, Usuario.PerfilId, Perfil.Descripcion AS TipoPerfil, Usuario.ClienteId, Cliente.Nombre AS ClienteNombre, Usuario.EstaActivo";
			string FROMSentence = " FROM Usuario";
			string INNERJOINSentence= " INNER JOIN CargaMasiva ON CargaMasiva.UsuarioId = Usuario.UsuarioId INNER JOIN Cliente ON Usuario.ClienteId = Cliente.ClienteId INNER JOIN Perfil ON Usuario.PerfilId = Perfil.PerfilId";
			string SQLSentence = SELECTSentence + FROMSentence + INNERJOINSentence;
			DataTable dataTable = DataBase.ExecuteReader(SQLSentence);
			List<Modelo.Usuario> usuarios = new List<Modelo.Usuario>();

			foreach (DataRow fila in dataTable.Rows)
			{
				Modelo.Usuario usuario = new Modelo.Usuario();

				usuario.FromDataRow(fila);

				usuarios.Add(usuario);
			}

			return usuarios;
		}
	}
}
