
namespace Alemana.Nucleo.Common.Caching
{
    /// <summary>
    /// Configuración de políticas para cache
    /// Autor: Lagash S.A.
    /// Fecha de creación: 29/04/2013
    /// Fecha de modificación: 29/04/2013
    /// </summary>
    public class CachePolicyConfiguration
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
        /// Tipo de Cache a manejar
        /// </summary>
        public CacheType CacheType
        {
            get;
            set;
        }

        /// <summary>
        /// Lifetime de objetos en cache en segundos (solo para LocalCache)
        /// </summary>
        public int DefaultLifeTime
        {
            get;
            set;
        }

        /// <summary>
        /// Límite de memoria del caché en el servidor (solo para LocalCache)
        /// </summary>
        public int CacheMemoryLimitMegabytes
        {
            get;
            set;
        }

        /// <summary>
        /// Porcentaje máximo de memoria física destinada al caché en el servidor (solo para LocalCache)
        /// </summary>
        public int PhysicalMemoryLimitPercentage
        {
            get;
            set;
        }
        
        /// <summary>
        /// Intervalo de verificación de estadísticas de almacenamiento del caché (solo para LocalCache)
        /// </summary>
        public System.TimeSpan PollingInterval
        {
            get;
            set;
        }


        #endregion
    }
}
