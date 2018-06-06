using System;
using System.Collections;
using System.Collections.Generic;

namespace Alemana.Nucleo.Common.Security
{
    /// <summary>
    /// Clase que representa los Claim, éstos se utilizan para generar la identidad del usuario
    /// </summary>
    /// 
    [Serializable]
    public class ClaimDictionary : IDictionary<string, object>
    {
        private IDictionary<string, object> _dictionary;

        /// <summary>
        /// Constructor de los claims
        /// </summary>
        /// <param name="dictionary">Diccionario con los claims, (Eje: Datos del usuario a validar)</param>
        public ClaimDictionary(IDictionary<string, object> dictionary)
        {
            _dictionary = dictionary;
        }

        #region IDictionary<string,object> Members

        public void Add(string key, object value)
        {
            throw new NotSupportedException("This dictionary is read-only");
        }

        public bool ContainsKey(string key)
        {
            return _dictionary.ContainsKey(key);
        }

        public ICollection<string> Keys
        {
            get { return _dictionary.Keys; }
        }

        public bool Remove(string key)
        {
            throw new NotSupportedException("This dictionary is read-only");
        }

        public bool TryGetValue(string key, out object value)
        {
            return _dictionary.TryGetValue(key, out value);
        }

        public ICollection<object> Values
        {
            get { return _dictionary.Values; }
        }

        public object this[string key]
        {
            get
            {
                return _dictionary[key];
            }
            set
            {
                if (_dictionary.ContainsKey(key))
                    _dictionary[key] = value;
                else
                    _dictionary.Add(key, value);
            }
        }

        #endregion

        #region ICollection<KeyValuePair<string,object>> Members

        public void Add(KeyValuePair<string, object> item)
        {
            throw new NotSupportedException("This dictionary is read-only");
        }

        public void Clear()
        {
            throw new NotSupportedException("This dictionary is read-only");
        }

        public bool Contains(KeyValuePair<string, object> item)
        {
            return _dictionary.Contains(item);
        }

        public void CopyTo(KeyValuePair<string, object>[] array, int arrayIndex)
        {
            _dictionary.CopyTo(array, arrayIndex);
        }

        public int Count
        {
            get { return _dictionary.Count; }
        }

        public bool IsReadOnly
        {
            get { return true; }
        }

        public bool Remove(KeyValuePair<string, object> item)
        {
            throw new NotSupportedException("This dictionary is read-only");
        }

        #endregion

        #region IEnumerable<KeyValuePair<string,object>> Members

        public IEnumerator<KeyValuePair<string, object>> GetEnumerator()
        {
            return _dictionary.GetEnumerator();
        }

        #endregion

        #region IEnumerable Members

        IEnumerator IEnumerable.GetEnumerator()
        {
            return (_dictionary as System.Collections.IEnumerable).GetEnumerator();
        }

        #endregion

    }

}
