using System;

namespace Alemana.Nucleo.Common.Security
{
    /// <summary>
    /// Interface que se utiliza para realizar el cambio de contraseña del usuario,
    /// verifica si la contraseña ya existe
    /// </summary>
    public interface IPasswordProvider
    {
        /// <summary>
        /// Realiza el cambio de contraseña de un usuario sin solicitar la contraseña anterior 
        /// </summary> 
        /// <param name="identity">Identidad autenticada del usuario</param>
        /// <param name="password">Nueva contraseña a cambiar</param>
        /// <returns>ChangePasswordResult con el resultado del proceso</returns>
        ChangePasswordResult Change(NucleoIdentity identity, string password);

    }

    [Serializable]
    public enum ChangePasswordResult
    {
        Ok = 0,
        OldPassword = 1,
        AccountDoesNotExists = 2
    }
}
