using Alemana.Nucleo.Common.Exceptions;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Diagnostics;
using System.Reflection;
using System.Text;

namespace Alemana.Nucleo.Common.Tracing
{
    /// <summary>
    /// Clase encargada de realizar el tracing
    /// Autor: Lagash S.A.
    /// Fecha de creación: 29/04/2013
    /// Fecha de modificación: 29/04/2013
    /// </summary>
    public class Tracer : IDisposable
    {
        #region fields
        /// <summary>
        /// Cantidad e frames a saltearse cuando se obtiene el nombre del metodo llamador
        /// desde Initialize()
        /// </summary>
        private const int initializeMethodFramesQuantity = 2;

        private TraceSource _traceSource;
        private bool? _lightweightSwitch;
        private bool _isDisposed = false;
        
        private bool _isLogicalOperationStart = false;
        private TraceRecord _defaultTraceRecord;

        /// <summary>
        /// Propiedades definidas por el usuario 
        /// </summary>
        private Dictionary<string, object> _context;

        #endregion fields

        #region ctor & finalizers

        /// <summary>
        /// Constructor que no inicia una nueva operación lógica
        /// </summary>
        /// <example>
        /// En el ejemplo se muestra el uso del componente de tracing
        /// <code>
        /// using (Tracer t = new Tracer())
        /// {
        ///     Metodo();
        ///     OtroMetodo();
        ///     ...
        /// }
        /// </code>
        /// </example>
        public Tracer()
        {
            Initialize(null, null, null);
        }

        /// <summary>
        /// Constructor que inicia una nueva operación lógica
        /// </summary>
        /// <param name="operation">Nombre de la operación lógica que se inicia</param>
        /// <example>
        /// En el ejemplo se muestra el uso del componente de tracing
        /// <code>
        /// using (Tracer t = new Tracer("Nombre de operación lógica"))
        /// {
        ///     Metodo();
        ///     OtroMetodo();
        ///     ...
        /// }
        /// </code>
        /// </example>
        public Tracer(string operation)
        {
            Initialize(operation, null, null);
        }


        /// <summary>
        /// Constructor.
        /// Este overload permite especificarle un source específico donde hacer el log en lugar
        /// de utilizar el por default de la aplicacion.
        /// </summary>
        /// <param name="customTraceSource">trace source explicito al cual loguear</param>
        /// <example>
        /// En el ejemplo se muestra el uso del componente de tracing
        /// <code>
        /// using (Tracer t = new Tracer(new TraceSource(minombredetracesource, SourceLevels.All))
        /// {
        ///     t.TraceError(mimensaje)
        ///     ...
        /// }
        /// </code>
        /// </example>
        public Tracer(TraceSource customTraceSource)
        {
            Initialize(null, customTraceSource, null);
        }

        /// <summary>
        /// Constructor.
        /// Este overload permite especificarle un source específico donde hacer el log en lugar
        /// de utilizar el por default de la aplicacion.
        /// </summary>
        /// <param name="customTraceSource">trace source explicito al cual loguear</param>
        /// <param name="callerMethodName">método que inicia el trace</param>
        /// <example>
        /// En el ejemplo se muestra el uso del componente de tracing
        /// <code>
        /// using (Tracer t = new Tracer(new TraceSource(minombredetracesource, SourceLevels.All))
        /// {
        ///     t.TraceError(mimensaje)
        ///     ...
        /// }
        /// </code>
        /// </example>
        public Tracer(TraceSource customTraceSource, string callerMethodName)
        {
            Initialize(null, customTraceSource, callerMethodName);
        }

        /// <summary>
        /// Constructor.
        /// Este overload permite especificarle un source específico donde hacer el log en lugar
        /// de utilizar el por default de la aplicacion.
        /// </summary>
        /// <param name="operation">Nombre de la operación lógica que se inicia</param>
        /// <param name="customTraceSource">trace source explicito al cual loguear</param>
        /// <example>
        /// En el ejemplo se muestra el uso del componente de tracing
        /// <code>
        /// using (Tracer t = new Tracer("Nombre de operación lógica", new TraceSource(minombredetracesource, SourceLevels.All))
        /// {
        ///     t.TraceError(mimensaje)
        ///     ...
        /// }
        /// </code>
        /// </example>
        public Tracer(string operation, TraceSource customTraceSource)
        {
            Initialize(operation, customTraceSource, null);
        }


