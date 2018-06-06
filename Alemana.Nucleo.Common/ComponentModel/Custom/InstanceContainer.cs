using System;
using System.Collections.Generic;

namespace Alemana.Nucleo.Common.ComponentModel.Custom
{
    internal class InstanceContainer
    {
        static InstanceContainer()
        {

        }

        static Dictionary<string, object> _proxies = new Dictionary<string, object>();

        public static void Register<T>(string realTypeFullName) where T : class
        {
            Type t = typeof(T);
            InstanceContainer.Register<T>(t.FullName, realTypeFullName);
        }

        public static void Register<T>(string name, string realTypeFullName) where T : class
        {
            if (_proxies.ContainsKey(name))
                return;

            ProxyBuilder<T> builder = new ProxyBuilder<T>(realTypeFullName);
            _proxies.Add(name, builder.BuildProxy());
        }

        public static T Get<T>() where T : class
        {
            Type t = typeof(T);
            return InstanceContainer.Get<T>(t.FullName);
        }

        public static T Get<T>(string name) where T : class
        {
            if (!_proxies.ContainsKey(name))
            {
                return null;
            }
            
            return (T)_proxies[name];
        }

    }
}
