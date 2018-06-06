namespace Alemana.Nucleo.Estadisticas.Contrato.Models
{
    public class IndicadorPaciente
    {
        private decimal atendidos;

        private decimal etiquetados;

        public decimal Atendidos
        {
            get { return atendidos; }
            set { atendidos = value; }
        }

        public decimal Etiquetados
        {
            get { return etiquetados; }
            set { etiquetados = value; }
        }
    }
}