        /// <summary>
        /// Libera los recursos utilizados por el objeto
        /// </summary>
        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        /// <summary>
        /// Libera los recursos utilizados por el objeto
        /// </summary>
        /// <param name="disposing">Define si ya se comenzó el proceso de liberación de recursos</param>
        protected virtual void Dispose(bool disposing)
        {
            if (disposing && !this._isDisposed)
            {
                if (!this.IsLightweightEnabled)
                {
                    if (_context.Count > 0)
                    {
                        TraceRecord traceContext = _defaultTraceRecord.Clone();
                        traceContext.DateTime = DateTime.Now;
                        traceContext.UtcDateTime = DateTime.UtcNow;
                        traceContext.Timestamp = DateTime.UtcNow.Ticks;
                        traceContext.Level = TraceEventType.Verbose.ToString();
                        traceContext.Context = ContextToText();
                        traceContext.Message = "Valores del contexto de la actividad.";
                        traceContext.Source = _traceSource.Name;

                        _traceSource.TraceData(TraceEventType.Verbose, (int)EventType.ContextValues, TraceRecordToText(traceContext));
                    }

                    TraceRecord traceStop = _defaultTraceRecord.Clone();
                    traceStop.DateTime = DateTime.Now;
                    traceStop.UtcDateTime = DateTime.UtcNow;
                    traceStop.Timestamp = DateTime.UtcNow.Ticks;
                    traceStop.Level = TraceEventType.Stop.ToString();
                    traceStop.Message = "Finalización de la actividad.";
                    traceStop.Source = _traceSource.Name;

                    _traceSource.TraceData(TraceEventType.Stop, (int)EventType.EndMethod, TraceRecordToText(traceStop));
                }

                if (this._isLogicalOperationStart)
                    Trace.CorrelationManager.StopLogicalOperation();

                this._traceSource.Flush();

                this._isDisposed = true;
            }
        }

        /// <summary>
        /// Libera los recursos utilizados por el objeto
        /// </summary>
        ~Tracer()
        {
            Dispose(false);
        }
        #endregion 

        #region Properties

        internal TraceSource TraceSource
        {
            get
            {
                return _traceSource;
            }
        }


        /// <summary>
        /// Retorna si la bandera de log liviano está activa
        /// </summary>
        private bool IsLightweightEnabled
        {
            get
            {
                if (this._isDisposed)
                    throw new TraceException(new ObjectDisposedException(Messages.ResourceDisposed), Messages.ResourceDisposed);

                if (this._lightweightSwitch == null)
                {
                    //switch definido en configuración
                    this._lightweightSwitch = GetBoolAppSetting(Defaults.LightweightTraceSetting);
                }

                return this._lightweightSwitch.Value;
            }
        }

        #endregion Properties

        #region Methods

        /// <summary>
        /// Escribe un mensaje de error en el log
        /// </summary>
        /// <param name="message">Mensaje a insertar en el log</param>
        /// <param name="args">Parametros de formateo del mensaje</param>
        public void TraceError(string message, params object[] args)
        {
            if (this._isDisposed)
                throw new TraceException(new ObjectDisposedException(Messages.ResourceDisposed), Messages.ResourceDisposed);

            TraceRecord trace = GetTraceRecord(TraceEventType.Error, message, args);

            this._traceSource.TraceData(TraceEventType.Error, (int)EventType.TraceWrite, TraceRecordToText(trace));
        }

        /// <summary>
        /// Escribe un mensaje de información en el log
        /// </summary>
        /// <param name="message">Mensaje a insertar en el log</param>
        /// <param name="args">Parametros de formateo del mensaje</param>
        public void TraceInformation(string message, params object[] args)
        {
            if (this._isDisposed)
                throw new TraceException(new ObjectDisposedException(Messages.ResourceDisposed), Messages.ResourceDisposed);

            TraceRecord trace = GetTraceRecord(TraceEventType.Information, message, args);

            this._traceSource.TraceData(TraceEventType.Information, (int)EventType.TraceWrite, TraceRecordToText(trace));
        }

