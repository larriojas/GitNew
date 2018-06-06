namespace Alemana.Nucleo.Common.Instrumentation
{
    /// <summary>
    /// Tipos de contadores permitidos en la aplicación
    /// Autor: Lagash S.A.
    /// Fecha de creación: 29/04/2013
    /// Fecha de modificación: 29/04/2013
    /// </summary>
    public enum AlemanaPerformanceCounterType
    {
        /// <summary>
        /// Tipo cantidad de items
        /// </summary>
        NumberOfItems,
        /// <summary>
        /// Tipo promedio por tiempo
        /// </summary>
        AverageTimer,
        /// <summary>
        /// Tipo cantidad por segundo
        /// </summary>
        RateOfCountsPerSecond
    }
}
