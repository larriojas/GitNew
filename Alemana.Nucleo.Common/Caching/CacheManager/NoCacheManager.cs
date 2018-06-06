using System;
using System.Collections.Generic;

namespace Alemana.Nucleo.Common.Caching.CacheManager
{
    /// <summary>
    /// Manejador de cache que no utiliza ningún cache
    /// Autor: Lagash S.A.
    /// Fecha de creación: 29/04/2013
    /// Fecha de modificación: 29/04/2013
    /// </summary>
    public class NoCacheManager : ICacheManager
    {
        #region ICacheManager Members

        /// <summary>
        /// Inserta un nuevo elemento al cache con el tiempo de vida por defecto si se
        /// encuentra configurado, y si tiempo de vida máximo en caso contrario.
        /// </summary>
        /// <param name="key">Clave del elemento</param>
        /// <param name="value">Elemento a insertar</param>
        public void AddItem(string key, object value)
        {
            return;
        }

        /// <summary>
        /// Inserta un nuevo elemento al cache
        /// </summary>
        /// <param name="key">Clave del elemento</param>
        /// <param name="value">Elemento a insertar</param>
        /// <param name="lifetime">Duración de vida del objeto en milisegundos</param>
        public void AddItem(string key, object value, int lifetime)
        {
            return;
        }

        /// <summary>
        /// Verifica si el cache tiene un item con clave <paramref name="key"/>
        /// </summary>
        /// <param name="key">Clave del item a buscar</param>
        /// <returns>Retorna true si el item se encuentra en el cache</returns>
        public bool HasItem(string key)
        {
            return false;
        }

        /// <summary>
        /// Obtiene un item del cache
        /// </summary>
        /// <param name="key">Clave del item</param>
        /// <returns>Item obtenido</returns>
        public object GetItem(string key)
        {
            return null;
        }

        /// <summary>
        /// Quita un item del cache
        /// </summary>
        /// <param name="key">Clave del item a quitar</param>
        public void RemoveItem(string key)
        {
            return;
        }

        /// <summary>
        /// Renueva el tiempo de vida del item especificado
        /// </summary>
        /// <param name="key">Clave del item</param>
        /// <param name="lifetime">Nuevo tiempo de vida del item en milisegundos</param>
        /// <returns>Retorna true si se encontro el item en el cache y se pudo completar 
        /// la operación correctamente</returns>
        public bool RenewItemLifetime(string key, int lifetime)
        {
            return false;
        }

        /// <summary>
        /// Obtiene una lista con todas las claves de los items del cache
        /// </summary>
        /// <returns>Lista con todos los items del cache</returns>
        public List<string> ListKeys()
        {
            return null;
        }

        /// <summary>
        /// Vacía todo el contenido del cache
        /// </summary>
        public void Clear()
        {
             return;
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
            return load();
        }
        
        #endregion
    }
}
