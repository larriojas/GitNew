namespace Alemana.Nucleo.Common.Security
{
    /// <summary>
    /// Enumerador con los posibles estados de una validación de usuario
    /// </summary>
    public enum AuthenticationStatus
    {
        OK,
        BadPassword,
        UserDoesNotExists,
        AccountExpired,
        AccountLocked,
        SessionExpired,
        AccountLockedByFailedRetries,
        FailedAuthentication,
        IncompleteData,
        AccountInProcess,
        UnexpectedError,
        NotEspecified
    }
}
