using System;
using System.Reflection;
using System.Linq;
using System.Collections.Generic;

namespace Alemana.Nucleo.Common.Caching.CacheManager
{
    public sealed class ObjectBinder : System.Runtime.Serialization.SerializationBinder
    {
        static Dictionary<string, Type> cache = new Dictionary<string, Type>();

        public override Type BindToType(string assemblyName, string typeName)
        {
            try
            {
                Type typeToDeserialize = null;

                String currentAssembly = Assembly.GetExecutingAssembly().FullName;
                // In this case we are always using the current assembly
                //assemblyName = currentAssembly;

                // Get the type using the typeName and assemblyName
                typeToDeserialize = FindType(typeName, assemblyName);

                return typeToDeserialize;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public static Type FindType(string typeName, string assemblyName)
        {
            if (cache.ContainsKey(typeName))
                return cache[typeName];

            Type t = Type.GetType(String.Format("{0}, {1}", typeName, assemblyName));

            if (t != null)
            {
                cache.Add(typeName, t);
                return t;
            }

            if (t == null)
            {
                t = Type.GetType(typeName);
            }

            if (cache.ContainsKey(typeName))
                return cache[typeName];

            t = AppDomain.CurrentDomain.GetAssemblies().FirstOrDefault(a => a.FullName == assemblyName).GetType(typeName);

            if (t == null)
            {
                foreach (var item in AppDomain.CurrentDomain.GetAssemblies())
                {
                    t = item.GetType(typeName);

                    if (t != null)
                        break;
                }
            }

            if (t != null)
                cache.Add(typeName, t);

            return t;

        }
    }

}