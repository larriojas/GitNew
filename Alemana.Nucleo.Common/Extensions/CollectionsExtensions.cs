using System.Collections.Generic;
using System.Linq;

namespace Alemana.Nucleo.Common.Extensions
{
    public static class CollectionsExtensions
    {
        /// <summary>
        /// Determina si una colección posee elementos o no.
        /// </summary>
        /// <typeparam name="T">Tipo de datos de los items en la colección.</typeparam>
        /// <param name="input">Colección a comprobar.</param>
        /// <returns>
        /// Devuelve <c>true</c> si la colección especificada posee elementos, <c>false</c> en caso contrario.
        /// </returns>
        public static bool HasElements<T>(this IEnumerable<T> input)
        {
            return (input != null && input.Any());
        }

        public static IEnumerable<T> Enum<T>(this IEnumerable<T> input)
        {
            return input ?? Enumerable.Empty<T>();
        }
    }
}
