using Alemana.Nucleo.Common.Exceptions;
using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.IO;
using System.Runtime.Caching;
using System.Runtime.Serialization;
using System.Runtime.Serialization.Formatters.Binary;
using System.Linq;

namespace Alemana.Nucleo.Common.Caching.CacheManager
{
    /// <summary>
    /// Manejador de cache que utiliza un BinarySerializer/>
    /// Autor: Lagash S.A.
    /// Fecha de creación: 29/04/2013
    /// Fecha de modificación: 29/04/2013
    /// </summary>
    public class BinarySerializerCacheManager : ICacheManager
    {

        public BinarySerializerCacheManager()
        {
            Init();
        }

        public BinarySerializerCacheManager(CachePolicyConfiguration policyConf)
        {
            Init();
        }

        private void Init()
        {
            formatter = new BinaryFormatter();

            if (!Directory.Exists("cache"))
                Directory.CreateDirectory("cache");

            if (!Directory.Exists("policy"))
                Directory.CreateDirectory("policy");

			string cachePath = System.Configuration.ConfigurationManager.AppSettings["Alemana.Nucleo.Contingencia.RutaGeneracionCache"];

			if (!String.IsNullOrWhiteSpace(cachePath))
			{
				try
				{
					if (!Directory.Exists(cachePath))
						Directory.CreateDirectory(cachePath);

					if (!Directory.Exists(Path.Combine(cachePath, "cache")))
						Directory.CreateDirectory(Path.Combine(cachePath, "cache"));
				}
				catch (Exception e)
				{
					Console.WriteLine(e.Message);
				}
			}
		}

        private IFormatter formatter;

        public void AddItem(string key, object value)
        {
            try
            {
                if (value == null)
                    value = "null";

                if (!key.EndsWith(".dat"))
                    key = key + ".dat";

				string cachePath = System.Configuration.ConfigurationManager.AppSettings["Alemana.Nucleo.Contingencia.RutaGeneracionCache"];
				
				cachePath = !string.IsNullOrWhiteSpace(cachePath) ? cachePath + @"\" : "";

				var path = Path.Combine(cachePath + @"cache", key);

                if (File.Exists(path))
                    File.Delete(path);

                var stream = new FileStream(path, FileMode.Create, FileAccess.Write, FileShare.None);
                formatter.Serialize(stream, value);
                stream.Close();
            }
            catch (Exception ex)
            {
                Console.WriteLine(string.Format("Error al agregar al caché la key {0}. Error: {1}.", key, ex.Message));
                //throw ex;
            }
        }

        public void AddItem(string key, object value, int lifetime)
        {
            AddItem(key, value);
        }

        static readonly object _object = new object();

        public bool HasItem(string key)
        {
            if (!key.EndsWith(".dat"))
                key = key + ".dat";

            var path = Path.Combine("cache", key);

            return File.Exists(path);
        }

        public object GetItem(string key)
        {
            try
            {
                if (!key.EndsWith(".dat"))
                    key = key + ".dat";

                var path = Path.Combine("cache", key);

                if (!File.Exists(path))
                    return null;

                var stream = new FileStream(path, FileMode.Open, FileAccess.Read, FileShare.Read);
                var value = formatter.Deserialize(stream);

                if (value.ToString() == "null")
                    return null;
                else
                    return value;
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error al obtener del caché la key {key}. Error: {ex.Message}.");
                //throw ex;
                return null;
            }
        }

        public void RemoveItem(string key)
        {
            try
            {
                var path = Path.Combine("cache", key);
                if (File.Exists(path))
                    File.Delete(path);
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error al obtener del caché la key {key}. Error: {ex.Message}.");
                //throw ex;
            }
        }

        public bool RenewItemLifetime(string key, int lifetime)
        {
            return true;
        }

        public List<string> ListKeys()
        {
            var keys = Directory.GetFiles("cache").Select((f) => f.Substring(0, f.Length - 3));
            return keys.ToList();
        }

        public void Clear()
        {
            if (!Directory.Exists("cache"))
                Directory.Delete("cache", true);
        }

        public T GetOrAdd<T>(string key, Func<T> load) where T : class
        {
            T item = this.GetItem(key) as T;

            if (item == null)
            {
                item = load();

                if (item == null)
                    throw new CacheException(Messages.CacheAddOrGetItemFailed, key);

                this.AddItem(key, item);
            }

            return item;
        }


    }
}