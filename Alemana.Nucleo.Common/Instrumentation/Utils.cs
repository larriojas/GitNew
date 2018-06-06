using Alemana.Nucleo.Common.Exceptions;
using System;
using System.Diagnostics;
using System.Text;

namespace Alemana.Nucleo.Common.Instrumentation
{
    /// <summary>
    /// Funciones útiles al módulo de instrumentación
    /// Autor: Lagash S.A.
    /// Fecha de creación: 29/04/2013
    /// Fecha de modificación: 29/04/2013
    /// </summary>
    static class Utils
    {
        #region methods

        /// <summary>
        /// Convierte la entrada en un <see cref="PerformanceCounterCategoryType"/>
        /// </summary>
        /// <param name="categoryTypeName">Nombre del tipo de categoría</param>
        /// <returns><see cref="PerformanceCounterCategoryType"/> buscado</returns>
        public static PerformanceCounterCategoryType ToPerformanceCounterCategoryType(
            string categoryTypeName)
        {
            if (string.IsNullOrEmpty(categoryTypeName))
                throw new InstrumentationException(Messages.CategoryNameNullOrEmpty,new ArgumentNullException());

            try
            {
                return (PerformanceCounterCategoryType)Enum.Parse(typeof(PerformanceCounterCategoryType),
                    categoryTypeName);
            }
            catch (ArgumentException ae)
            {
                throw new InstrumentationException(string.Format(
                    Messages.InvalidCounterCategoryType, categoryTypeName, 
                    EnumerateEnumValues < PerformanceCounterCategoryType>()), ae);
            }
        }

        /// <summary>
        /// Convierte a <see cref="AlemanaPerformanceCounterType"/> el la representación en string
        /// del tipo de contador
        /// </summary>
        /// <param name="counterTypeName">Respresentacion en string del tipo de contador</param>
        /// <returns>tipo de contador</returns>
        public static AlemanaPerformanceCounterType ToAlemanaPerformanceCounterType(
            string counterTypeName)
        {
            if (string.IsNullOrEmpty(counterTypeName))
                throw new InstrumentationException(Messages.AlemanaCounterTypeNullOrEmpry);

            try
            {
                return (AlemanaPerformanceCounterType)Enum.Parse(typeof(AlemanaPerformanceCounterType),
                    counterTypeName);
            }
            catch (ArgumentException ae)
            {
                throw new InstrumentationException(string.Format(
                    Messages.InvalidAlemanaCounterType,
                    counterTypeName, EnumerateEnumValues<AlemanaPerformanceCounterType>()), ae);
            }
        }

        /// <summary>
        /// Mapea a <paramref name="AlemanaCounterType"/> en su correspondiente
        /// <see cref="PerformanceCounterType"/>
        /// </summary>
        /// <param name="AlemanaCounterType">tipo de contador de Alemana</param>
        /// <returns>tipo <see cref="PerformanceCounterType"/> asociado a 
        /// <paramref name="AlemanaCounterType"/>
        /// </returns>
        public static PerformanceCounterType ToPerformanceCounterType(
            AlemanaPerformanceCounterType AlemanaCounterType)
        {
            switch (AlemanaCounterType)
            {
                case AlemanaPerformanceCounterType.NumberOfItems:
                    return PerformanceCounterType.NumberOfItems64;
                case AlemanaPerformanceCounterType.AverageTimer:
                    return PerformanceCounterType.AverageTimer32;
                case AlemanaPerformanceCounterType.RateOfCountsPerSecond:
                    return PerformanceCounterType.RateOfCountsPerSecond64;
                default:
                    throw new InstrumentationException(string.Format(Messages.InvalidValue,
                        AlemanaCounterType.ToString()));
            }
        }

        /// <summary>
        /// Mapea un <see cref="AlemanaPerformanceCounterType"/> al tipo 
        /// <see cref="PerformanceCounterType"/> base asociado.
        /// </summary>
        /// <param name="AlemanaCounterType">tipo de contador Alemana</param>
        /// <returns>tipo base asociado</returns>
        /// <remarks>No todos los <see cref="AlemanaPerformanceCounterType"/> tienen
        /// tipo base asociado</remarks>
        public static PerformanceCounterType ToPerformanceCounterBaseType(
            AlemanaPerformanceCounterType AlemanaCounterType)
        {
            switch (AlemanaCounterType)
            {
                case AlemanaPerformanceCounterType.AverageTimer:
                    return PerformanceCounterType.AverageBase;
                default:
                    throw new InstrumentationException(string.Format(
                        Messages.ValueWithoutAssociatedBaseType,
                        AlemanaCounterType.ToString()));
            }
        }

        /// <summary>
        /// Arma un string con todos los valores del enumerado <paramref name="T"/>.
        /// </summary>
        /// <typeparam name="T">Tipo que debe ser <see cref="Enum"/></typeparam>
        /// <returns>string con todos los valores del enumerado</returns>
        public static string EnumerateEnumValues<T>()
        {
            Type enumType = typeof(T);

            if (enumType.BaseType != typeof(Enum))
                throw new InstrumentationException(
                    string.Format(Messages.ValueHasToBeEnum, enumType.ToString()));

            StringBuilder buffer = new StringBuilder();
            foreach (object value in Enum.GetValues(enumType))
            {
                if (buffer.Length == 0)
                    buffer.Append(value.ToString());
                else
                {
                    buffer.Append(" | " + value.ToString());
                }
            }

            return buffer.ToString();
        }

        #endregion
    }
}
