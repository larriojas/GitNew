
namespace Alemana.Nucleo.Common.ComponentModel.Custom
{
    internal class ProxyBuilder<T> where T : class
    {
        #region Constructors

        public ProxyBuilder() : this(typeof(T).Name) {}
        
        public ProxyBuilder(string realTypeFullName)
        {
            _realTypeFullName = realTypeFullName;
            _proxyGenerator = new RemotingProxyGenerator();
        }

        #endregion

        #region Properties2

        private string _realTypeFullName;

        public string RealTypeFullName
        {
            get
            {
                return _realTypeFullName;
            }
        }

        private IProxyGenerator _proxyGenerator;

        public IProxyGenerator ProxyGenerator
        {
            get
            {
                return _proxyGenerator;
            }
        }

        #endregion        

        #region Public Methods
        
        public T BuildProxy( )
        {
            return _proxyGenerator.BuildProxy<T>( _realTypeFullName );
        }
        
        #endregion
    }
}