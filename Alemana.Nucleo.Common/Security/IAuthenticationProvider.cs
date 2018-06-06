namespace Alemana.Nucleo.Common.Security
{
    /// <summary>
    /// Interface de los claims, se utiliza para obtener los datos del usuario, como
    /// para otros datos, Ej: para el Login
    /// </summary>
    public interface IAuthenticationProvider
    {
        /// <summary>
        /// Método que obtiene los Claims del Usuario
        /// </summary>
        /// <param name="inputClaims">Claims existentes o custom claims</param>
        /// <returns>Retorna los claims del usuario/identidad</returns>
        ClaimDictionary Authenticate(ClaimDictionary inputClaims);
    }
}
