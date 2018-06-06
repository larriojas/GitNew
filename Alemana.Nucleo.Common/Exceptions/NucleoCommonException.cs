using System;
using System.Runtime.Serialization;

namespace Alemana.Nucleo.Common.Exceptions
{
    public class NucleoCommonException : NucleoBaseException
    {
        #region fields
        private const int _eventId = 2001;
        #endregion

        #region Constructores

        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="mensaje">Mensaje de la excepción</param>
        /// <param name="args">Parametros a reemplazar en el mensaje</param>
        public NucleoCommonException (string mensaje, params object[] args)
            : base(_eventId, mensaje, args)
        {
        }

        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="mensaje">Mensaje de le excepción</param>
        /// <param name="innerException">Excepción interna</param>
        /// <param name="args">Parametros a reemplazar en el mensaje</param>
        public NucleoCommonException(Exception innerException, string mensaje, params object[] args)
            : base(_eventId, innerException, mensaje, args)
        {
        }

        /// <summary>
        /// Constructor para serialización
        /// </summary>
        /// <param name="info">Información de serialización</param>
        /// <param name="context">Parámetro de contexto</param>
        public NucleoCommonException(SerializationInfo info, StreamingContext context)
            : base(_eventId, info, context)
        {
        }

        #endregion

    }
}
