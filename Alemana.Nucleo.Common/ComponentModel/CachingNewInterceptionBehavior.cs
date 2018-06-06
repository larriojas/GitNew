using Alemana.Nucleo.Common.Caching;
using Microsoft.Practices.Unity.InterceptionExtension;
using Sixeyed.Caching;
using Sixeyed.Caching.Serialization.Serializers;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Alemana.Nucleo.Common.ComponentModel
{
    public class CachingNewInterceptionBehavior : IInterceptionBehavior
    {

        public IMethodReturn Invoke(IMethodInvocation input, GetNextInterceptionBehaviorDelegate getNext)
        {
            string key = CacheKeyBuilder.GetCacheKey(input, new BinarySerializer());

            key = key.Replace(".", string.Empty);

            if (Defaults.TipoInicio == 4 && IsInCache(key + "return"))
            {
                var res = FetchFromCache(key + "return");

                GetArgumentToCache(input, key);
                
                return input.CreateMethodReturn(res, GetArgument(input));
            }

            IMethodReturn methodReturn = getNext()(input, getNext);

            if (!input.MethodBase.ToString().Contains("System.Threading.Task") && Utility.ReflectionHelper.IsSerializable(input.MethodBase) && Defaults.HabilitarDistribuidorCache == "1")
            {
                if (methodReturn != null && methodReturn.ReturnValue != null && methodReturn.Exception == null)
                    AddToCache(key + "return", methodReturn);

                AddArgumentToCache(input, key);
            }


            return methodReturn;
        }

        private object[] GetArgument(IMethodInvocation input)
        {
            List<object> parametros = new List<object>();

            for (int i = 0; i < input.Arguments.Count; i++)
            {
                parametros.Add(input.Arguments[i]);
            }

            return parametros.ToArray();
        }

        private void AddArgumentToCache(IMethodInvocation input, string key)
        {
            List<object> parametros = new List<object>();

            for (int i = 0; i < input.Arguments.Count; i++)
            {
                if (input.Arguments.GetParameterInfo(i).ParameterType.IsByRef)
                    Caching.Cache.Default.AddItem(key + "argument" + input.Arguments.GetParameterInfo(i).Name, input.Arguments[i]);
            }
        }

        private void GetArgumentToCache(IMethodInvocation input, string key)
        {
            List<object> parametros = new List<object>();

            for (int i = 0; i < input.Arguments.Count; i++)
            {
                if (input.Arguments.GetParameterInfo(i).ParameterType.IsByRef)
                    input.Arguments[i] = FetchFromCache(key + "argument" + input.Arguments.GetParameterInfo(i).Name);
            }
        }

        private void SaveRefParameter(string methodName, IMethodInvocation input)
        {
            for (int i = 0; i < input.Arguments.Count; i++)
            {
                if (input.Arguments.GetParameterInfo(i).ParameterType.IsByRef)
                {
                    if (IsInCache(methodName + input.Arguments.GetParameterInfo(i).Name))
                    {
                        input.Arguments[i] = FetchFromCache(methodName + input.Arguments.GetParameterInfo(i).Name);
                        continue;
                    }

                    Caching.Cache.Default.AddItem(methodName + input.Arguments.GetParameterInfo(i).Name, input.Arguments[i]);
                }
            }
        }

        private static string GetKey(IMethodInvocation input)
        {
            string argumentKey = string.Empty;

            for (int i = 0; i < input.Arguments.Count; i++)
            {
                if (input.Arguments.GetParameterInfo(i).IsOut) ;

                if (input.Arguments[i] is DateTime && input.Arguments[i] != null)
                {
                    argumentKey = argumentKey + ((DateTime)input.Arguments[i]).ToShortDateString();
                    continue;
                }

                argumentKey = argumentKey + ((input.Arguments[i] != null) ? input.Arguments[i].ToString() : string.Empty);
            }

            var key = input.MethodBase.Module + input.MethodBase.Name + argumentKey.GetHashCode();

            key = key.Replace(".", string.Empty);
            key = key.Replace("dll", string.Empty);

            return key;
        }

        public IEnumerable<Type> GetRequiredInterfaces()
        {
            return Type.EmptyTypes;
        }

        public bool WillExecute
        {
            get { return true; }
        }

        private bool IsInCache(string key)
        {
            return Caching.Cache.Default.HasItem(key);
        }

        private object FetchFromCache(string key)
        {
            return Caching.Cache.Default.GetItem(key);
        }

        private void AddToCache(string item, IMethodReturn value)
        {
            Caching.Cache.Default.AddItem(item, value.ReturnValue);
        }
    }
}