using Alemana.Nucleo.Common.Exceptions;
using System;
using System.Configuration;

namespace Alemana.Nucleo.Common.Caching.Configuration
{
        /// <summary>
        /// Clase que representa la sección de configuración del módulo de Caching en el 
        /// archivo de configuración
        /// Autor: Lagash S.A.
        /// Fecha de creación: 29/04/2013
        /// Fecha de modificación: 29/04/2013
        /// </summary>
    public class CachingSection : ConfigurationSection
    {
        #region fields

        private const string policyCollectionProperty = "cachePolicies";
        private const string defaultPolicyProperty = "defaultPolicy";

        #endregion fields

        #region properties

        [ConfigurationProperty(defaultPolicyProperty, IsRequired = false)]
        public string DefaultPolicyName
        {
            get
            {
                return (string)this[defaultPolicyProperty];
            }
        }

        /// <summary>
        /// Coleción de políticas de manejo de cache que componen la sección
        /// </summary>
        [ConfigurationProperty(policyCollectionProperty)]
        public CachePolicyElementCollection PolicyCollection
        {
            get
            {
                return (CachePolicyElementCollection)this[policyCollectionProperty];
            }
        }

        #endregion properties
        
        #region methods

        /// <summary>
        /// Convierte el nombre de un tipo de cache a CacheType
        /// </summary>
        /// <param name="cacheTypeName">Nombre de tipo de cache</param>
        /// <returns>Instancia de CacheType de <paramref name="cacheTypeName"/></returns>
        public static CacheType ToCacheType(string cacheTypeName)
        {
            CacheType type;
            try
            {
                type = (CacheType)Enum.Parse(typeof(CacheType), cacheTypeName);
            }
            catch (ArgumentException ae)
            {
                throw new CacheException(string.Format(
                    Messages.InvalidCacheType, cacheTypeName), ae);
            }

            return type;
        }
        #endregion methods
    }

}
