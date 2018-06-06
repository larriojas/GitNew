using System.Configuration;

namespace Alemana.Nucleo.Common.Instrumentation.Configuration
{
    /// <summary>
    /// Representa la sección de instrumentación en el archivo de confirugación
    /// Autor: Lagash S.A.
    /// Fecha de creación: 29/04/2013
    /// Fecha de modificación: 29/04/2013
    /// </summary>
    class InstrumentationSection : ConfigurationSection
    {
        #region Variables

        private const string counterCategoryCollectionProperty = "performanceCounterCategories";

        #endregion Variables

        #region Properties

        /// <summary>
        /// Colección de elementos <see cref="PerformanceCounterCategoryElement"/>
        /// </summary>
        [ConfigurationProperty(counterCategoryCollectionProperty)]
        public PerformanceCounterCategoryElementCollection CategoryCollection
        {
            get
            {
                return (PerformanceCounterCategoryElementCollection)this[counterCategoryCollectionProperty];
            }
        } 

        #endregion Properties
    }
}
