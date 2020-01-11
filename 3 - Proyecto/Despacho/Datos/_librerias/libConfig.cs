using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Datos._librerias
{
    class libConfig
    {
        const string SERVER_KEY = "Server";
        const string DB_KEY = "DB";
        const string USER_KEY = "User";
        const string PASSWORD_KEY = "Password";

        static string LeerTexto(string key)
        {
            return ConfigurationSettings.AppSettings.Get(key);
        }

        public static string ObtenerConnectionString()
        {
            StringBuilder builder = new StringBuilder();

            string Server = LeerTexto(SERVER_KEY);
            string DB = LeerTexto(DB_KEY);
            string User = LeerTexto(USER_KEY);
            string Password = LeerTexto(PASSWORD_KEY);

            builder.AppendFormat("Driver={{SQL Server}};Server={0};Database={1};UID={2};PWD={3};", Server, DB, User, Password);

            return builder.ToString();
        }
    }
}
