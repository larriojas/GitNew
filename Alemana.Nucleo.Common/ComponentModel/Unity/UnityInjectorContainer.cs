using Microsoft.Practices.Unity;
using Microsoft.Practices.Unity.InterceptionExtension;
using System;

namespace Alemana.Nucleo.Common.ComponentModel.Unity
{
    /// <summary>
    /// Implementación del contenedor basada en el componente Unity de Microsoft P&P
    /// </summary>
    internal class UnityInjectorContainer : IComponentContainer
    {
        private IUnityContainer _unityContainer;

        #region .ctor
        /// <summary>
        /// Constructor de la clase. Crea el contenedor y agrega la extensión de intercepción para inyectar políticas
        /// </summary>
        public UnityInjectorContainer()
        {
            _unityContainer = new UnityContainer();
            _unityContainer.AddNewExtension<Interception>();
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
            _unityContainer.RegisterType<TFrom, TTo>().Configure<Interception>().SetInterceptorFor<TFrom>(new InterfaceInterceptor());
        }

        public void Register<TFrom, TTo>(bool useCache) where TTo : TFrom
        {
            if (useCache)
                _unityContainer.RegisterType<TFrom, TTo>(new InterceptionBehavior<CachingInterceptionBehavior>()).Configure<Interception>().SetInterceptorFor<TFrom>(new InterfaceInterceptor());

            _unityContainer.RegisterType<TFrom, TTo>().Configure<Interception>().SetInterceptorFor<TFrom>(new InterfaceInterceptor());
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
            _unityContainer.RegisterType<TFrom, TTo>(name).Configure<Interception>().SetInterceptorFor<TFrom>(name, new InterfaceInterceptor());
        }

        public void RegisterInternal<TFrom, TTo>(string name) where TTo : TFrom
        {
            if (String.IsNullOrWhiteSpace(name))
                Register<TFrom, TTo>();
            else
                Register<TFrom, TTo>(name);
        }
        
        /// <summary>
        /// Registra el type representado como un fullname en el string pasado como parámetro,
        /// asociado a la interface pasada en TFrom.
        /// </summary>
        /// <typeparam name="TFrom">Type de la interface</typeparam>
        /// <param name="tToFullName">Fullname de la clase que implementa la interface TFrom</param>
        public void Register<TFrom>(string tToFullName) where TFrom : class
        {
            Type t = Utility.ReflectionHelper.GetType(tToFullName);
            Utility.ReflectionHelper.GenericInvoke(this, typeof(TFrom), t, "RegisterInternal", null); 
        }


        /// <summary>
        /// Registra el type representado como un fullname en el string pasado como parámetro,
        /// asociado a la interface pasada en TFrom y a un nombre de instancia.
        /// </summary>
        /// <typeparam name="TFrom">Type de la interface</typeparam>
        /// <param name="tToFullName">Fullname de la clase que implementa la interface TFrom</param>
        public void Register<TFrom>(string tToFullName, string name) where TFrom : class
        {
            Type t = Utility.ReflectionHelper.GetType(tToFullName);
            Utility.ReflectionHelper.GenericInvoke(this, typeof(TFrom), t, "RegisterInternal", name);
        }

        
        /// <summary>
        /// Obtiene la implementación previamente registrada para una interfase dada.
        /// </summary>
        /// <typeparam name="T">Interface del componente</typeparam>
        /// <returns>Devuelve una instancia que implementa la interface</returns>
        public T Resolve<T>() where T : class
        {
            if (!_unityContainer.IsRegistered<T>())
                return null;

            return _unityContainer.Resolve<T>();
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
            if (!_unityContainer.IsRegistered<T>(name))
                return null;
            
            return _unityContainer.Resolve<T>(name);
        }

        #endregion
    }
}
