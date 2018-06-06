using Alemana.Nucleo.Common.ExceptionHandling;
using Alemana.Nucleo.Common.Exceptions;
using Microsoft.Practices.Unity.InterceptionExtension;
using System;

namespace Alemana.Nucleo.Common.Policies.Handlers
{
    /// <summary>
    /// Implementación del ICallHandler para la politica de exception handling de invocaciones a métodos.
    /// Autor: Lagash S.A.
    /// Fecha de creación: 29/04/2013
    /// Fecha de modificación: 29/04/2013
    /// </summary>
    /// <remarks>
    /// Esta clase aplica la política de manejo de excepciones de un método de una clase a la que se le haya aplicado
    /// la política de exception handling a través del atributo <see cref="ExceptionPolicyAttribute"/>.
    /// </remarks>
    public class ExceptionHandler : ICallHandler
    {
        #region fields
        private string _policyName;
        #endregion

        #region .ctor

        public ExceptionHandler(string policyName)
        {
            if (String.IsNullOrEmpty(policyName))
                throw new NucleoCommonException(Messages.ParameterCanBeNull, "policyName");
            
            _policyName = policyName;
        }

        #endregion

        #region ICallHandler Members

        /// <summary>
        /// Order en el que debe ejecutarse el Handler
        /// </summary>
        public int Order { get; set; }

        /// <summary>
        /// Implementación de la invocación del handler. Aplica la política de manejo de excepciones
        /// especificada en el constructor a través de un nombre, que debe estar en el archivo de configuración
        /// </summary>
        /// <param name="input">Datos de la invocación</param>
        /// <param name="getNext">Delegado al próximo eslabón de la cadena de responsabilidad</param>
        /// <returns>Resultado de la invocación</returns>
        public IMethodReturn Invoke(IMethodInvocation input, GetNextHandlerDelegate getNext)
        {
            //Ejecuto la siguiente policy o el target method
            IMethodReturn ret = getNext()(input, getNext);

            if(ret.Exception != null)
            {
                //Política de ExceptionHandling 
                ExceptionPolicy.HandleException(ret.Exception, _policyName);
            }

            // Devuelvo el resultado
            return ret;
                
        }

        #endregion
    }
}