        /// <summary>
        /// Escribe un mensaje de advertencia en el log
        /// </summary>
        /// <param name="message">Mensaje a insertar en el log</param>
        /// <param name="args">Parametros de formateo del mensaje</param>
        public void TraceWarning(string message, params object[] args)
        {
            if (this._isDisposed)
                throw new TraceException(new ObjectDisposedException(Messages.ResourceDisposed), Messages.ResourceDisposed);

            TraceRecord trace = GetTraceRecord(TraceEventType.Warning, message, args);

            this._traceSource.TraceData(TraceEventType.Warning, (int)EventType.TraceWrite, TraceRecordToText(trace));
        }

        /// <summary>
        /// Escribe un mensaje verborrajico en el log
        /// </summary>
        /// <param name="message">Mensaje a insertar en el log</param>
        /// <param name="args">Parametros de formateo del mensaje</param>
        public void TraceVerbose(string message, params object[] args)
        {
            if (this._isDisposed)
                throw new TraceException(new ObjectDisposedException(Messages.ResourceDisposed), Messages.ResourceDisposed);

            TraceRecord trace = GetTraceRecord(TraceEventType.Verbose, message, args);

            this._traceSource.TraceData(TraceEventType.Verbose, (int)EventType.TraceWrite, TraceRecordToText(trace));
        }


        /// <summary>
        /// Escribe en el log una entrada con los valores especificados en <paramref name="data"/>
        /// </summary>
        /// <param name="message">Información a insertar en el log</param>
        /// <param name="args">Parametros de formateo del mensaje</param>
        public void TraceWrite(string message, params object[] args)
        {
            if (this._isDisposed)
                throw new TraceException(new ObjectDisposedException(Messages.ResourceDisposed), Messages.ResourceDisposed);

            this.TraceInformation(message, args);
        }

        /// <summary>
        /// Agrega un valor con un id al contexto del tracer
        /// </summary>
        /// <param name="id">Idenrificador de lo que se quiere agregar al contexto</param>
        /// <param name="value">Valor de lo que se quiere agregar al contexto</param>
        public void AddToContext(string id, object value)
        {
            if (this._isDisposed)
                throw new TraceException(new ObjectDisposedException(Messages.ResourceDisposed), Messages.ResourceDisposed);

            _context.Add(id, value);
        }

        /// <summary>
        /// Quita el valor con determinado id del contexto
        /// </summary>
        /// <param name="id">Identificador del valor a quitar del contexto</param>
        public void RemoveFromContext(string id)
        {
            if (this._isDisposed)
                throw new TraceException(new ObjectDisposedException(Messages.ResourceDisposed), Messages.ResourceDisposed);

            if (_context.ContainsKey(id))
                _context.Remove(id);
        }

        /// <summary>
        /// Obtiene el valor del contexto a partir de su id
        /// </summary>
        /// <param name="id">Identificador del valor</param>
        /// <returns>Valor obtenido</returns>
        public object GetFromContext(string id)
        {
            if (this._isDisposed)
                throw new TraceException(new ObjectDisposedException(Messages.ResourceDisposed), Messages.ResourceDisposed);

            object retorno = null;
            if (_context.ContainsKey(id))
                retorno = _context[id];

            return retorno;
        }

