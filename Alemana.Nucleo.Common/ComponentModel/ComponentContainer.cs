using System;
using Alemana.Nucleo.Common.ComponentModel.Unity;

namespace Alemana.Nucleo.Common.ComponentModel
{
    /// <summary>
    /// Provee el comportamiento de contenedor de componentes
    /// a efectos de poder implementar el patrón de Inversión de Control (IoC).
    /// </summary>
    public class ComponentContainer : IComponentContainer
    {
        private static IComponentContainer _container;

        #region .ctor
        /// <summary>
        /// Constructor de la clase.
        /// </summary>
        public ComponentContainer()
        {
           _container = new UnityInjectorContainer();
        }

        #endregion

        #region Static Stuff

        private static ComponentContainer _instance;
                
        /// <summary>
        /// Instancia estática del contenedor.
        /// </summary>
        public static ComponentContainer Instance
        {
            get { return ComponentContainer._instance; }
        }

        /// <summary>
        /// Inicializa la instancia estática del contenedor.
        /// </summary>
        public static void Initialize()
        {
            if (!IsInitialized)
                _instance = new ComponentContainer();
        }

        public static bool IsInitialized
        {
            get
            {
                return _instance != null;
            }
        }

        #endregion

        #region IComponentContainer Members

        /// <summary>
        /// Registra el type especificado en el genérico TTo como 
        /// implementación de la interface pasada en TFrom.
        /// </summary>
        /// <typeparam name="TFrom">Interface del componente</typeparam>
        /// <typeparam name="TTo">Implementación del componente</typeparam>
        public void Register<TFrom, TTo>() where TTo : TFrom
        {
            _container.Register<TFrom, TTo>();
        }

        /// <summary>
        /// Registra el type especificado en el genérico TTo como 
        /// implementación de la interface pasada en TFrom, y lo identifica con un
        /// nombre para luego poder obtenerlo por el mismo.
        /// </summary>
        /// <typeparam name="TFrom">Interface del componente</typeparam>
        /// <typeparam name="TTo">Implementación del componente</typeparam>
        /// <param name="name">Nombre identificatorio de la implementación</param>
        public void Register<TFrom, TTo>(string name) where TTo : TFrom
        {
            _container.Register<TFrom, TTo>(name);
        }

        /// <summary>
        /// Registra el type especificado en el instance container con el nombre pasado como parámetro en name como 
        /// implementación de la interface pasada en TFrom, y lo identifica con un
        /// nombre para luego poder obtenerlo por el mismo.
        /// </summary>
        /// <typeparam name="TFrom">Interface del componente</typeparam>
        /// <param name="tToFullName">String conteniendo el full-name de la clase de implementación</param>
        public void Register<TFrom>(string tToFullName) where TFrom : class
        {
            _container.Register<TFrom>(tToFullName);
        }

        /// <summary>
        /// Registra el type especificado en el instance container con el nombre pasado como parámetro en name como 
        /// implementación de la interface pasada en TFrom, y lo identifica con un
        /// nombre para luego poder obtenerlo por el mismo.
        /// </summary>
        /// <typeparam name="TFrom">Interface del componente</typeparam>
        /// <param name="tToFullName">String conteniendo el full-name de la clase de implementación</param>
        /// <param name="name">Nombre identificatorio de la implementación</param>
        public void Register<TFrom>(string tToFullName, string name) where TFrom : class
        {
            _container.Register<TFrom>(tToFullName, name);
        }
        
        
        /// <summary>
        /// Obtiene la implementación previamente registrada para una interfase dada.
        /// </summary>
        /// <typeparam name="T">Interface del componente</typeparam>
        /// <returns>Devuelve una instancia que implementa la interface</returns>
        public T Resolve<T>() where T : class
        {
            return _container.Resolve<T>();
        }

        /// <summary>
        /// Obtiene la implementación previamente registrada para una interfase dada e identificada 
        /// por un nombre,
        /// </summary>
        /// <typeparam name="T">Interface del componente.</typeparam>
        /// <param name="name">Nombre identificatorio de la instancia a obtener.</param>
        /// <returns>Devuelve una instancia que implementa la interface</returns>
        public T Resolve<T>(string name) where T : class
        {
            return _container.Resolve<T>(name);
        }

        public void Register<TFrom, TTo>(bool useCache) where TTo : TFrom
        {
            _container.Register<TFrom, TTo>(useCache);
        }
        #endregion
    }
}
