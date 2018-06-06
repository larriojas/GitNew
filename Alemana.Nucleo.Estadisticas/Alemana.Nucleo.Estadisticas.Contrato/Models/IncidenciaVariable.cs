namespace Alemana.Nucleo.Estadisticas.Contrato.Models
{
    public class IncidenciaVariable
    {
        private decimal codigo;

        private string nombre;

        private string nombreExportar;

        private decimal idIncidenciaModulo;

        private decimal orden;

        private string valor;

        public decimal Codigo
        {
            get { return this.codigo; }
            set { this.codigo = value; }
        }

        public string Nombre
        {
            get { return this.nombre; }
            set { this.nombre = value; }
        }

        public string NombreExportar
        {
            get { return this.nombreExportar; }
            set { this.nombreExportar = value; }
        }

        public decimal IdIncidenciaModulo
        {
            get { return this.idIncidenciaModulo; }
            set { this.idIncidenciaModulo = value; }
        }

        public decimal Orden
        {
            get { return this.orden; }
            set { this.orden = value; }
        }

        public string Valor
        {
            get { return this.valor; }
            set { this.valor = value; }
        }
    }
}