        /// <summary>
        /// Inicializa el contexto de la operación lógica
        /// </summary>
        /// <param name="operation">Nombre de la operación lógica que se inicia</param>
        /// <param name="customTraceSource">custom trace source opcional para utilizar para loguear la excepcion. Este valor puede ser null</param>
        private void Initialize(string operation, TraceSource customTraceSource, string callerMethodName)
        {
            if (this._isDisposed)
                throw new TraceException(new ObjectDisposedException(Messages.ResourceDisposed), Messages.ResourceDisposed);

            this._context = new Dictionary<string, object>();

            if (Trace.CorrelationManager.ActivityId == Guid.Empty)
                Trace.CorrelationManager.ActivityId = Guid.NewGuid();

            if (!String.IsNullOrEmpty(operation))
            {
                this._isLogicalOperationStart = true;
                Trace.CorrelationManager.StartLogicalOperation(operation);
            }

            // Obtengo todos los datos de ambiente en la inicialización, 
            // ya que en general resulta una operación costosa.
            _defaultTraceRecord = TraceRecord.Default;
            
            if (!String.IsNullOrEmpty(callerMethodName))
            {
                _defaultTraceRecord.CallerMethod = callerMethodName;
            }

            if (customTraceSource != null)
                this._traceSource = customTraceSource;
            else
                this._traceSource = new TraceSource(Defaults.DefaultTraceSource, SourceLevels.All);

            if (!this.IsLightweightEnabled)
            {
                if (String.IsNullOrEmpty(_defaultTraceRecord.CallerMethod))
                {
                    _defaultTraceRecord.CallerMethod = GetCallerMethodName(Tracer.initializeMethodFramesQuantity);
                }

                TraceRecord trace = _defaultTraceRecord.Clone();
                trace.DateTime = DateTime.Now;
                trace.UtcDateTime = DateTime.UtcNow;
                trace.Timestamp = DateTime.UtcNow.Ticks;
                trace.Level = TraceEventType.Start.ToString();
                trace.Message = "Inicio de actividad.";
                trace.Source = _traceSource.Name;

                this._traceSource.TraceData(TraceEventType.Start, (int)EventType.BeginMethod, TraceRecordToText(trace));
            }
        }

        /// <summary>
        /// Obtiene el nombre del método que lo invocó según <see cref="System.Diagnostics.StackFrame"/> y 
        /// <paramref name="framesQty"/>
        /// </summary>
        /// <param name="framesQty">Cantidad de frames en el stack que hay que saltearse para 
        /// obtener el frame del método llamador</param>
        /// <returns>Nombre del método buscado o nulo el modo liviano está activo</returns>
        private string GetCallerMethodName(int framesQty)
        {
            if (this._isDisposed)
                throw new TraceException(new ObjectDisposedException(Messages.ResourceDisposed), Messages.ResourceDisposed);

            MethodBase mb = new StackFrame(framesQty + 1, true).GetMethod();
            return mb.ReflectedType.FullName + "." + mb.Name;

        }

        /// <summary>
        /// Convierte a texto los datos del contexto
        /// </summary>
        /// <returns></returns>
        private string ContextToText()
        {
            StringBuilder sb = new StringBuilder();

            foreach (var pair in _context)
                sb.AppendLine(String.Format("[{0}] = {1}.", pair.Key, pair.Value.ToString()));

            return sb.ToString();
        }

        /// <summary>
        /// Recupera el objeto de traza configurado 
        /// </summary>
        /// <param name="level">Nivel de traza</param>
        /// <param name="message">Mensaje</param>
        /// <param name="args">Argumentos para formatear el mensaje</param>
        /// <returns></returns>
        private TraceRecord GetTraceRecord(TraceEventType level, string message, object[] args)
        {
            TraceRecord trace = this._defaultTraceRecord.Clone();
            trace.DateTime = DateTime.Now;
            trace.UtcDateTime = DateTime.UtcNow;
            trace.Timestamp = DateTime.UtcNow.Ticks;
            trace.Level = level.ToString();

            trace.Message = message;
            trace.Source = this._traceSource.Name;
            return trace;
        }
        
        /// <summary>
        /// Convierte a texto la clase TraceRecord
        /// </summary>
        /// <returns></returns>
        private string TraceRecordToText(TraceRecord trace)
        {
            if (IsLightweightEnabled)
                return trace.ToShortString();

            return trace.ToString();
        }

        private bool GetBoolAppSetting(string key)
        {
            bool boolValue = false;
            string value = ConfigurationManager.AppSettings[key];

            if (!String.IsNullOrWhiteSpace(value))
                Boolean.TryParse(value, out boolValue);

            return boolValue;
        }


        #endregion
    }
}
