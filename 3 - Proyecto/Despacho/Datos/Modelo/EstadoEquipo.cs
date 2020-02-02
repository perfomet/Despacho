﻿using System.Data;

namespace Datos.Modelo
{
	public class EstadoEquipo : Base.ModeloBase
	{
		public int Estadoequipoid { get; set; }
		public string Descripcion { get; set; }
		public bool EstaActivo { get; set; }
		public void FromDataRow(DataRow fila)
		{
			this.Estadoequipoid = int.Parse(fila[0].ToString());
			this.Descripcion = fila[1].ToString();
			this.EstaActivo = bool.Parse(fila[2].ToString());
		}
	}
}
