using System;
using System.Collections.Generic;

namespace Alemana.Nucleo.Estadisticas.Contrato.Models
{
    public class IncidenciaModulo
    {
        private decimal codigo;

        private decimal idPlantilla;

        private decimal idPaciente;

        private string nombre;

        private decimal orden;

        private DateTime fecha;

        private IEnumerable<IncidenciaVariable> variables;

        public decimal Codigo
        {
            get { return this.codigo; }
            set { this.codigo = value; }
        }

        public decimal IdPlantilla
        {
            get { return this.idPlantilla; }
            set { this.idPlantilla = value; }
        }

        public decimal IdPaciente
        {
            get { return this.idPaciente; }
            set { this.idPaciente = value; }
        }

        public string Nombre
        {
            get { return this.nombre; }
            set { this.nombre = value; }
        }

        public decimal Orden
        {
            get { return this.orden; }
            set { this.orden = value; }
        }

        public DateTime Fecha
        {
            get { return this.fecha; }
            set { this.fecha = value; }
        }

        public IEnumerable<IncidenciaVariable> Variables
        {
            get { return variables; }
            set { variables = value; }
        }
    }
}