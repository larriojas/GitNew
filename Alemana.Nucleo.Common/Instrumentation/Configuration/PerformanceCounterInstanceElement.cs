using System.Configuration;

namespace Alemana.Nucleo.Common.Instrumentation.Configuration
{
    /// <summary>
    /// Representa un elemento de instancia de contador
    /// Autor: Lagash S.A.
    /// Fecha de creación: 29/04/2013
    /// Fecha de modificación: 29/04/2013
    /// </summary>
    class PerformanceCounterInstanceElement : ConfigurationElement
    {
        #region fields

        private const string nameProperty = "name";
        private const string isActiveProperty = "isActive"; 

        #endregion fields

        #region ctor and finalizers

        /// <summary>
        /// Constructor
        /// </summary>
        public PerformanceCounterInstanceElement()
        {

        }

        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="name"></param>
        public PerformanceCounterInstanceElement(string name)
        {
            Name = name;
        } 

        #endregion ctor and finalizers

        #region properties

        /// <summary>
        /// Nombre del elemento
        /// </summary>
        [ConfigurationProperty(nameProperty, IsKey = true)]
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
        /// Indica si el elemento esta activo
        /// </summary>
        [ConfigurationProperty(isActiveProperty, DefaultValue = true)]
        public bool IsActive
        {
            get
            {
                return (bool)this[isActiveProperty];
            }
            set
            {
                this[isActiveProperty] = value;
            }
        } 

        #endregion properties
    }
}
