using System;
using System.Runtime.Serialization;

namespace Alemana.Nucleo.Common.Exceptions
{
    public class SecurityManagerException : NucleoBaseException
    {
        #region fields
        private const int _eventId = 2007;
        #endregion

        #region Constructores.

        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="message">Mensaje de la excepción</param>
        /// <param name="args">Parametros a reemplazar en el mensaje</param>
        public SecurityManagerException(string message, params object[] args)
            : base(_eventId, message, args)
        {
        }

        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="message">Mensaje de la excepción</param>
        /// <param name="innerException">Excepción interna</param>
        /// <param name="args">Parametros a reemplazar en el mensaje</param>
        public SecurityManagerException(Exception innerException, string message, params object[] args)
            : base(_eventId, message, innerException)
        {
        }

        /// <summary>
        /// Constructor para serialización
        /// </summary>
        /// <param name="info">Información de serialización</param>
        /// <param name="context">Parámetro de contexto</param>
        public SecurityManagerException(SerializationInfo info, StreamingContext context)
            : base(_eventId, info, context)
        {
        }
        #endregion

    }
}
