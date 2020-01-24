using System;
using System.Collections.Generic;
using System.Data;
using Datos.DB;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Datos.Datos
{
	public class Camion
	{
    public static List<Modelo.Camion> ObtenerCamiones()
    {
      DataTable dataTable = DataBase.ExecuteReader("SELECT * FROM Camion");
      List<Modelo.Camion> camiones = new List<Modelo.Camion>();

      foreach (DataRow fila in dataTable.Rows)
      {
        Modelo.Camion camion = new Modelo.Camion();
        camion.FromDataRow(fila);
        camiones.Add(camion);
      }

      return camiones;
    }

    public static Modelo.Camion ObtenerCamion(string Patente)
    {
      Modelo.Camion camion = new Modelo.Camion();

      DataTable dataTable = DataBase.ExecuteReader("SELECT * FROM Camion WHERE Patente LIKE '" + Patente +"'");

      if (dataTable.Rows.Count > 0)
      {
        DataRow fila = dataTable.Rows[0];
        camion.FromDataRow(fila);
      }

      return camion;
    }
    public static bool Crear(Modelo.Camion camion)
    {
      StringBuilder builder = new StringBuilder();
      
      builder.AppendFormat("INSERT INTO Camion VALUES ('{0}', '{1}', '{2}')", camion.Patente, camion.Descripcion, camion.EmpresaTransporteId);

      return DataBase.ExecuteNonQuery(builder.ToString()) > 0;
    }

    public static bool Modificar(Modelo.Camion camion)
    {
      StringBuilder builder = new StringBuilder();
      
      builder.AppendFormat("UPDATE Cliente SET Patente = '{0}', Descripcion = '{1}',  EmpresaTransporteId = {2} WHERE Patente LIKE '" + camion.Patente + "'", camion.Patente, camion.Descripcion, camion.EmpresaTransporteId);

      return DataBase.ExecuteNonQuery(builder.ToString()) > 0;
    }

    public static bool Eliminar(string patente)
    {
      StringBuilder builder = new StringBuilder();
      builder.AppendFormat("DELETE FROM Camion WHERE Patente LIKE '" + patente + "'");

      return DataBase.ExecuteNonQuery(builder.ToString()) > 0;
    }
  }
}

