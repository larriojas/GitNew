using System.Configuration;

namespace Alemana.Nucleo.Common.ExceptionHandling.Configuration
{
    /// <summary>
    /// Clase que representa la sección de configuración de manejo de excepciones.
    /// Autor: Lagash S.A.
    /// Fecha de creación: 29/04/2013
    /// Fecha de modificación: 29/04/2013
    /// </summary>
    public class ExceptionHandlingSection : ConfigurationSection
    {
        #region fields

        private const string policyCollectionProperty = "exceptionPolicies";

        #endregion fields

        #region properties

        /// <summary>
        /// Coleción de políticas de manejo de excepción que componen la sección
        /// </summary>
        [ConfigurationProperty(policyCollectionProperty)]
        public ExceptionPolicyElementCollection PolicyCollection
        {
            get
            {
                return (ExceptionPolicyElementCollection)this[policyCollectionProperty];
            }
        }

        #endregion properties
    }
}
