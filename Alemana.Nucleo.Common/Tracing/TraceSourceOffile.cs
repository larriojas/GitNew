using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Alemana.Nucleo.Common.Tracing
{
    public class TraceSourceOffile
    {
        public static TraceSource GetTraceSource()
        {
            return new TraceSource(Defaults.DefaultOfflineTraceSource, SourceLevels.All);
        }

        public static TraceSource GetCacheTraceSource()
        {
            return new TraceSource(Defaults.DefaultCacheOfflineTraceSource, SourceLevels.All);
        }
    }
}
