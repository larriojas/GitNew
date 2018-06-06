using System.Collections.Generic;

namespace Alemana.Nucleo.Common.Caching
{
    /// <summary>
    /// Configuración de colección de políticas
    /// Autor: Lagash S.A.
    /// Fecha de creación: 29/04/2013
    /// Fecha de modificación: 29/04/2013
    /// </summary>
    class PolicyConfiguration
    {
        #region properties
        /// <summary>
        /// Nombre de la política por default
        /// </summary>
        public string DefaultPolicy
        {
            get;
            set;
        }

        /// <summary>
        /// Lista de políticas de caching
        /// </summary>
        public List<CachePolicyConfiguration> CachePolicyList
        {
            get;
            set;
        }
        #endregion
    }
}
