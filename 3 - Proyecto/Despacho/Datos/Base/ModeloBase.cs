using System.Data;

namespace Datos.Base
{
	public interface ModeloBase
	{
		void FromDataRow(DataRow fila);
	}
}
