using System.Collections.Generic;

namespace Alemana.Nucleo.Common.Security.Providers
{
    public class Suser2AuthorizationProvider : IAuthorizationProvider
    {
        public IEnumerable<string> GetPermissions(string identityName)
        {
            return new List<string>(){ "Administrativo", "Clínico", "Administrador" };
        }

    }
}
