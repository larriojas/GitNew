using Alemana.Nucleo.Common.Caching.Configuration;
using Alemana.Nucleo.Common.Exceptions;
using System.Collections.Generic;
using System.Configuration;
using System.Diagnostics;

namespace Alemana.Nucleo.Common.Caching
{
    /// <summary>
    /// Manejador de la configuración del cache. Se asegura que se obtenga la sección de
    /// configuración de cache una sola vez.
    /// Autor: Lagash S.A.
    /// Fecha de creación: 29/04/2013
    /// Fecha de modificación: 29/04/2013
    /// </summary>
    static class CacheConfigurationManager
    {
        #region Static fields
        private static string _defaultPolicyName = string.Empty;
        private static readonly string _cachingSectionName = "cachingSection";
        private static List<CachePolicyConfiguration> _policyConfigurationList;
        private static TraceSource _traceSource;
        
        /// <summary>
        /// Seccion de caching del archivo de configuración
        /// </summary>
        private static CachingSection section = null;

        #endregion fields

        #region ctor and finalizers

        /// <summary>
        /// Constructor
        /// </summary>
        static CacheConfigurationManager()
        {
        }

        #endregion ctor and finalizers

        #region Public methods

        /// <summary>
        /// Carga los datos de propiedades de cache del archivo de configuración 
        /// <see cref="PerformanceCounterContainer"/>
        /// </summary>
        internal static void Initialize()
        {
            _policyConfigurationList = new List<CachePolicyConfiguration>();
            
            try
            {
                section = ConfigurationManager.GetSection(_cachingSectionName) as CachingSection;
            }
            catch (ConfigurationErrorsException ceex)
            {
                throw new CacheException(Messages.CacheLoadError, ceex);
            }

            if (section == null)
                throw new CacheException(Messages.CacheLoadError);
            else
                LoadConfigurationFromConfigurationSection(section);

            _traceSource = new TraceSource("AlemanaCachingSource", SourceLevels.All);
        }

        /// <summary>
        /// Obtiene el <see cref="PolicyConfiguration"/> de la lista
        /// </summary>
        /// <param name="policyName">Nombre de la política a obtener</param>
        /// <returns>Política obtenida</returns>
        internal static CachePolicyConfiguration GetPolicyConfiguration(string policyName)
        {
            //Si el nombre de la política es vacio 
            if (policyName == string.Empty)
                throw new CacheException(Messages.EmptyPolicyName);

            CachePolicyConfiguration policyConf = _policyConfigurationList.Find(
                cp => cp.Name == policyName);

            //Si no encuentro la política
            if (policyConf == null)
                throw new CacheException(Messages.InexistentPolicyName + policyName);

            return policyConf;
        }

        /// <summary>
        /// Obtiene el nombre de la política por default
        /// </summary>
        /// <returns>Nombre de la Política por default </returns>
        internal static string GetDefaultPolicyName()
        {
            //Obtengo el nombre de la política default del archivo de configuración
            return section.DefaultPolicyName;

        }

        /// <summary>
        /// Retorna la lista de políticas de cache
        /// </summary>
        internal static List<CachePolicyConfiguration> GetPolicyConfigurationList()
        {
            return _policyConfigurationList;
        }
        #endregion

        #region Private methods

        /// <summary>
        /// Carga la configuración a partir de <paramref name="section"/>
        /// </summary>
        /// <param name="section">Sección de configuración del archivo de configuración</param>
        private static void LoadConfigurationFromConfigurationSection(CachingSection section)
        {
            foreach (CachePolicyElement policyElement in section.PolicyCollection)
            {
                CachePolicyConfiguration policyConf = new CachePolicyConfiguration()
                {
                    Name = policyElement.Name,
                    CacheType = policyElement.CacheType,
                    DefaultLifeTime = policyElement.DefaultLifeTime,
                    CacheMemoryLimitMegabytes = policyElement.CacheMemoryLimitMegabytes,
                    PhysicalMemoryLimitPercentage = policyElement.PhysicalMemoryLimitPercentage,
                    PollingInterval = policyElement.PollingInterval
                };
                CacheConfigurationManager._policyConfigurationList.Add(policyConf);
            }

        }
        #endregion
    }
}
