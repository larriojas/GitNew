using System.ComponentModel;

namespace Alemana.Nucleo.Estadisticas.Contrato.Models
{
    public class IndicadorCanalesVirtuales
    {
        private decimal cRM;
        private decimal cRMRespuesta;
        private decimal portalPacientes;
        private decimal portalPacientesRespuesta;
        private decimal rCE;
        private decimal rCERespuesta;
        private decimal totalMensajes;
        private decimal totalMensajesRespuesta;

        public decimal CRM
        {
            get { return cRM; }
            set { cRM = value; }
        }

        public decimal CRMRespuesta
        {
            get { return cRMRespuesta; }
            set { cRMRespuesta = value; }
        }

        public decimal PortalPacientes
        {
            get { return portalPacientes; }
            set { portalPacientes = value; }
        }

        public decimal PortalPacientesRespuesta
        {
            get { return portalPacientesRespuesta; }
            set { portalPacientesRespuesta = value; }
        }

        public decimal RCE
        {
            get { return rCE; }
            set { rCE = value; }
        }

        public decimal RCERespuesta
        {
            get { return rCERespuesta; }
            set { rCERespuesta = value; }
        }

        public decimal TotalMensajes
        {
            get { return totalMensajes; }
            set { totalMensajes = value; }
        }

        public decimal TotalMensajesRespuesta
        {
            get { return totalMensajesRespuesta; }
            set { totalMensajesRespuesta = value; }
        }
    }

    public enum EstadoLectura
    {
        [Description("Leído")]
        Leido,

        [Description("No Leído")]
        NoLeido
    }

    public enum EstadoMensaje
    {
        [Description("Abierto")]
        ABIERTO = 0,

        [Description("Cerrado")]
        CERRADO = 1
    }

    public enum Fuente
    {
        [Description("Canales Virtuales")]
        CanalesVirtuales = 0,

        [Description("CRM")]
        CRM = 1,

        [Description("Portal Salud")]
        PortalSalud = 2,

        [Description("RCE")]
        RCE = 3
    }
}