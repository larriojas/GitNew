using Microsoft.Practices.Unity.InterceptionExtension;

namespace Alemana.Nucleo.Common.Policies
{
    /// <summary>
    /// Representa un atributo para poder inyectar políticas de manejo de excepciones en los métodos 
    /// Autor: Lagash S.A.
    /// Fecha de creación: 29/04/2013
    /// Fecha de modificación: 29/04/2013
    /// </summary>
    public class ExceptionPolicyAttribute : HandlerAttribute
    {
        #region fields
        private string _policyName = null;
        #endregion

        #region .ctor

        public ExceptionPolicyAttribute(string policyName = null)
        {
            _policyName = policyName;
        }

        #endregion

        #region HandlerAtribute overrides

        /// <summary>
        /// Crea un handler de manejo de excepciones
        /// </summary>
        /// <returns>Un nuevo objeto call handler.</returns>
        public override ICallHandler CreateHandler(Microsoft.Practices.Unity.IUnityContainer container)
        {
            return new Handlers.ExceptionHandler(_policyName);
        }

        #endregion

    }
 }
