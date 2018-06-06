using System;
using System.Runtime.Serialization;
using System.Text;

namespace Alemana.Nucleo.Common.Exceptions
{
    [Serializable]
    public class NucleoBaseException : Exception
    {
        #region fields
        private string _uniqueId = Guid.NewGuid().ToString();
        private const short _category = 1;
        private string _message;
        private string _innerExceptionMessages = string.Empty;
        private int _eventId = 0;
        #endregion fields

        #region properties
        /// <summary>
        /// Identificador unico de instancia de error.
        /// (un Guid único para identificar la instancia del error). Este dato perfectamente 
        /// se le puede mostrar al usuario, para tener una trazabilidad exacta del error y 
        /// su correspondiente mensaje.
        /// </summary>
        public string UniqueId
        {
            get
            {
                return _uniqueId;
            }
        }

        #endregion

        #region Constructores

        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="message">Mensaje de la excepción</param>
        public NucleoBaseException(int eventId, string message, params object[] args)
            : base(String.Format(message, args))
        {
            _eventId = eventId;
            _message = String.Format(message, args);
        }

        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="message">Mensaje de la excepción</param>
        /// <param name="innerException">Excepción interna</param>
        public NucleoBaseException(int eventId, Exception innerException, string message, params object[] args)
            : base(String.Format(message, args), innerException)
        {
            _eventId = eventId;
            _message = String.Format(message, args);
            
            SetInnerExceptionsMessages(innerException);
        }

        /// <summary>
        /// Constructor para serialización
        /// </summary>
        /// <param name="info">Información de serialización</param>
        /// <param name="context">Parámetro de contexto</param>
        public NucleoBaseException(int eventId, SerializationInfo info, StreamingContext context)
            : base(info, context)
        {
            _eventId = eventId;
            _message = context.ToString();
        }

        #endregion

        #region Private methods

        private void SetInnerExceptionsMessages(Exception innerException)
        {
            _innerExceptionMessages = "InnerExceptions->";
            
            StringBuilder sb = new StringBuilder();
            
            while(innerException!=null) 
            {
                sb.Append(innerException.GetType().ToString());
                sb.Append(":");
                sb.Append(innerException.Message);
                sb.Append("Stack Trace:");
                sb.Append(innerException.StackTrace);
                sb.Append("|");

                innerException = innerException.InnerException;
            }

            
            _innerExceptionMessages += sb.ToString();
            //MessageBox.Show("Error de dll: " + sb.ToString());

        }
        
        #endregion

    }
}
