using System.Configuration;

namespace Alemana.Nucleo.Common.ExceptionHandling.Configuration
{
    /// <summary>
    /// Representa una política en el archivo de configuración.
    /// Autor: Lagash S.A.
    /// Fecha de creación: 29/04/2013
    /// Fecha de modificación: 29/04/2013
    /// </summary>
    public class ExceptionPolicyElement : ConfigurationElement
    {
        #region fields
        
        private const string logProperty = "log";
        private const string nameProperty = "name";
        private const string exceptionTypeProperty = "exceptionTypes";

        #endregion fields

        #region ctor and finalizers

        /// <summary>
        /// Constructor del elemento
        /// </summary>
        public ExceptionPolicyElement()
        {

        }

        /// <summary>
        /// Constructor del elemento
        /// </summary>
        /// <param name="name">Nombre del elemento</param>
        public ExceptionPolicyElement(string name)
        {
            Name = name;
        } 

        #endregion ctor and finalizers

        #region properties

        /// <summary>
        /// Propiedad log
        /// </summary>
        [ConfigurationProperty(logProperty, DefaultValue = "false")]
        public bool IsLogged
        {
            get
            {
                return (bool)this[logProperty];
            }
            set
            {
                this[logProperty] = value;
            }
        }

        /// <summary>
        /// Propiedad Name
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
        /// Colección de <see cref="ExceptionTypeCollection"/>
        /// </summary>
        [ConfigurationProperty(exceptionTypeProperty)]
        public ExceptionTypeElementCollection ExceptionTypeCollection
        {
            get
            {
                return (ExceptionTypeElementCollection)this[exceptionTypeProperty];
            }
        } 

        #endregion properties
    }
}
