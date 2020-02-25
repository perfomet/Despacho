using System.Collections.Generic;
using System.Data;

namespace Datos.Modelo
{
	public class Solicitud : Base.ModeloBase
	{
		public int SolicitudDespachoId { get; set; }
		public int NumeroSolicitud { get; set; }
		public int TipoSolicitudId { get; set; }
		public int EstadoSolicitudId { get; set; }
		public string FechaSolicitud { get; set; }
		public string FechaRecepcion { get; set; }
		public string NumeroCliente { get; set; }
		public string NombreCliente { get; set; }
		public string CalleDireccionCliente { get; set; }
		public int NumeroDireccionCliente { get; set; }
		public int RegionClienteId { get; set; }
		public int ComunaClienteId { get; set; }
		public string NumeroTelefonoContacto { get; set; }
		public string NumeroTelefonoContactoAdicional { get; set; }
		public string RutCliente { get; set; }
		public string VRutCliente { get; set; }
		public int PrioridadId { get; set; }
		public int UnidadNegocioId { get; set; }
		public int GerenciaId { get; set; }
		public string ObservacionAof { get; set; }

		public string FechaDespacho { get; set; }
		public string PatenteCamion { get; set; }
		public bool LlamadaDiaAnterior { get; set; }
		public string ComentariosLlamada { get; set; }

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

		public int SolicitanteId { get; set; }

		public List<EquipoSolicitado> EquiposSolicitados { get { return Datos.EquipoSolicitado.ObtenerEquiposSolicitados(SolicitudDespachoId); } }
		public List<EquipoRetirado> EquiposRetirados { get { return Datos.EquipoRetirado.ObtenerEquiposRetirados(SolicitudDespachoId); } }
		public List<PersonalAsignado> PersonalAsignado { get { return Datos.PersonalAsignado.ObtenerPersonalAsignado(SolicitudDespachoId); } }

		public TipoSolicitud TipoSolicitud { get { return Datos.TipoSolicitud.ObtenerTipoSolicitud(this.TipoSolicitudId); } }
		public EstadoSolicitud EstadoSolicitud { get { return Datos.EstadoSolicitud.ObtenerEstadoSolicitud(this.EstadoSolicitudId); } }
		public Prioridad Prioridad { get { return Datos.Prioridad.ObtenerPrioridad(this.PrioridadId); } }
		public Usuario Solicitante { get { return Datos.Usuario.ObtenerUsuario(this.SolicitanteId); } }
		public UnidadNegocio UnidadNegocio { get { return Datos.UnidadNegocio.ObtenerUnidadNegocio(this.UnidadNegocioId); } }

		public void FromDataRow(DataRow fila)
		{
			this.SolicitudDespachoId = int.Parse(fila[0].ToString());
			this.NumeroSolicitud = int.Parse(fila[1].ToString());
			this.TipoSolicitudId = int.Parse(fila[2].ToString());
			this.EstadoSolicitudId = int.Parse(fila[3].ToString());
			this.FechaSolicitud = fila[4].ToString();
			this.FechaRecepcion = fila[5].ToString();
			this.NumeroCliente = fila[6].ToString();
			this.NombreCliente = fila[7].ToString();
			this.CalleDireccionCliente = fila[8].ToString();
			this.NumeroDireccionCliente = int.Parse(fila[9].ToString());
			this.RegionClienteId = int.Parse(fila[10].ToString());
			this.ComunaClienteId = int.Parse(fila[11].ToString());
			this.NumeroTelefonoContacto = fila[12].ToString();
			this.NumeroTelefonoContactoAdicional = fila[13].ToString();
			this.RutCliente = fila[14].ToString();
			this.VRutCliente = fila[15].ToString();
			this.PrioridadId = int.Parse(fila[16].ToString());
			this.UnidadNegocioId = int.Parse(fila[17].ToString());
			this.GerenciaId = int.Parse(fila[18].ToString());
			this.ObservacionAof = fila[19].ToString();

			this.FechaDespacho = fila[20].ToString();
			this.PatenteCamion = fila[21].ToString();
			this.LlamadaDiaAnterior = fila[22].ToString().Equals(string.Empty) ? false : bool.Parse(fila[22].ToString());
			this.ComentariosLlamada = fila[23].ToString();

			this.NumeroDocumento = fila[24].ToString().Equals(string.Empty) ? 0 : int.Parse(fila[24].ToString());
			this.NumeroEntrega = fila[25].ToString().Equals(string.Empty) ? 0 : int.Parse(fila[25].ToString());
			this.FechaEntregaDocumento = fila[26].ToString();
			this.FechaRecepcionDocumento = fila[27].ToString();
			this.Folio = fila[28].ToString().Equals(string.Empty) ? 0 : int.Parse(fila[28].ToString());
			this.TipoDocumentoId = fila[29].ToString().Equals(string.Empty) ? 0 : int.Parse(fila[29].ToString());

			this.Concrecion = fila[30].ToString().Equals(string.Empty) ? false : bool.Parse(fila[30].ToString());
			this.NombreConcrecion = fila[31].ToString();
			this.RUTConcrecion = fila[32].ToString();
			this.VRUTConcrecion = fila[33].ToString();
			this.MotivoNoConcrecion = fila[34].ToString();

			this.SolicitanteId = int.Parse(fila[35].ToString());
		}
	}
}
