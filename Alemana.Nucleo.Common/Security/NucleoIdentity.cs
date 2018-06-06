using System;
using System.Collections.Generic;
using System.Security.Principal;

namespace Alemana.Nucleo.Common.Security
{
    /// <summary>
    /// Clase que representa la identidad del Usuario
    /// Autor: Lagash S.A.
    /// Fecha de creación: 29/04/2013
    /// </summary>
    public class NucleoIdentity : IIdentity
    {
        private string _offlineUsername;
        private decimal _offlineSessionId;
		private string _offlinerut;

        public NucleoIdentity(WindowsIdentity identity)
        {
            Dictionary<string, object> claims = new Dictionary<string, object>();
            claims.Add(ClaimKeys.UserName, identity.Name);
            
            if (identity.IsAuthenticated)
                claims.Add(ClaimKeys.AuthenticationStatus, AuthenticationStatus.OK);
            else
                claims.Add(ClaimKeys.AuthenticationStatus, AuthenticationStatus.NotEspecified);

            Claims = new ClaimDictionary(claims);
            Nucleo.Shared.DataHolder.SetValue(Nucleo.Common.Tracing.ClaimNames.SessionID.ToString(),(object) (string.IsNullOrEmpty(SessionId) ? -1M : decimal.Parse(SessionId)));
        }

        /// <summary>
        /// Constructor interno de la clase
        /// </summary>
        /// <param name="claimDictionary">Claims del usuario</param>
        internal NucleoIdentity(ClaimDictionary claimDictionary)
        {
            Claims = claimDictionary;

            Nucleo.Shared.DataHolder.SetValue(Nucleo.Common.Tracing.ClaimNames.SessionID.ToString(), (object)(string.IsNullOrEmpty(SessionId) ? -1M : decimal.Parse(SessionId)));
        }

        /// <summary>
        /// Propiedad con los Claims del usuario
        /// </summary>
        public ClaimDictionary Claims { get; private set; }

        
        /// <summary>
        /// Propiedad con el tipo de autenticación 
        /// </summary>
        public string AuthenticationType
        {
            get
            {
                return "NucleoIdentity";
            }
        }

        /// <summary>
        /// Propiedad si el usuario debe cambiar contraseña
        /// </summary>
        public bool MustChangePassword
        {
            get
            {
                if (!Claims.ContainsKey(ClaimKeys.MustChangePassword))
                    return false;

                return (bool) Claims[ClaimKeys.MustChangePassword];
            }
        }

        /// <summary>
        /// Propiedad que identifica si el usuario se encuentra Autenticado
        /// </summary>
        public bool IsAuthenticated 
        { 
            get
            {
                if (!Claims.ContainsKey(ClaimKeys.AuthenticationStatus))
                    return false;
                
                var status = ((AuthenticationStatus)Enum.Parse(typeof(AuthenticationStatus),
                                Claims[ClaimKeys.AuthenticationStatus].ToString()));

                return status == AuthenticationStatus.OK;
            }
        }
 
        /// <summary>
        /// Propiedad que obtiene el nombre de la cuenta de usuario (UserName)
        /// </summary>
        public string Name 
        {
            get
            {
                return !Claims.ContainsKey(ClaimKeys.UserName) ? 
                    String.Empty : Claims[ClaimKeys.UserName].ToString();
            }
        }

        /// <summary>
        /// Propiedad que obtiene el nombre del usuario (FirstName)
        /// </summary>
        public string FirstName
        {
            get
            {
                return !Claims.ContainsKey(ClaimKeys.FirstName) ?
                    String.Empty : Claims[ClaimKeys.FirstName].ToString();
            }
        }

        /// <summary>
        /// Propiedad que obtiene el apellido paterno (FathersName)
        /// </summary>
        public string FathersName
        {
            get
            {
                return !Claims.ContainsKey(ClaimKeys.FathersName) ?
                    String.Empty : Claims[ClaimKeys.FathersName].ToString();
            }
        }

        /// <summary>
        /// Propiedad que obtiene el apellido materno (MothersName)
        /// </summary>
        public string MothersName
        {
            get
            {
                return !Claims.ContainsKey(ClaimKeys.MothersName) ?
                    String.Empty : Claims[ClaimKeys.MothersName].ToString();
            }
        }

        /// <summary>
        /// Propiedad que obtiene el Rut del usuario
        /// </summary>
        public string Rut
        {
            get
            {
                return !Claims.ContainsKey(ClaimKeys.Rut) ?
                    String.Empty : Claims[ClaimKeys.Rut].ToString();
            }
        }

        
        /// <summary>
        /// Propiedad que obtiene el SessionId
        /// </summary>
        public string SessionId
        {
            get
            {
                return !Claims.ContainsKey(ClaimKeys.SessionId) ?
                    String.Empty : Claims[ClaimKeys.SessionId].ToString();
            }
            set
            {
                Claims[ClaimKeys.SessionId] = value;
            }
        }

        /// <summary>
        /// Propiedad que obtiene el SessionId
        /// </summary>
        public string Alias
        {
            get
            {
                return !Claims.ContainsKey(ClaimKeys.Alias) ?
                    String.Empty : Claims[ClaimKeys.Alias].ToString();
            }
        }

        /// <summary>
        /// Propiedad que obtiene el UsuarioId
        /// </summary>
        public string UsuarioId
        {
            get
            {
                return !Claims.ContainsKey(ClaimKeys.UsuarioId) ?
                    String.Empty : Claims[ClaimKeys.UsuarioId].ToString();
            }
        }

        /// <summary>
        /// Propiedad que obtiene el Mac 
        /// </summary>
        public string Mac
        {
            get
            {
                return !Claims.ContainsKey(ClaimKeys.Mac) ?
                    String.Empty : Claims[ClaimKeys.Mac].ToString();
            }
        }

        /// <summary>
        /// Propiedad que obtiene el Ip 
        /// </summary>
        public string Ip
        {
            get
            {
                return !Claims.ContainsKey(ClaimKeys.Ip) ?
                    String.Empty : Claims[ClaimKeys.Ip].ToString();
            }
        }

        /// <summary>
        /// Propiedad que obtiene el Version nucleo
        /// </summary>
        public string VersionNuc
        {
            get
            {
                return !Claims.ContainsKey(ClaimKeys.VersionNuc) ?
                    String.Empty : Claims[ClaimKeys.VersionNuc].ToString();
            }
        }

        /// <summary>
        /// Propiedad que obtiene / setea el rut
        /// </summary>
        public string OfflineRut
        {
            get
            {
                return _offlinerut;
            }
            set
            {
                _offlinerut = value;
            }
        }

        /// <summary>
        /// Propiedad que obtiene / setea nombre offline
        /// </summary>
        public string OfflineUsername
        {
            get
            {
                return _offlineUsername;
            }
            set
            {
                _offlineUsername = value;

            }
        }

		/// <summary>
		/// Propiedad que obtiene / setea la sessionId offline
		/// </summary>
		public decimal OfflineSessionId
		{
			get
			{
				return _offlineSessionId;
			}
			set
			{
				_offlineSessionId = value;

			}
		}

	}

}
