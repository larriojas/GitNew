using System;
using System.Collections.Generic;

namespace Alemana.Nucleo.Common.Caching
{
    /// <summary>
    /// Interfaz con los metodos que deben implementar los cache managers
    /// Autor: Lagash S.A.
    /// Fecha de creación: 29/04/2013
    /// Fecha de modificación: 26/05/2017
    /// </summary>
    public interface ICacheManager
    {
        /// <summary>
        /// Inserta un nuevo elemento al cache con el tiempo de vida por defecto si se
        /// encuentra configurado, y si tiempo de vida máximo en caso contrario.
        /// </summary>
        /// <param name="key">Clave del elemento</param>
        /// <param name="value">Elemento a insertar</param>
        void AddItem(string key, object value);

        /// <summary>
        /// Inserta un nuevo elemento al cache
        /// </summary>
        /// <param name="key">Clave del elemento</param>
        /// <param name="value">Elemento a insertar</param>
        /// <param name="lifetime">Duración de vida del objeto en segundos</param>
        void AddItem(string key, object value, int lifetime);

        /// <summary>
        /// Verifica si el cache tiene un item con clave <paramref name="key"/>
        /// </summary>
        /// <param name="key">Clave del item a buscar</param>
        /// <returns>Retorna true si el item se encuentra en el cache</returns>
        bool HasItem(string key);

        /// <summary>
        /// Obtiene un item del cache
        /// </summary>
        /// <param name="key">Clave del item</param>
        /// <returns>Item obtenido</returns>
        object GetItem(string key);

        /// <summary>
        /// Quita un item del cache
        /// </summary>
        /// <param name="key">Clave del item a quitar</param>
        void RemoveItem(string key);

        /// <summary>
        /// Renueva el tiempo de vida del item especificado
        /// </summary>
        /// <param name="key">Clave del item</param>
        /// <param name="lifetime">Nuevo tiempo de vida del item en segundos</param>
        /// <returns>Retorna true si se encontro el item en el cache y se pudo completar 
        /// la operación correctamente</returns>
        bool RenewItemLifetime(string key, int lifetime);

        /// <summary>
        /// Obtiene una lista con todas las claves de los items del cache
        /// </summary>
        /// <returns>Lista con todos los items del cache</returns>
        List<string> ListKeys();

        /// <summary>
        /// Vacía todo el contenido del cache
        /// </summary>
        void Clear();

        /// <summary>
        /// Intenta obtener un elemento del caché y si no lo encuentra intenta obtenerlo invocando a la funcion pasada por parámetro.
        /// </summary>
        /// <remarks>
        /// Si el item es obtenido llamando a la función, es agregado al caché para obtenerlo desde allí en las futuras invocaciones. 
        /// </remarks>
        /// <param name="key">Clave del item</param>
        /// <param name="load">Función para obtener el elemento en caso de que no se encuentre en cache</param>
        /// <returns>Devuelve el item solicitado</returns>
        T GetOrAdd<T>(string key, Func<T> load) where T : class;

    }
}
