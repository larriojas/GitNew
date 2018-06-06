using System;
using System.Diagnostics;
using System.Threading;

namespace Alemana.Nucleo.Common.Tracing
{
    /// <summary>
    /// Representa los datos de trace a almacenar/recuperar
    /// Autor: Lagash S.A.
    /// Fecha de creación: 29/04/2013
    /// Fecha de modificación: 29/04/2013
    /// </summary>
    public class TraceRecord
    {
        #region fields

        #region static fields 
        static int processId = Process.GetCurrentProcess().Id;
        static string processName = Process.GetCurrentProcess().ProcessName;
        static string machineName = Environment.MachineName;
        static string userDomainName = Environment.UserDomainName;
        static string userName = Environment.UserName;
        #endregion

        int _traceId;
        DateTime _dateTime;
        DateTime _utcDateTime;
        long _timestamp;
        string _userName;
        int _processId;
        string _processName;
        int _threadId;
        string _level;
        string _source;
        Guid _activityId;
        string _application;
        string _callerMethod;
        string _message;
        string _context;
        string _callStack;
        string _machineName;
        #endregion

        #region properties
        /// <summary>
        /// Identificador del trace
        /// </summary>
        public int TraceId
        {
            get { return _traceId; }
            set { _traceId = value; }
        }

        /// <summary>
        /// Fecha del trace
        /// </summary>
        public DateTime DateTime
        {
            get { return _dateTime; }
            set { _dateTime = value; }
        }

        /// <summary>
        /// Fecha en formato UTC
        /// </summary>
        public DateTime UtcDateTime
        {
            get { return _utcDateTime; }
            set { _utcDateTime = value; }
        }

        /// <summary>
        /// Fecha en ticks
        /// </summary>
        public long Timestamp
        {
            get { return _timestamp; }
            set { _timestamp = value; }
        }

        /// <summary>
        /// Nombre del usuario Windows del proceso
        /// </summary>
        public string UserName
        {
            get { return _userName; }
            set 
            {
                if (!String.IsNullOrEmpty(value))
                    if (value.Length > 50)
                        _userName = value.Substring(0, 50);
                    else
                        _userName = value; 
            }
        }

        /// <summary>
        /// Identificador del proceso
        /// </summary>
        public int ProcessId
        {
            get { return _processId; }
            set { _processId = value; }
        }

        /// <summary>
        /// Nombre del proceso
        /// </summary>
        public string ProcessName
        {
            get { return _processName; }
            set
            {
                if (!String.IsNullOrEmpty(value))
                    if (value.Length > 255)
                        _processName = value.Substring(0, 255);
                    else
                        _processName = value;
            }
        }

        /// <summary>
        /// Nombre de la máquina
        /// </summary>
        public string MachineName
        {
            get { return _machineName; }
            set
            {
                if (!String.IsNullOrEmpty(value))
                    if (value.Length > 255)
                        _machineName = value.Substring(0, 255);
                    else
                        _machineName = value;
            }
        }

        /// <summary>
        /// Identificador del thread
        /// </summary>
        public int ThreadId
        {
            get { return _threadId; }
            set { _threadId = value; }
        }

        /// <summary>
        /// Nivel del thread
        /// </summary>
        public string Level
        {
            get { return _level; }
            set { _level = value; }
        }

        /// <summary>
        /// Origen
        /// </summary>
        public string Source
        {
            get { return _source; }
            set
            {
                if (!String.IsNullOrEmpty(value))
                    if (value.Length > 255)
                        _source = value.Substring(0, 255);
                    else
                        _source = value;
            }
        }

        /// <summary>
        /// Identificador de la actividad
        /// </summary>
        public Guid ActivityId
        {
            get { return _activityId; }
            set { _activityId = value; }
        }
        
        /// <summary>
        /// Nombre de la aplicación
        /// </summary>
        public string Application
        {
            get { return _application; }
            set
            {
                if (!String.IsNullOrEmpty(value))
                    if (value.Length > 50)
                        _application = value.Substring(0, 50);
                    else
                        _application = value;
            }
        }

        /// <summary>
        /// Método llamador
        /// </summary>
        public string CallerMethod
        {
            get { return _callerMethod; }
            set
            {
                if (!String.IsNullOrEmpty(value))
                    if (value.Length > 255)
                        _callerMethod = value.Substring(0, 255);
                    else
                        _callerMethod = value;
            }
        }

        /// <summary>
        /// Mensaje
        /// </summary>
        public string Message
        {
            get { return _message; }
            set { _message = value; }
        }

        /// <summary>
        /// Contexto
        /// </summary>
        public string Context
        {
            get { return _context; }
            set { _context = value; }
        }

        /// <summary>
        /// Stack de llamadas
        /// </summary>
        public string CallStack
        {
            get { return _callStack; }
            set { _callStack = value; }
        }

        /// <summary>
        /// Convierte el objeto a formato texto con algunos datos (modo lightweight = true)
        /// </summary>
        /// <returns>Mensaje</returns>
        public string ToShortString()
        {
            return String.Format("[{0}]:[{1}]",DateTime.ToString(), Message);
        }

        /// <summary>
        /// Convierte el objeto a formato texto con todos los datos (modo lightweight = false)
        /// </summary>
        /// <returns>Mensaje</returns>
        public override string ToString()
        {
            return String.Format("{0}|{1}|{2}|{3}|{4}", 
                DateTime.ToString(),
                this.ActivityId,
                Message,
                CallerMethod,
                Context);
        }

        /// <summary>
        /// Crea una copia del objeto
        /// </summary>
        /// <returns>Copia del objeto</returns>
        public TraceRecord Clone()
        {
            return new TraceRecord()
            {
                ActivityId = this.ActivityId,
                Application = this.Application,
                CallerMethod = this.CallerMethod,
                CallStack = this.Context,
                DateTime = this.DateTime,
                Level = this.Level,
                MachineName = this.MachineName,
                Message = this.Message,
                ProcessId = this.ProcessId,
                ProcessName = this.ProcessName,
                Source = this.Source,
                ThreadId = this.ThreadId,
                Timestamp = this.Timestamp,
                TraceId = this.TraceId,
                UserName = this.UserName,
                UtcDateTime = this.UtcDateTime
            };
        }


        #endregion

        #region Static Methods
        /// <summary>
        /// Recupera los valores por defecto para el objeto
        /// </summary>
        public static TraceRecord Default
        {
            get
            {
                return new TraceRecord()
                {
                    ActivityId = Trace.CorrelationManager.ActivityId,
                    Application = Defaults.ApplicationName,
                    DateTime = DateTime.Now,
                    UtcDateTime = DateTime.UtcNow,
                    Timestamp = DateTime.UtcNow.Ticks,
                    ProcessId = processId,
                    ProcessName = processName,
                    MachineName = machineName,
                    ThreadId = Thread.CurrentThread.ManagedThreadId,
                    UserName = userDomainName + "\\" + userName,
                    Level = TraceEventType.Information.ToString()
                };
            }
        }

        #endregion

    }
}
