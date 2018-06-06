namespace Alemana.Nucleo.Common.Tracing
{
    /// <summary>
    /// Tipo de evento procesado por el Tracer
    /// Autor: Lagash S.A.
    /// Fecha de creación: 29/04/2013
    /// Fecha de modificación: 29/04/2013
    /// </summary>
    internal enum EventType
    {
        /// <summary>
        /// Comienzo de método
        /// </summary>
        BeginMethod,
        /// <summary>
        /// Fin de método
        /// </summary>
        EndMethod,
        /// <summary>
        /// Escritura de valores de contexto
        /// </summary>
        ContextValues,
        /// <summary>
        /// Escritura de un <see cref="TraceData"/>
        /// </summary>
        TraceWrite,
    }


    public enum ClaimEventType
    {
        ModuleUsageStartAuditing,
        ModuleUsageEndAuditing
    }

    public enum ClaimNames
    {
        ModuleName,
        SessionID,
        FichaID,
        EpisodioID,
        EncuentroID

    }
}