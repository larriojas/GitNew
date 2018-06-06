using System;

namespace Alemana.Nucleo.Common.ExceptionHandling
{
    /// <summary>
    /// Configuración de políticas para excepciones
    /// Autor: Lagash S.A.
    /// Fecha de creación: 29/04/2013
    /// Fecha de modificación: 29/04/2013
    /// </summary>
    class ExceptionPolicyConfiguration
    {
        #region properties
        /// <summary>
        /// Nombre de la política
        /// </summary>
        public string Name
        {
            get;
            set;
        }

        /// <summary>
        /// Acción a tomarse en la política
        /// </summary>
        public HandlingAction Action
        {
            get;
            set;
        }

        /// <summary>
        /// Tipo de excepción a manejar
        /// </summary>
        public Type ExceptionType
        {
            get;
            set;
        }

        /// <summary>
        /// Tipo de la nueva excepción
        /// </summary>
        public Type NewExceptionType
        {
            get;
            set;
        }

        /// <summary>
        /// Mensaje de la nueva excepción
        /// </summary>
        public string NewExceptionMessage
        {
            get;
            set;
        }
        #endregion
    }
}
