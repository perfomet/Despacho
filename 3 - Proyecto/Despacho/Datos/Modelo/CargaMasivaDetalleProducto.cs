﻿using System.Data;

namespace Datos.Modelo
{
	public class CargaMasivaDetalleProducto : Base.ModeloBase
	{
		public int CargaMasivaDetalleId { get; set; }
		public string NumeroPlaca { get; set; }

		public CargaMasivaDetalle CargaMasivaDetalle
		{
			get { return new CargaMasivaDetalle(); }
		}

		public void FromDataRow(DataRow fila)
		{
			this.CargaMasivaDetalleId = int.Parse(fila[0].ToString());
			this.NumeroPlaca = fila[1].ToString();
		}
	}
}
