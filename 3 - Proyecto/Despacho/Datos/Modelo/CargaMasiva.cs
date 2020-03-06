using System;
using System.Collections.Generic;
using System.Data;

namespace Datos.Modelo
{
	public class CargaMasiva : Base.ModeloBase
	{
		public int CargaMasivaId { get; set; }
		public int UsuarioId { get; set; }
		public DateTime FechaHora { get; set; }
		public string Archivo { get; set; }
		public Usuario Usuario
		{
			get { return Datos.Usuario.ObtenerUsuario(UsuarioId); }
		}
		
		
		public List<CargaMasivaDetalle> Detalle
		{
			get { return new List<CargaMasivaDetalle>(); }
		}

		public void FromDataRow(DataRow fila)
		{
			this.CargaMasivaId = int.Parse(fila[0].ToString());
			this.UsuarioId = int.Parse(fila[1].ToString());
			this.FechaHora = DateTime.Parse(fila[2].ToString());
			this.Archivo = fila[3].ToString();
		}
	}
}
