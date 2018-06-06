using System;

namespace Alemana.Nucleo.Common.Tracing
{
    public static class Logger
    {
        private static Tracer _tracer;

        static Logger()
        {
            Init();
        }

        private static void Init()
        {
            _tracer = new Tracer();
        }

        public static void Verbose(string message, params object[] args)
        {
            _tracer.TraceVerbose(message, args);
        }

        public static void Information(string message, params object[] args)
        {
            _tracer.TraceInformation(message, args);
        }

        public static void Warning(string message, params object[] args)
        {
            _tracer.TraceWarning(message, args);
        }

        public static void Flush()
        {
            _tracer.TraceSource.Flush();
        }

        public static void Error(string message, params object[] args)
        {
            _tracer.TraceError(message, args);
        }

        public static void Error(Exception ex, string message, params object[] args)
        {
            _tracer.TraceError(message, args);
            _tracer.TraceError(ex.ToString(), args);
        }

        public static void Error(Exception ex)
        {
            _tracer.TraceError(ex.ToString());
        }
    }
}
