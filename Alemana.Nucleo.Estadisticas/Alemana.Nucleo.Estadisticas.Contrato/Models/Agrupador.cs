////////////////////////////////////////////////////////////////////////////////////////////////////
// namespace: Alemana.Nucleo.Plantillas.Contrato.Models
//
// summary:	Clase que representa un agrupador.
////////////////////////////////////////////////////////////////////////////////////////////////////

using System.Collections.Generic;

namespace Alemana.Nucleo.Estadisticas.Contrato.Models
{
    public class Agrupador
    {
        private decimal codigo;

        private decimal idModulo;

        private string nombre;

        private decimal orden;

        private IEnumerable<Variable> variables;

        public decimal Codigo
        {
            get { return this.codigo; }
            set { this.codigo = value; }
        }

        public decimal IdModulo
        {
            get { return this.idModulo; }
            set { this.idModulo = value; }
        }

        public string Nombre
        {
            get { return this.nombre; }
            set { this.nombre = value; }
        }

        ////////////////////////////////////////////////////////////////////////////////////////////////////
        /// <summary>Gets or sets  codigo. </summary>
        ///
        /// <value> El codigo representa el Id del agrupador</value>
        ////////////////////////////////////////////////////////////////////////////////////////////////////
        ////////////////////////////////////////////////////////////////////////////////////////////////////
        /// <summary>Gets or sets El identificador modulo. </summary>
        ///
        /// <value> El identificador modulo asociado al agrupador </value>
        ////////////////////////////////////////////////////////////////////////////////////////////////////
        ////////////////////////////////////////////////////////////////////////////////////////////////////
        /// <summary>Gets or sets  nombre. </summary>
        ///
        /// <value> El nombre del agrupador </value>
        ////////////////////////////////////////////////////////////////////////////////////////////////////
        public decimal Orden
        {
            get { return this.orden; }
            set { this.orden = value; }
        }

        public IEnumerable<Variable> Variables
        {
            get { return variables; }
            set { variables = value; }
        }
    }
}