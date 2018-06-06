using System;
using System.Collections.Generic;

namespace Alemana.Nucleo.Shared
{
    public sealed class DataHolder
    {
        private static volatile DataHolder instance;
        private static object syncRoot = new Object();

        private DataHolder()
        {
        }

        public static DataHolder Instance
        {
            get
            {
                if (instance == null)
                {
                    lock (syncRoot)
                    {
                        if (instance == null)
                            instance = new DataHolder();
                    }
                }

                return instance;
            }
        }

        private static Dictionary<string, object> Values = new Dictionary<string, object>();

        public static object GetValue(string key)
        {
            if (Values.ContainsKey(key))
                return Values[key];

            return null;
        }

        public static void SetValue(string key, object value)
        {
            if (Values.ContainsKey(key))
                Values[key] = value;
            else
                Values.Add(key, value);
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

    public class DataHolderKeys
    {
        public static string Roles = "Roles";
    }
}
