using System;
using System.Collections.Generic;
using System.Data;

namespace Datos.Modelo
{
	public class CargaMasiva : Base.ModeloBase
	{
		public int CargaMasivaId { get; set; }
		public int UsuarioId { get; set; }
		public string FechaHora { get; set; }
		public string Archivo { get; set; }

		public Usuario Usuario
		{
			get { return UsuarioId > 0 ? Datos.Usuario.ObtenerUsuario(UsuarioId) : null; }
		}


		public List<CargaMasivaDetalle> Detalle
		{
			get { return Datos.CargaMasivaDetalle.ObtenerCargasMasivasDetalle(); }
		}

		public void FromDataRow(DataRow fila)
		{
			this.CargaMasivaId = int.Parse(fila[0].ToString());
			this.UsuarioId = int.Parse(fila[1].ToString());
			this.FechaHora = fila[2].ToString();
			this.Archivo = fila[3].ToString();
		}
	}
}
