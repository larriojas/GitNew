using Microsoft.Practices.Unity.InterceptionExtension;
using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Diagnostics;

namespace Alemana.Nucleo.Common.Policies.Handlers
{
    /// <summary>
    /// Implementación del ICallHandler para la politica de instrumentación de invocaciones a métodos.
    /// Autor: Lagash S.A.
    /// Fecha de creación: 29/04/2013
    /// Fecha de modificación: 29/04/2013
    /// </summary>
    /// <remarks>
    /// Esta clase realiza la instrumentación de un método de una clase a la que se le haya aplicado
    /// la política de instrumentación a través del atributo <see cref="InstrumentationPolicyAttribute"/>.
    /// </remarks>
    public class InstrumentationHandler : ICallHandler
    {
        #region Constants
        // Performance Counters constants
        private const string perfCountersCategory = "Alemana.Nucleo.Common";
        private const string successfulCalls = "# Ejecuciones exitosas";
        private const string averageBaseCallTime = "Tiempo promedio base";
        private const string averageCallTime = "Tiempo promedio de ejecución";
        private const string executingCalls = "# Ejecuciones en curso";
        private const string callsPerSecond = "# Ejecuciones / sec";
        private const string exceptionCalls = "# Ejecuciones con Errores";
        private const string exceptionPerSecond = "# Errores / sec";
        #endregion

        #region Private Fields
        /// <summary>
        /// Especificación de los contadores a utilizar por le handler.
        /// </summary>
        private CounterSet _counterSet;

        /// <summary>
        /// Nombre de la instancia.
        /// </summary>
        private string _instanceName;

        /// <summary>
        /// Diccionario con los contadores de performance
        /// </summary>
        private Dictionary<string, PerformanceCounter> _perfCounters = new Dictionary<string, PerformanceCounter>();

        /// <summary>
        /// Diccionar de contadores para reutilización
        /// </summary>
        private static ConcurrentDictionary<string, Dictionary<string, PerformanceCounter>> _perfCounterInstances = new ConcurrentDictionary<string, Dictionary<string, PerformanceCounter>>();

        /// <summary>
        /// Flag que especifica si la instrumentación se encuentra disponible
        /// </summary>
        private bool _countersAreAvailable = false;

        #endregion

        #region .ctor

        /// <summary>
        /// Constructor de la clase
        /// </summary>
        /// <param name="instanceName">Nombre de la instancia de los contadores de performance.</param>
        /// <param name="counterSet">Especificación de los contadores a utilizar.</param>
        public InstrumentationHandler(string instanceName, CounterSet counterSet)
        {
            // Si no existe la categoría es porque no están instalados los contadores.
            _counterSet = counterSet;
            _instanceName = instanceName;

            if (!PerformanceCounterCategory.Exists(perfCountersCategory))
                return;

            _perfCounters = _perfCounterInstances.GetOrAdd(instanceName, GetInstanceCounters);
            _countersAreAvailable = true;

        }
        #endregion

        #region Private Methods
        /// <summary>
        /// Inicializa o recupera los contadores para una instancia especifica
        /// </summary>
        /// <param name="instanceName">Nombre de la instancia</param>
        public static Dictionary<string, PerformanceCounter> GetInstanceCounters(string instanceName)
        {
            Dictionary<string, PerformanceCounter> counters = new Dictionary<string, PerformanceCounter>();
            if (PerformanceCounterCategory.CounterExists(successfulCalls, perfCountersCategory))
                counters.Add(successfulCalls, new PerformanceCounter(perfCountersCategory, successfulCalls, instanceName, false));

            if (PerformanceCounterCategory.CounterExists(averageBaseCallTime, perfCountersCategory))
                counters.Add(averageBaseCallTime, new PerformanceCounter(perfCountersCategory, averageBaseCallTime, instanceName, false));

            if (PerformanceCounterCategory.CounterExists(averageCallTime, perfCountersCategory))
                counters.Add(averageCallTime, new PerformanceCounter(perfCountersCategory, averageCallTime, instanceName, false));

            if (PerformanceCounterCategory.CounterExists(executingCalls, perfCountersCategory))
                counters.Add(executingCalls, new PerformanceCounter(perfCountersCategory, executingCalls, instanceName, false));

            if (PerformanceCounterCategory.CounterExists(callsPerSecond, perfCountersCategory))
                counters.Add(callsPerSecond, new PerformanceCounter(perfCountersCategory, callsPerSecond, instanceName, false));

            if (PerformanceCounterCategory.CounterExists(exceptionCalls, perfCountersCategory))
                counters.Add(exceptionCalls, new PerformanceCounter(perfCountersCategory, exceptionCalls, instanceName, false));

            if (PerformanceCounterCategory.CounterExists(exceptionPerSecond, perfCountersCategory))
                counters.Add(exceptionPerSecond, new PerformanceCounter(perfCountersCategory, exceptionPerSecond, instanceName, false));

            return counters;
        }


