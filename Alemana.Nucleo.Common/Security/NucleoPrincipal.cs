using Alemana.Nucleo.Common.Extensions;
using Alemana.Nucleo.Common.Utility;
using System;
using System.Collections.Generic;
using System.Security.Principal;

namespace Alemana.Nucleo.Common.Security
{
    /// <summary>
    /// Clase que representa el Principal del Usuario
    /// Autor: Lagash S.A.
    /// Fecha de creación: 29/04/2013
    /// </summary>
    public class NucleoPrincipal : IPrincipal
    {
        /// <summary>
        /// Constructor con el ticket de la cookie
        /// </summary>
        /// <param name="ticket">ticket de la cookie encriptada</param>
        public NucleoPrincipal(string ticket)
        {
            var principalClaims = CryptoHelper.Decrypt(ticket).FromJSON<Dictionary<string, object>>();
            
            SessionStartTime = DateTime.Parse(principalClaims["SessionStartTime"].ToString());
            LastAccessTime = DateTime.UtcNow;
            principalClaims["LastAccessTime"] = LastAccessTime;
           
            Ticket = CryptoHelper.Encrypt(principalClaims.ToString());
            
            principalClaims.Remove("SessionStartTime");
            principalClaims.Remove("LastAccessTime");
            NucleoIdentity = new NucleoIdentity(new ClaimDictionary(principalClaims));
        }

      
        /// <summary>
        /// Constructor con la identidad del usuario
        /// </summary>
        /// <param name="identity">Identidad del Usuario</param>
        public NucleoPrincipal(NucleoIdentity identity)
        {
            NucleoIdentity = identity;
            var principalClaims = new Dictionary<string, object>(identity.Claims);
            principalClaims["SessionStartTime"] = DateTime.UtcNow;
            principalClaims["LastAccessTime"] = DateTime.Parse(principalClaims["SessionStartTime"].ToString()); 
            Ticket = CryptoHelper.Encrypt(principalClaims.ToJSON());
        }

        /// <summary>
        /// Propiedad con el ticket encriptado
        /// </summary>
        public string Ticket { get; private set; }

        /// <summary>
        /// Propiedad con el Identity del Usuario
        /// </summary>
        public NucleoIdentity NucleoIdentity { get; private set; }

        /// <summary>
        /// Propiedad con la fecha de sesion del usuraio
        /// </summary>
        public DateTime SessionStartTime { get; private set; }

        /// <summary>
        /// Propiedad con la fecha del último acceso del usuario
        /// </summary>
        public DateTime LastAccessTime { get; private set; }

        public string CypherKey
        {
            get
            {
                return this.NucleoIdentity.Claims[ClaimKeys.CypherKey] as string;
            }
        }


        public int SessionTimeoutSeconds 
        {
            get
            {
                if (!NucleoIdentity.Claims.ContainsKey(ClaimKeys.SessionTimeoutSeconds))
                    return Defaults.DefaultSessionTimeout; // 5 minutos

                return (int)NucleoIdentity.Claims[ClaimKeys.SessionTimeoutSeconds];
            }
        }

        /// <summary>
        /// Identidad del Usuario
        /// </summary>
        public IIdentity Identity
        {
            get { return NucleoIdentity; }
        }

        /// <summary>
        /// Método que obtiene si se encuentra en el Role
        /// </summary>
        /// <param name="role">Nombre del role</param>
        /// <returns>Retorna un bool si el role es el mismo</returns>
        public bool IsInRole(string role)
        {
            return SecurityManager.IsUserInRole(this.NucleoIdentity, role);
        }


    }
}
