using Alemana.Nucleo.Common.Caching;
using System;
using System.Collections.Generic;

namespace Alemana.Nucleo.Shared
{
    public sealed class DataHolderFile
    {
        private static volatile DataHolderFile instance;
        private static object syncRoot = new Object();

        private DataHolderFile()
        {
        }

        public static DataHolderFile Instance
        {
            get
            {
                if (instance == null)
                {
                    lock (syncRoot)
                    {
                        if (instance == null)
                            instance = new DataHolderFile();
                    }
                }

                return instance;
            }
        }

        private static Dictionary<string, object> Values = new Dictionary<string, object>();

        public static object GetValue(string key)
        {
            if (Cache.Default.HasItem(key))
                return Cache.Default.GetItem(key);

            return null;
        }

        public static void SetValue(string key, object value)
        {
            if (Cache.Default.HasItem(key))
            {
                Cache.Default.RemoveItem(key);
                Cache.Default.AddItem(key, value);
            }

            Cache.Default.AddItem(key, value);
        }

        public object this[string key]
        {
            get
            {
                return GetValue(key);
            }

            set
            {
                SetValue(key, value);
            }
        }

        public static void ClearData()
        {
            Values.Clear();
        }
    }

    
}
