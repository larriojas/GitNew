namespace Alemana.Nucleo.Estadisticas.Contrato.Models
{
    public class Variable
    {
        private decimal codigo;

        private string nombre;

        private string nombreExportar;

        private decimal idAgrupador;

        private decimal orden;

        public decimal Orden
        {
            get { return this.orden; }
            set { this.orden = value; }
        }

        public string NombreExportar
        {
            get { return this.nombreExportar; }
            set { this.nombreExportar = value; }
        }

        public decimal Codigo
        {
            get { return this.codigo; }
            set { this.codigo = value; }
        }

        public decimal IdAgrupador
        {
            get { return this.idAgrupador; }
            set { this.idAgrupador = value; }
        }

        public string Nombre
        {
            get { return this.nombre; }
            set { this.nombre = value; }
        }
    }
}