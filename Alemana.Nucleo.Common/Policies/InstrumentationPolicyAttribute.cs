using Microsoft.Practices.Unity.InterceptionExtension;

namespace Alemana.Nucleo.Common.Policies
{
    /// <summary>
    /// Representa un atributo para poder inyectar políticas de contadores de performance en los métodos
    /// Autor: Lagash S.A.
    /// Fecha de creación: 29/04/2013
    /// Fecha de modificación: 29/04/2013
    /// </summary>
    public class InstrumentationPolicyAttribute : HandlerAttribute
    {
        #region fields

        private bool _successfulCalls = false;
        private bool _executingCalls = false;
        private bool _callsPerSecond = false;
        private bool _averageCallTime = false;
        private bool _averageBaseCallTime = false;
        private bool _exceptionCalls = false;
        private bool _exceptionPerSecond = false;
        private string _instanceName = "Default";

        #endregion

        #region properties

        public bool SuccessfulCalls
        {
            get { return _successfulCalls; }
            set { _successfulCalls = value; }
        }
        public bool ExecutingCalls
        {
            get { return _executingCalls; }
            set { _executingCalls = value; }
        }
        public bool CallsPerSecond
        {
            get { return _callsPerSecond; }
            set { _callsPerSecond = value; }
        }
        public bool AverageCallTime
        {
            get { return _averageCallTime; }
            set { _averageCallTime = value; }
        }
        public bool AverageBaseCallTime
        {
            get { return _averageBaseCallTime; }
            set { _averageBaseCallTime = value; }
        }
        public bool ExceptionCalls
        {
          get { return _exceptionCalls; }
          set { _exceptionCalls = value; }
        }
        public bool ExceptionPerSecond
        {
            get { return _exceptionPerSecond; }
            set { _exceptionPerSecond = value; }
        }
        public string InstanceName
        {
            get { return _instanceName; }
            set { _instanceName = value; }
        }

        #endregion

        #region Constructores

        public InstrumentationPolicyAttribute()
        {
        }
        
        public InstrumentationPolicyAttribute(string instanceName)
        {
            _instanceName = instanceName;

            // Si no se especifica ningun contador se asume que 
            // se quiere setear todos los contadores
            if (!_averageBaseCallTime &&
                !_averageCallTime &&
                !_callsPerSecond &&
                !_exceptionCalls &&
                !_exceptionPerSecond &&
                !_executingCalls &&
                !_successfulCalls)
            {
                _averageBaseCallTime = true;
                _averageCallTime  = true;
                _callsPerSecond  = true;
                _exceptionCalls  = true;
                _exceptionPerSecond  = true;
                _executingCalls  = true;
                _successfulCalls = true;
            }

        }

        #endregion

        #region HandlerAttribute overrides

        /// <summary>
        /// Crea un handler para la instrumentación
        /// </summary>
        /// <returns>Un nuevo objeto call handler.</returns>
        public override ICallHandler CreateHandler(Microsoft.Practices.Unity.IUnityContainer container)
        {
            CounterSet set = new CounterSet()
            {
                AverageBaseCallTime = this.AverageBaseCallTime,
                AverageCallTime = this.AverageCallTime,
                CallsPerSecond = this.CallsPerSecond,
                ExceptionCalls = this.ExceptionCalls,
                ExceptionPerSecond = this.ExceptionPerSecond,
                ExecutingCalls = this.ExecutingCalls,
                SuccessfulCalls = this.SuccessfulCalls
            };

            return new Handlers.InstrumentationHandler(_instanceName, set);
        }

        #endregion
    }

    /// <summary>
    /// Clase para establecer los contadores de performance a incluir en la instrumentación
    /// Autor: Lagash S.A.
    /// Fecha de creación: 04/10/2010
    /// Fecha de modificación: 04/10/2010
    /// </summary>
    public class CounterSet
    {
        #region fields
        
        private bool _successfulCalls = false;
        private bool _executingCalls = false;
        private bool _callsPerSecond = false;
        private bool _averageCallTime = false;
        private bool _averageBaseCallTime = false;
        private bool _exceptionCalls = false;
        private bool _exceptionPerSecond = false;

        #endregion

        #region properties

        public bool SuccessfulCalls
        {
            get { return _successfulCalls; }
            set { _successfulCalls = value; }
        }
        public bool ExecutingCalls
        {
            get { return _executingCalls; }
            set { _executingCalls = value; }
        }
        public bool CallsPerSecond
        {
            get { return _callsPerSecond; }
            set { _callsPerSecond = value; }
        }
        public bool AverageCallTime
        {
            get { return _averageCallTime; }
            set { _averageCallTime = value; }
        }
        public bool AverageBaseCallTime
        {
            get { return _averageBaseCallTime; }
            set { _averageBaseCallTime = value; }
        }
        public bool ExceptionCalls
        {
            get { return _exceptionCalls; }
            set { _exceptionCalls = value; }
        }
        public bool ExceptionPerSecond
        {
            get { return _exceptionPerSecond; }
            set { _exceptionPerSecond = value; }
        }

        #endregion

    }

 }
