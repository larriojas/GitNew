using System;
using System.Globalization;
using System.Reflection;
using System.Runtime.Remoting.Messaging;
using System.Runtime.Remoting.Proxies;

namespace Alemana.Nucleo.Common.ComponentModel.Custom
{
    internal class RemotingProxyInterceptor<T> : RealProxy where T : class
    {
        #region Private Vars

        private string _realTypeFullName;

        #endregion

        #region Constructors

        public RemotingProxyInterceptor(string realTypeFullName) : base(typeof(T))
        {
            _realTypeFullName = realTypeFullName;
        }

        #endregion

        #region Public Methods
        
        public T CreateProxy( )
        {
            return base.GetTransparentProxy( ) as T;
        }

        public override IMessage Invoke( IMessage msg )
        {
            object result = null;

            IMethodCallMessage call = msg as IMethodCallMessage;
            if ( call != null )
                result = InvokeMember( _realTypeFullName, call.MethodName, call.Args );

            return new ReturnMessage( result, null, 0, null, call );
        }
    
        #endregion

        #region Private Members
        
        private static object CreateObject(Type type)
        {
            object result;

            try
            {
                result = Activator.CreateInstance(type);
            }
            catch
            {
                result = null;
            }

            return result;
        }

        private static object InvokeMember(string methodType, string methodName, params object[] methodParameters)
        {

            Type type = Type.GetType(methodType, true);
            object instance = CreateObject(type);
            object result = type.InvokeMember(methodName, BindingFlags.InvokeMethod, null, instance, methodParameters, CultureInfo.InvariantCulture);
       
            IDisposable dispInstance = instance as IDisposable;
            
            if ( dispInstance != null )
                dispInstance.Dispose( );
            instance = null;

            return result;
        }

        #endregion
    }
}