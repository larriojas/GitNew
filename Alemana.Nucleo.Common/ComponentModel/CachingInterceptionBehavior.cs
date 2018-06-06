using Alemana.Nucleo.Common.Caching;
using Alemana.Nucleo.Common.Tracing;
using Alemana.Nucleo.Common.Utility;
using Microsoft.Practices.Unity.InterceptionExtension;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Alemana.Nucleo.Common.ComponentModel
{
    public class CachingInterceptionBehavior : IInterceptionBehavior
    {

        public IMethodReturn Invoke(IMethodInvocation input, GetNextInterceptionBehaviorDelegate getNext)
        {
            string methodName = GetKey(input);

            if (Defaults.TipoInicio == 4)
            {
                if (IsInCache(methodName))
                {
                    SaveRefParameter(methodName, input);

                    object[] parametros = GetParametros(input.Arguments);

                    return input.CreateMethodReturn(
                      FetchFromCache(methodName), parametros);
                }
                //else
                //    MessageBox.Show($"No existen datos en el caché para el servicio {methodName}.");
            }

            IMethodReturn result = getNext()(input, getNext);

            if (!input.MethodBase.ToString().Contains("System.Threading.Task") && Utility.ReflectionHelper.IsSerializable(input.MethodBase))
            {
                AddToCache(methodName, result);
                SaveRefParameter(methodName, input);
            }

            return result;
        }

        private object[] GetParametros(IParameterCollection arguments)
        {
            List<object> parametros = new List<object>();

            for (int i = 0; i < arguments.Count; i++)
            {
                parametros.Add(arguments[i]);
            }

            return parametros.ToArray();
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

                    Cache.Default.AddItem(methodName + input.Arguments.GetParameterInfo(i).Name, input.Arguments[i]);
                }
            }
        }

        private static string GetKey(IMethodInvocation input)
        {
            using (Tracer t = new Tracer(TraceSourceOffile.GetCacheTraceSource()))
            {
                string argumentKey = string.Empty;
                string argumentKeyForLog = string.Empty;

                for (int i = 0; i < input.Arguments.Count; i++)
                {
                    if (input.Arguments[i] is DateTime && input.Arguments[i] != null)
                    {
                        argumentKey = argumentKey + ((DateTime)input.Arguments[i]).ToShortDateString();
                        argumentKeyForLog = "|" + argumentKeyForLog + ((DateTime)input.Arguments[i]).ToShortDateString();
                        continue;
                    }

                    argumentKey = argumentKey + ((input.Arguments[i] != null) ? input.Arguments[i].ToString() : string.Empty);
                    argumentKeyForLog = "|" + argumentKeyForLog + ((input.Arguments[i] != null) ? input.Arguments[i].ToString() : string.Empty);
                }

                var key = input.MethodBase.Module + input.MethodBase.Name + argumentKey.GetHashCode();
                var keyForLog = input.MethodBase.Module + "|" + input.MethodBase.Name + "|" + argumentKeyForLog;

                t.TraceVerbose(keyForLog);

                key = key.Replace(".", string.Empty);
                key = key.Replace("dll", string.Empty);

                return key;
            }
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
            return Cache.Default.HasItem(key);
        }

        private object FetchFromCache(string key)
        {
            return Cache.Default.GetItem(key);
        }

        private void AddToCache(string item, IMethodReturn result)
        {
            Cache.Default.AddItem(item, result.ReturnValue);
        }
    }
}