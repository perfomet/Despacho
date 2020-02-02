using System.Data;

namespace Datos.Modelo
{
	public class Usuario : Base.ModeloBase
	{
		public int UsuarioId { get; set; }
		public string Username { get; set; }
		public string Password { get; set; }
		public string Nombres { get; set; }
		public string ApellidoPaterno { get; set; }
		public string ApellidoMaterno { get; set; }
		public string NombreCompleto { get { return Nombres + " " + ApellidoPaterno + (ApellidoMaterno == null ? "" : " " + ApellidoMaterno); } }
		public string Email { get; set; }
		public int PerfilId { get; set; }
		public int? ClienteId { get; set; }
		public bool EstaActivo { get; set; }
		public Perfil Perfil
		{
			get { return Datos.Perfil.ObtenerPerfil(PerfilId); }
			set { Perfil = value; }
		}

		public Cliente Cliente
		{
			get { return Datos.Cliente.ObtenerCliente(ClienteId.GetValueOrDefault()); }
			set { Cliente = value; }
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
			if (fila[8] != null && !fila[8].ToString().Equals("")) this.ClienteId = int.Parse(fila[8].ToString());
			this.EstaActivo = bool.Parse(fila[9].ToString());
		}
	}
}
