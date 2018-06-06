namespace Alemana.Nucleo.Estadisticas.Contrato.Models
{
    public class IndicadorDiagnostico
    {
        private decimal id;

        private string nombre;

        private decimal numero;

        public decimal Id
        {
            get { return id; }
            set { id = value; }
        }

        public string Nombre
        {
            get { return nombre; }
            set { nombre = value; }
        }

        public decimal Numero
        {
            get { return numero; }
            set { numero = value; }
        }
    }
}