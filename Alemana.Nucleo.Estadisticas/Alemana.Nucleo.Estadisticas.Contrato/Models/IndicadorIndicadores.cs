namespace Alemana.Nucleo.Estadisticas.Contrato.Models
{
    public class IndicadorIndicadores
    {
        private decimal farmacologia;

        private decimal imagenologia;

        private decimal laboratorio;

        public decimal Farmacologia
        {
            get { return farmacologia; }
            set { farmacologia = value; }
        }

        public decimal Imagenologia
        {
            get { return imagenologia; }
            set { imagenologia = value; }
        }

        public decimal Laboratorio
        {
            get { return laboratorio; }
            set { laboratorio = value; }
        }
    }
}