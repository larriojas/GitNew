namespace Alemana.Nucleo.Common.ExceptionHandling
{
    /// <summary>
    /// Enumerado con los valores de la acción a tomar al manejar una excepción.
    /// Autor: Lagash S.A.
    /// Fecha de creación: 29/04/2013
    /// Fecha de modificación: 29/04/2013
    /// </summary>
    public enum HandlingAction
    {
        /// <summary>
        /// Ninguno
        /// </summary>
        None = 0,
        /// <summary>
        /// Relanzar la excepción
        /// </summary>
        Rethrow = 1,
        /// <summary>
        /// Encapsular la excepción en otra
        /// </summary>
        Wrap = 2,
        /// <summary>
        /// Reemplazar la excepción
        /// </summary>
        Replace = 3,
        /// <summary>
        /// Loguea + Relanzar la excepción
        /// </summary>
        LogAndRethrow = 4,
        /// <summary>
        /// Encapsular la excepción en otra y loguea
        /// </summary>
        LogAndWrap = 5,
        /// <summary>
        /// Loguea + Reemplaza la excepción
        /// </summary>
        LogAndReplace = 6,
        /// <summary>
        /// Solamente loguea la excepción
        /// </summary>
        JustLog =7
    }
}
