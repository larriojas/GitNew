using System;
using System.Runtime.Serialization;

namespace Alemana.Nucleo.Common.Exceptions
{
    ///<summary>
    /// Excepción interna de Instrumentation. Indica un error interno en este módulo.
    /// Autor: Lagash S.A.
    /// Fecha de creación: 29/04/2013
    /// Fecha de modificación: 29/04/2013
    ///</summary>
    [Serializable]
    public class InstrumentationException : NucleoBaseException
    {
        #region fields
        private const int _eventId = 2005;
        #endregion

        #region Constructores

        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="mensaje">Mensaje de la excepción</param>
        /// <param name="args">Parametros a reemplazar en el mensaje</param>
        public InstrumentationException (string mensaje, params object[] args)
            : base(_eventId, mensaje, args)
        {
        }

        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="mensaje">Mensaje de le excepción</param>
        /// <param name="innerException">Excepción interna</param>
        /// <param name="args">Parametros a reemplazar en el mensaje</param>
        public InstrumentationException (Exception innerException, string mensaje, params object[] args)
            : base(_eventId, innerException, mensaje, args)
        {
        }

        /// <summary>
        /// Constructor para serialización
        /// </summary>
        /// <param name="info">Información de serialización</param>
        /// <param name="context">Parámetro de contexto</param>
        public InstrumentationException(SerializationInfo info, StreamingContext context)
            : base(_eventId, info, context)
        {
        }
        #endregion
    }
}