        /// <summary>
        /// Incremento el contador
        /// </summary>
        /// <param name="counter"></param>
        private void IncreaseCounter(string counter)
        {
            // Verifico que los contadores se encuentren disponibles.
            // (podrían no estarlo si no existe la categoría)
            if (_countersAreAvailable)
            {
                // Si el contador existe, lo incremento.
                if (_perfCounters.ContainsKey(counter))
                    _perfCounters[counter].IncrementBy(1);
            }
        }

        /// <summary>
        /// Decremento el contador
        /// </summary>
        /// <param name="counter"></param>
        private void DecreaseCounter(string counter)
        {
            // Verifico que los contadores se encuentren disponibles.
            // (podrían no estarlo si no existe la categoría)
            if (_countersAreAvailable)
            {
                // Si el contador existe, lo incremento.
                if (_perfCounters.ContainsKey(counter))
                    _perfCounters[counter].Decrement();
            }
        }

        /// <summary>
        /// Registro tiempo en el contador
        /// </summary>
        /// <param name="counter"></param>
        /// <param name="startTime"></param>
        private void RegisterCounterTime(string counter, DateTime startTime)
        {
            // Verifico que los contadores se encuentren disponibles.
            // (podrían no estarlo si no existe la categoría)
            if (_countersAreAvailable)
            {
                // Si el contador existe, lo incremento.
                if (_perfCounters.ContainsKey(counter))
                    _perfCounters[counter].IncrementBy(DateTime.UtcNow.Ticks - startTime.Ticks);
            }
        }


        #endregion

        #region ICallHandler Members

        /// <summary>
        /// Order en el que debe ejecutarse el Handler
        /// </summary>
        public int Order
        {
            get;
            set;
        }

        /// <summary>
        /// Implementación de la invocación del handler. Realiza los incrementos, registros y decrementos en los contadores
        /// especificados en el constructor a través de la clase <see cref="CounterSet"/>
        /// </summary>
        /// <param name="input">Datos de la invocación</param>
        /// <param name="getNext">Delegado al próximo eslabón de la cadena de responsabilidad</param>
        /// <returns>Resultado de la invocación</returns>
        public IMethodReturn Invoke(IMethodInvocation input, GetNextHandlerDelegate getNext)
        {
            DateTime startTime = DateTime.UtcNow;

            // Incremento el contador de ejecuciones en curso
            if (_counterSet.ExecutingCalls)
            {
                IncreaseCounter(executingCalls);
            }

            // Incremento el contador de llamadas por segundo
            if (_counterSet.CallsPerSecond)
            {
                IncreaseCounter(callsPerSecond);
            }

            // Ejecuto la siguiente policy o target method
            IMethodReturn ret = getNext()(input, getNext);

            // Registro el tiempo de llamada
            if (_counterSet.AverageCallTime)
            {
                RegisterCounterTime(averageCallTime, startTime);
            }

            // Registro el tiempo base de llamada
            if (_counterSet.AverageBaseCallTime)
            {
                RegisterCounterTime(averageBaseCallTime, startTime);
            }

            // Decremento el contador de ejecuciones en curso
            if (_counterSet.ExceptionCalls)
            {
                DecreaseCounter(executingCalls);
            }

            // Si la llamada no retornó excepciones, incremento el contador de llamadas exitosas
            if (ret.Exception == null)
            {
                if (_counterSet.SuccessfulCalls)
                {
                    IncreaseCounter(successfulCalls);
                }
            }
            else // Incremento el contador de excepciones y excepciones por segundo
            {
                if (_counterSet.ExceptionCalls)
                {
                    IncreaseCounter(exceptionCalls);
                }
                if (_counterSet.ExceptionPerSecond)
                {
                    IncreaseCounter(exceptionPerSecond);
                }
            }

            // Devuelvo el resultado
            return ret;
        }

        #endregion
    }
}
