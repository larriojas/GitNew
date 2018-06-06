namespace Alemana.Nucleo.Estadisticas.Contrato.Models
{
    public class Area
    {
        private decimal id;

        private string nombre;

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
    }
}