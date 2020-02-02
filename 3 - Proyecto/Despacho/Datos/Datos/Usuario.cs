﻿using Datos.DB;
using System.Collections.Generic;
using System.Data;
using System.Text;

namespace Datos.Datos
{
	public class Usuario
	{
		
		public static List<Modelo.Usuario> ObtenerUsuarios()
		{
			string SELECTSentence = "SELECT *";
			string FROMSentence = " FROM Usuario";
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
			string SELECTSentence = "SELECT Usuario.UsuarioId, Usuario.Username, Usuario.Password, Usuario.Nombres, Usuario.ApellidoPaterno, Usuario.ApellidoMaterno, Usuario.Email, Usuario.PerfilId, Usuario.ClienteId, Usuario.EstaActivo";
			string FROMSentence = " FROM Usuario";
			string WHERESentence = " WHERE Usuario.Username = '" + username + "'";
			string SQLSentence = SELECTSentence + FROMSentence + WHERESentence;
			DataTable dataTable = DataBase.ExecuteReader(SQLSentence );
				
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
			builder.AppendFormat(SQLSentence, usuario.UsuarioId , usuario.Username , usuario.Password , usuario.Nombres , usuario.ApellidoPaterno , usuario.ApellidoMaterno, usuario.NombreCompleto , usuario.Email , usuario.PerfilId , usuario.ClienteId , usuario.Perfil , usuario.Cliente);

			return DataBase.ExecuteNonQuery(builder.ToString()) > 0;
		}
		
		public static bool Modificar(Modelo.Usuario usuario)
		{
			StringBuilder builder = new StringBuilder();
			string UPDATESentence = "UPDATE Usuario";
			string SETSentence= " SET Usuario.UsuarioId = '{0}', Username = '{1}',  Password = {2}, Nombres = {3}, ApellidoPaterno = {4}, ApellidoMaterno = {5}, NombreCompleto = {6}, Email = {7}, PerfilId = {8}, ClienteId = {9}, Perfil = {10}, Cliente = {11}";
			string WHERESentence= " WHERE Usuario.UsuarioId = {0}";
			string SQLSentence = UPDATESentence + SETSentence + WHERESentence;
			builder.AppendFormat(SQLSentence, usuario.UsuarioId, usuario.Username, usuario.Password, usuario.Nombres, usuario.ApellidoPaterno, usuario.ApellidoMaterno, usuario.NombreCompleto , usuario.Email, usuario.PerfilId , usuario.ClienteId , usuario.Perfil, usuario.Cliente);

			return DataBase.ExecuteNonQuery(builder.ToString()) > 0;
		}

		public static Modelo.Usuario ObtenerUsuario(int usuarioId)
		{
			Modelo.Usuario usuario = new Modelo.Usuario();
			//SELECT Usuario.UsuarioId, Usuario.Username AS NombreUsuario, Usuario.Password, Usuario.Nombres, Usuario.ApellidoPaterno, Usuario.ApellidoMaterno, Usuario.Email, Usuario.PerfilId, Usuario.ClienteId, Usuario.EstaActivo, 
      //                   Perfil.Descripcion, Cliente.Nombre
			//FROM            Usuario INNER JOIN
			//									 Perfil ON Usuario.PerfilId = Perfil.PerfilId INNER JOIN Cliente ON Usuario.ClienteId = Cliente.ClienteId
			string SELECTSentence = "SELECT Usuario.UsuarioId, Usuario.Username, Usuario.Password, Usuario.Nombres, Usuario.ApellidoPaterno, Usuario.ApellidoMaterno, Usuario.Email, Usuario.PerfilId, Usuario.ClienteId, Usuario.EstaActivo";
			string FROMSentence = " FROM Usuario";
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
	}
}
