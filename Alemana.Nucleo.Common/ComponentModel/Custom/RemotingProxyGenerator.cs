
namespace Alemana.Nucleo.Common.ComponentModel.Custom
{
    internal class RemotingProxyGenerator : IProxyGenerator
    {
        #region IProxyGenerator Members

        public T BuildProxy<T>( string realTypeFullName ) where T : class
        {
            RemotingProxyInterceptor<T> interceptor = new RemotingProxyInterceptor<T>( realTypeFullName );

            return interceptor.CreateProxy( );
        }

        #endregion
    }
}