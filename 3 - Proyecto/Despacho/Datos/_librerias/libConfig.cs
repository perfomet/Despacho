using System.Configuration;
using System.Text;

namespace Datos._librerias
{
	public static class LibConfig
	{
		private const string ServerKey = "Server";
		const string DbKey = "DB";
		const string DbExistenciasKey = "DBExistencias";
		const string TablaExistenciasKey = "TablaExistencias";
		const string UserKey = "User";
		const string PasswordKey = "Password";

		public static string DbExistencias => LeerTexto(DbExistenciasKey);
		public static string TablaExistencias => LeerTexto(TablaExistenciasKey);
		public static string Db => LeerTexto(DbKey);

		static string LeerTexto(string key)
		{
			return ConfigurationManager.AppSettings.Get(key);
		}

		public static string ObtenerConnectionString()
		{
			StringBuilder builder = new StringBuilder();

			var server = LeerTexto(ServerKey);
			var db = LeerTexto(DbKey);
			var user = LeerTexto(UserKey);
			var password = LeerTexto(PasswordKey);

			builder.AppendFormat("Driver={{SQL Server}};Server={0};Database={1};UID={2};PWD={3};", server, db, user, password);

			return builder.ToString();
		}
	}
}
