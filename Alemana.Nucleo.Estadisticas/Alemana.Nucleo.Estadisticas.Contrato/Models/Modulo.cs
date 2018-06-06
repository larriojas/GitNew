﻿////////////////////////////////////////////////////////////////////////////////////////////////////
// file:	Models\Modulo.cs
//
// summary:	Implements la clase modulo
////////////////////////////////////////////////////////////////////////////////////////////////////

using System.Collections.Generic;

namespace Alemana.Nucleo.Estadisticas.Contrato.Models
{
    ////////////////////////////////////////////////////////////////////////////////////////////////////
    /// <summary>Representa un modulo. </summary>
    ///
    /// <remarks>   Gustavo Osvaldo, 5/26/2014. </remarks>
    ////////////////////////////////////////////////////////////////////////////////////////////////////

    public class Modulo
    {
        private IEnumerable<Agrupador> agrupadores;

        /// <summary>   El codigo representa el id del modulo </summary>
        private decimal codigo;

        /// <summary>   El identificador de la plantilla. </summary>
        private decimal idPlantilla;

        /// <summary>   El nombre. </summary>
        private string nombre;

        /// <summary>   El orden. </summary>
        private decimal orden;

        public IEnumerable<Agrupador> Agrupadores
        {
            get { return agrupadores; }
            set { agrupadores = value; }
        }

        ////////////////////////////////////////////////////////////////////////////////////////////////////
        /// <summary>Gets or sets  codigo. </summary>
        ///
        /// <value> El codigo representa el id del modulo </value>
        ////////////////////////////////////////////////////////////////////////////////////////////////////

        public decimal Codigo
        {
            get { return this.codigo; }
            set { this.codigo = value; }
        }

        ////////////////////////////////////////////////////////////////////////////////////////////////////
        /// <summary>Gets or sets El identificador plantilla. </summary>
        ///
        /// <value> El identificador de la plantilla. </value>
        ////////////////////////////////////////////////////////////////////////////////////////////////////

        public decimal IdPlantilla
        {
            get { return this.idPlantilla; }
            set { this.idPlantilla = value; }
        }

        ////////////////////////////////////////////////////////////////////////////////////////////////////
        /// <summary>Gets or sets  nombre. </summary>
        ///
        /// <value> El nombre. </value>
        ////////////////////////////////////////////////////////////////////////////////////////////////////

        public string Nombre
        {
            get { return this.nombre; }
            set { this.nombre = value; }
        }

        ////////////////////////////////////////////////////////////////////////////////////////////////////
        /// <summary>Gets or sets  no vigentes. </summary>
        ///
        /// <value> Los agrupadores no vigentes </value>
        ////////////////////////////////////////////////////////////////////////////////////////////////////

        public decimal Orden
        {
            get { return this.orden; }
            set { this.orden = value; }
        }

        ////////////////////////////////////////////////////////////////////////////////////////////////////
        /// <summary>Gets or sets  vigentes. </summary>
        ///
        /// <value> Los agrupadores vigentes </value>
        ////////////////////////////////////////////////////////////////////////////////////////////////////
    }
}