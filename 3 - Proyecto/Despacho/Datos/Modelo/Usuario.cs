using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Datos.Modelo
{
    public class Usuario
    {
        public int UsuarioId { get; set; }
        public string Username { get; set; }
        public string Password { get; set; }
        public string Nombres { get; set; }
        public string ApellidoPaterno { get; set; }
        public string ApellidoMaterno { get; set; }
        public string Email { get; set; }
        public int PerfilId { get; set; }
        public int? ClienteId { get; set; }

        public Perfil Perfil { 
            get {
                return new Perfil();
            } 
            set { 
                Perfil = value; 
            } 
        }

        public Cliente Cliente
        {
            get
            {
                return new Cliente();
            }
            set
            {
                Cliente = value;
            }
        }

        public void FromDataRow(DataRow fila)
        {
            this.UsuarioId = int.Parse(fila[0].ToString());
            this.Username = fila[1].ToString();
            this.Password = fila[2].ToString();
            this.Nombres = fila[3].ToString();
            this.ApellidoPaterno = fila[4].ToString();
            this.ApellidoMaterno = fila[5].ToString();
            this.Email = fila[6].ToString();
            this.PerfilId = int.Parse(fila[7].ToString());
            if(fila[8] != null && !fila[8].ToString().Equals("")) this.ClienteId = int.Parse(fila[8].ToString());
        }
    }
}
