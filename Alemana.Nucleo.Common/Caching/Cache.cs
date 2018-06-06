using Alemana.Nucleo.Common.Caching.CacheManager;
using Alemana.Nucleo.Common.Exceptions;
using System;
using System.Collections.Generic;

namespace Alemana.Nucleo.Common.Caching
{
    /// <summary>
    /// Clase encargada de obtener los diferentes manejadores de caché posibles
    /// Autor: Lagash S.A.
    /// Fecha de creación: 29/04/2013
    /// Fecha de modificación: 29/04/2013
    /// </summary>
    public static class Cache
    {
        #region fields
        private static IDictionary<string, ICacheManager>  _instances = null;
        #endregion fields

        #region .ctor
        static Cache()
        {
            Initialize();
        }

        #endregion

        #region public methods

        /// <summary>
        /// Obtiene el manejador de cache por defecto.
        /// </summary>
        /// <returns>Manejador de cache por defecto</returns>
        public static ICacheManager Default
        {
            get
            {
                return GetCache(CacheConfigurationManager.GetDefaultPolicyName());
            }
        }

        public static IDictionary<string, ICacheManager> Instances
        {
            get
            {
                return _instances;
            }
        }


        /// <summary>
        /// Obtiene el manejador de cache según configuración
        /// </summary>
        /// <returns>Manejador de cache configurado</returns>
        public static ICacheManager GetCache(string policyName)
        {
            ICacheManager cacheManager = null;

            CachePolicyConfiguration policyConf = CacheConfigurationManager.GetPolicyConfiguration(policyName);

            if (Cache.Instances.TryGetValue(policyConf.Name, out cacheManager) == false)
                throw new CacheException(Messages.InexistentPolicyName,new ArgumentNullException());
            
            return cacheManager;

        }
        #endregion 

        #region private methods
        /// <summary>
        /// Inicializa Los Managers de las policies definidas en archivo de configuración
        /// </summary>
        private static void Initialize()
        {
            _instances = new Dictionary<string, ICacheManager>();
            
            //Inicializo la configuración de políticas
            CacheConfigurationManager.Initialize();

            //Agrego cada política de cache a la lista de políticas
            foreach(CachePolicyConfiguration cachePolicyConf in CacheConfigurationManager.GetPolicyConfigurationList())
            {

                switch (cachePolicyConf.CacheType)
                {
                    case CacheType.LocalCache:
                        _instances.Add(cachePolicyConf.Name, new LocalCacheManager(cachePolicyConf));
                        break;
                    case CacheType.FileCache:
                        _instances.Add(cachePolicyConf.Name, new FileCacheManager(cachePolicyConf));
                        break;
                    case CacheType.NoCache:
                        _instances.Add(cachePolicyConf.Name, new NoCacheManager());
                        break;
                    case CacheType.BinaryCache:
                        _instances.Add(cachePolicyConf.Name, new BinarySerializerCacheManager());
                        break;

                    default:
                        throw new CacheException(string.Format(
                            Messages.InvalidCacheType,cachePolicyConf.CacheType.ToString()));
                }
            }
        }
        
        #endregion 
    }
}
