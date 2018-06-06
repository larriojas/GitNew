using System.Collections.Generic;

namespace Alemana.Nucleo.Common.ExceptionHandling
{
    /// <summary>
    /// Configuración de política
    /// Autor: Lagash S.A.
    /// Fecha de creación: 29/04/2013
    /// Fecha de modificación: 29/04/2013
    /// </summary>
    class PolicyConfiguration
    {
        #region properties
        /// <summary>
        /// Si genera Log
        /// </summary>
        public bool Log
        {
            get;
            set;
        }
        /// <summary>
        /// Nombre de la política
        /// </summary>
        public string Name
        {
            get;
            set;
        }

        /// <summary>
        /// Lista de políticas de excepciones
        /// </summary>
        public List<ExceptionPolicyConfiguration> ExceptionPolicyList
        {
            get;
            set;
        }
        #endregion
    }
}
