////////////////////////////////////////////////////////////////////////////////////////////////////
// file:	Models\Plantilla.cs
//
// summary:	Implements  plantilla class
////////////////////////////////////////////////////////////////////////////////////////////////////
using System.Collections.Generic;

namespace Alemana.Nucleo.Estadisticas.Contrato.Models
{
    ////////////////////////////////////////////////////////////////////////////////////////////////////
    /// <summary>   A plantilla. </summary>
    ///
    /// <remarks>   Gustavo Osvaldo, 5/26/2014. </remarks>
    ////////////////////////////////////////////////////////////////////////////////////////////////////

    public class PlantillaEstadistica
    {
        /// <summary>    codigo. </summary>
        private decimal codigo;

        /// <summary>    descripcion. </summary>
        private string descripcion;

        private IEnumerable<Modulo> modulos;

        public decimal Codigo
        {
            get { return codigo; }
            set { codigo = value; }
        }

        public string Descripcion
        {
            get { return descripcion; }
            set { descripcion = value; }
        }

        public IEnumerable<Modulo> Modulos
        {
            get { return modulos; }
            set { modulos = value; }
        }

        ////////////////////////////////////////////////////////////////////////////////////////////////////
        /// <summary>   Gets or sets  descripcion. </summary>
        ///
        /// <value>  descripcion. </value>
        ////////////////////////////////////////////////////////////////////////////////////////////////////
    }
}