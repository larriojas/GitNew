using System.Configuration;

namespace Alemana.Nucleo.Common.Caching.Configuration
{
    /// <summary>
    /// Representa una política en el archivo de configuración.
    /// Autor: Lagash S.A.
    /// Fecha de creación: 29/04/2013
    /// Fecha de modificación: 29/04/2013
    /// </summary>
    public class CachePolicyElement : ConfigurationElement
    {
        #region fields

        private const string nameProperty = "name";
        private const string cacheTypeProperty = "cacheType";
        private const string defaultLifetimeProperty = "defaultLifetime";

        //Propiedades utilizadas en LocalCacheManager
        private const string cacheMemoryLimitMegabytesProperty = "cacheMemoryLimitMegabytes";
        private const string physicalMemoryLimitPercentageProperty = "physicalMemoryLimitPercentage";
        private const string pollingIntervalProperty = "pollingInterval";

        private CacheType? cacheType = null;
                
        #endregion fields

        #region ctor and finalizers

        /// <summary>
        /// Constructor del elemento
        /// </summary>
        public CachePolicyElement()
        {

        }

        /// <summary>
        /// Constructor del elemento
        /// </summary>
        /// <param name="name">Nombre del elemento</param>
        public CachePolicyElement(string name)
        {
            Name = name;
        } 

        #endregion ctor and finalizers

        #region properties

        /// <summary>
        /// Propiedad Name
        /// </summary>
        [ConfigurationProperty(nameProperty, IsKey = true, IsRequired=true)]
        public string Name
        {
            get
            {
                return (string)this[nameProperty];
            }
            set
            {
                this[nameProperty] = value;
            }
        }

        /// <summary>
        /// Nombre del tipo de cache obtenido de configuración
        /// </summary>
        [ConfigurationProperty(cacheTypeProperty, IsRequired = true)]
        public string CacheTypeName
        {
            get
            {
                return (string)this[cacheTypeProperty];
            }
        }

        /// <summary>
        /// Tipo de cache obtenido de configuración
        /// </summary>
        public CacheType CacheType
        {
            get
            {
                if (this.cacheType == null)
                    this.cacheType = CachingSection.ToCacheType(this.CacheTypeName);

                return this.cacheType.Value;
            }
        }

        /// <summary>
        /// Tiempo de vida por defecto de los objetos del cache
        /// </summary>
        [ConfigurationProperty(defaultLifetimeProperty)]
        public int DefaultLifeTime
        {
            get
            {
                return (int)this[defaultLifetimeProperty];
            }
            set
            {
                this[defaultLifetimeProperty] = value;
            }
        }
        /// <summary>
        /// Límite de espacio de almacenamiento
        /// </summary>
        [ConfigurationProperty(cacheMemoryLimitMegabytesProperty , DefaultValue="0" )]
        public int CacheMemoryLimitMegabytes
        {
            get
            {
                return (int)this[cacheMemoryLimitMegabytesProperty];
            }
            set
            {
                this[cacheMemoryLimitMegabytesProperty] = value;
            }
        }

        /// <summary>
        /// Porcentaje máximo de almacenamiento en el servidor
        /// </summary>
        [ConfigurationProperty(physicalMemoryLimitPercentageProperty , DefaultValue="0")]
        public int PhysicalMemoryLimitPercentage
        {
            get
            {
                return (int)this[physicalMemoryLimitPercentageProperty];
            }
            set
            {
                this[physicalMemoryLimitPercentageProperty] = value;
            }
        }

        /// <summary>
        /// Intervalo de verificación de los límites establecidos
        /// </summary>
        [ConfigurationProperty(pollingIntervalProperty, DefaultValue = "00:02:00")]
        public System.TimeSpan PollingInterval
        {
            get
            {
                return (System.TimeSpan)this[pollingIntervalProperty];
            }
            set
            {
                this[pollingIntervalProperty] = value;
            }
        }

        #endregion properties
    }
}

