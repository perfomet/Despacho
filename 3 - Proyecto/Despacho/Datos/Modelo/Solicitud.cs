using System.Data;

namespace Datos.Modelo
{
	public class Solicitud : Base.ModeloBase
	{
		public int SolicitudDespachoId { get; set; }
		public int TipoSolicitudId { get; set; }
		public int EstadoSolicitudId { get; set; }
		public string FechaSolicitud { get; set; }
		public string FechaRecepcion { get; set; }
		public string BodegaOrigen { get; set; }
		public string NumeroCliente { get; set; }
		public string NombreCliente { get; set; }
		public string DireccionCliente { get; set; }
		public int ComunaClienteId { get; set; }
		public string NumeroTelefonoContacto { get; set; }
		public string RutCliente { get; set; }
		public string VRutCliente { get; set; }
		public string Proyecto { get; set; }
		public int PrioridadId { get; set; }
		public int UnidadNegocioId { get; set; }
		public int GerenciaId { get; set; }
		public string ObservacionAof { get; set; }

		public string FechaDespacho { get; set; }
		public string PatenteCamion { get; set; }
		public bool LlamadaDiaAnterior { get; set; }
		public string ComentariosLlamada { get; set; }
		public int EnlaceId { get; set; }

		public int NumeroDocumento { get; set; }
		public int NumeroEntrega { get; set; }
		public string FechaEntregaDocumento { get; set; }
		public string FechaRecepcionDocumento { get; set; }
		public int Folio { get; set; }
		public int TipoDocumentoId { get; set; }

		public bool Concrecion { get; set; }
		public string NombreConcrecion { get; set; }
		public string RUTConcrecion { get; set; }
		public string VRUTConcrecion { get; set; }
		public string MotivoNoConcrecion { get; set; }



		public void FromDataRow(DataRow fila)
		{
			this.SolicitudDespachoId = int.Parse(fila[0].ToString());
			this.TipoSolicitudId = int.Parse(fila[1].ToString());
			this.EstadoSolicitudId = int.Parse(fila[2].ToString());
			this.FechaSolicitud = fila[3].ToString();
			this.FechaRecepcion = fila[4].ToString();
			this.BodegaOrigen = fila[5].ToString();
			this.NumeroCliente = fila[6].ToString();
			this.NombreCliente = fila[7].ToString();
			this.DireccionCliente = fila[8].ToString();
			this.ComunaClienteId = int.Parse(fila[9].ToString());
			this.NumeroTelefonoContacto = fila[10].ToString();
			this.RutCliente = fila[11].ToString();
			this.VRutCliente = fila[12].ToString();
			this.Proyecto = fila[13].ToString();
			this.PrioridadId = int.Parse(fila[14].ToString());
			this.UnidadNegocioId = int.Parse(fila[15].ToString());
			this.GerenciaId = int.Parse(fila[16].ToString());
			this.ObservacionAof = fila[17].ToString();

			this.FechaDespacho = fila[18].ToString();
			this.PatenteCamion = fila[19].ToString();
			this.LlamadaDiaAnterior = bool.Parse(fila[20].ToString());
			this.ComentariosLlamada = fila[21].ToString();
			this.EnlaceId = int.Parse(fila[22].ToString());

			this.NumeroDocumento = int.Parse(fila[23].ToString());
			this.NumeroEntrega = int.Parse(fila[24].ToString());
			this.FechaEntregaDocumento = fila[25].ToString();
			this.FechaRecepcionDocumento = fila[26].ToString();
			this.Folio = int.Parse(fila[27].ToString());
			this.TipoDocumentoId = int.Parse(fila[28].ToString());

			this.Concrecion = bool.Parse(fila[29].ToString());
			this.NombreConcrecion = fila[30].ToString();
			this.VRUTConcrecion = fila[31].ToString();
			this.MotivoNoConcrecion = fila[32].ToString();
		}
	}
}
