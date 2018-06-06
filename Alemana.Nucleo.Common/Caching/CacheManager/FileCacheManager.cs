using Alemana.Nucleo.Common.Exceptions;
using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Runtime.Caching;

namespace Alemana.Nucleo.Common.Caching.CacheManager
{
    /// <summary>
    /// Manejador de cache que utiliza <see cref="System.Runtime.Caching.ObjectCache"/>
    /// Autor: Lagash S.A.
    /// Fecha de creación: 29/04/2013
    /// Fecha de modificación: 29/04/2013
    /// </summary>
    public class FileCacheManager : ICacheManager
    {
        #region fields
        private NameValueCollection propertiesMemoryCache;
        private string _name;
        private FileCache _innerCache;
        private int _defaultLifetime;

        #endregion fields

        #region ctor and finalizers

        public FileCacheManager()
        {
            _defaultLifetime = 120;
            _innerCache = new FileCache(new ObjectBinder());
        }

        public FileCacheManager(CachePolicyConfiguration policyConf)
        {
            propertiesMemoryCache = new NameValueCollection();

            _name = policyConf.Name;
            _defaultLifetime = policyConf.DefaultLifeTime;

            propertiesMemoryCache.Add("CacheMemoryLimitMegabytes", policyConf.CacheMemoryLimitMegabytes.ToString());
            propertiesMemoryCache.Add("PhysicalMemoryLimitPercentage", policyConf.PhysicalMemoryLimitPercentage.ToString());
            propertiesMemoryCache.Add("PollingInterval", policyConf.PollingInterval.ToString());

            _innerCache = new FileCache(new ObjectBinder());
        }

        #endregion ctor and finalizers

        #region properties

        /// <summary>
        /// Obtiene el cache
        /// </summary>
        private ObjectCache LocalCache
        {
            get
            {
                return _innerCache;
            }
        }

        #endregion properties

        #region ICacheManager Members

        /// <summary>
        /// Inserta un nuevo elemento al cache con el tiempo de vida por defecto si se
        /// encuentra configurado, y si tiempo de vida máximo en caso contrario.
        /// </summary>
        /// <param name="key">Clave del elemento</param>
        /// <param name="value">Elemento a insertar</param>
        public void AddItem(string key, object value)
        {
            if (value == null)
            {
                this.LocalCache.Add(key, "null", DateTime.UtcNow.AddSeconds(_defaultLifetime));
                return;
            }
            try
            {
                this.LocalCache.Add(key, value, DateTime.UtcNow.AddSeconds(_defaultLifetime));
            }
            catch (Exception e) {
                Console.WriteLine(e.ToString());
            }
        }

        /// <summary>
        /// Inserta un nuevo elemento al cache
        /// </summary>
        /// <param name="key">Clave del elemento</param>
        /// <param name="value">Elemento a insertar</param>
        /// <param name="lifetime">Duración de vida del objeto en milisegundos</param>
        public void AddItem(string key, object value, int lifetime)
        {
            if (value == null)
            {
                this.LocalCache.Add(key, "null", DateTime.UtcNow.AddSeconds(lifetime));
                return;
            }

            this.LocalCache.Add(key, value, DateTime.UtcNow.AddSeconds(lifetime));
        }

        static readonly object _object = new object();

        /// <summary>
        /// Verifica si el cache tiene un item con clave <paramref name="key"/>
        /// </summary>
        /// <param name="key">Clave del item a buscar</param>
        /// <returns>Retorna true si el item se encuentra en el cache</returns>
        public bool HasItem(string key)
        {
            lock(_object){
                return (this.LocalCache[key] != null);
            }
        }

        /// <summary>
        /// Obtiene un item del cache
        /// </summary>
        /// <param name="key">Clave del item</param>
        /// <returns>Item obtenido</returns>
        public object GetItem(string key)
        {
            var value = this.LocalCache[key];

            if (value.ToString() == "null")
                return null;

            return value;
        }

        /// <summary>
        /// Quita un item del cache
        /// </summary>
        /// <param name="key">Clave del item a quitar</param>
        public void RemoveItem(string key)
        {
            this.LocalCache.Remove(key);
        }

        /// <summary>
        /// Renueva el tiempo de vida del item especificado
        /// </summary>
        /// <param name="key">Clave del item</param>
        /// <param name="lifetime">Nuevo tiempo de vida del item en segundos</param>
        /// <returns>Retorna true si se encontro el item en el cache y se pudo completar
        /// la operación correctamente</returns>
        public bool RenewItemLifetime(string key, int lifetime)
        {
            object item = LocalCache[key];
            if (item != null)
            {
                RemoveItem(key);
                AddItem(key, item, lifetime);
            }
            return (item != null);
        }

        /// <summary>
        /// Obtiene una lista con todos los items del cache
        /// </summary>
        /// <returns>Lista con todos los items del cache</returns>
        public List<string> ListKeys()
        {
            List<string> cachedItemList = new List<string>();
            foreach (KeyValuePair<string, object> entry in this.LocalCache)
            {
                cachedItemList.Add(entry.Key.ToString());
            }

            return cachedItemList;

        }

        /// <summary>
        /// Vacía todo el contenido del cache
        /// </summary>
        public void Clear()
        {
            foreach (KeyValuePair<string, object> entry in this.LocalCache)
            {
                this.LocalCache.Remove(entry.Key.ToString());
            }
        }

        /// <summary>
        /// Intenta obtener un elemento del caché y si no lo encuentra intenta obtenerlo invocando a la funcion pasada por parámetro.
        /// </summary>
        /// <remarks>
        /// Si el item es obtenido llamando a la función, es agregado al caché para obtenerlo desde allí en las futuras invocaciones. 
        /// </remarks>
        /// <param name="key">Clave del item</param>
        /// <param name="load">Función para obtener el elemento en caso de que no se encuentre en cache</param>
        /// <returns>Devuelve el item solicitado</returns>
        public T GetOrAdd<T>(string key, Func<T> load) where T : class
        {
            T item = this.GetItem(key) as T;

            if (item == null)
            {
                item = load();

                if (item == null)
                    throw new CacheException(Messages.CacheAddOrGetItemFailed, key);

                this.AddItem(key, item);
            }

            return item;
        }

        #endregion

    }
}