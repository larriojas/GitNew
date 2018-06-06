namespace Alemana.Nucleo.Common.Caching
{
    /// <summary>
    /// Tipo de Cache
    /// </summary>
    public enum CacheType
    {
        /// <summary>
        /// No se genera Cache
        /// </summary>
        NoCache,
        /// <summary>
        /// Cache de System.Runtime.Caching
        /// </summary>
        LocalCache,

        FileCache,

        BinaryCache
    }
    
}
