using System.Collections.Generic;

namespace Alemana.Nucleo.Common.Security
{
    public interface IAuthorizationProvider
    {
        IEnumerable<string> GetPermissions(string identity);
    }

}
