using Alemana.Nucleo.Common.Exceptions;
using Alemana.Nucleo.Common.Tracing;
using Alemana.Nucleo.Common.wsSession;
using Alemana.Nucleo.Common.WsSuser2Roles;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Deployment.Application;
using System.DirectoryServices.AccountManagement;
using System.Linq;

namespace Alemana.Nucleo.Common.Security.Providers
{
    public class ActiveDirectorySecurityProvider : IPasswordProvider, IAuthenticationProvider
    {
        public ActiveDirectorySecurityProvider()
        {
            Initialize();
        }

        private short LDAPServerRetryCount = 3;

        public void Initialize()
        {
            SwitchLDAPServer();

            _isInitialized = true;
        }

        public void SwitchLDAPServer()
        {
            if (LDAPServerRetryCount == 0)
                throw new System.DirectoryServices.AccountManagement.PrincipalServerDownException("Se han excedido los reintentos para ingresar al proveedor de seguridad de Active Directory");

            var connString = ConfigurationManager.AppSettings[Defaults.ADConnectionStringSetting + LDAPServerRetryCount.ToString()];

            if (String.IsNullOrWhiteSpace(connString))
                throw new NucleoCommonException("Ha ocurrido un error durante la inicialización del proveedor de seguridad de Active Directory: el string de conexión a AD es inválido. Verifique que exista la entrada [{0}] en el archivo de configuración y esta sea válida.", Defaults.ADConnectionStringSetting + LDAPServerRetryCount.ToString());

            Uri uri;
            if (!Uri.TryCreate(connString, UriKind.Absolute, out uri))
                throw new NucleoCommonException("Ha ocurrido un error durante la inicialización del proveedor de seguridad de Active Directory: el string de conexión a AD es inválido. Verifique que exista la entrada [{0}] en el archivo de configuración y esta sea válida.", Defaults.ADConnectionStringSetting + LDAPServerRetryCount.ToString());

            ActiveDirectoryUri = uri;

            LDAPServerRetryCount--;
        }

        private bool _isInitialized = false;

        public ClaimDictionary Authenticate(ClaimDictionary inputClaims)
        {
            ClaimDictionary claims;
            try
            {
                claims = AuthenticateExecute(inputClaims);
            }
            catch (System.DirectoryServices.AccountManagement.PrincipalServerDownException)
            {
                SwitchLDAPServer();
                claims = Authenticate(inputClaims);
            }
            catch (Exception)
            {
                throw;
            }

            return claims;
        }

        public ClaimDictionary AuthenticateExecute(ClaimDictionary inputClaims)
        {
            if (!_isInitialized)
                throw new NucleoCommonException("El proveedor de seguridad de Active Directory no se encuentra inicializado. Posibles causas: No se encuentra configurado correctamente el string de conexión a AD.");

            // Traspaso los claims al diccionario de salida.
            Dictionary<string, object> outputClaims = new Dictionary<string, object>();
            foreach (var claim in inputClaims)
            {
                if (claim.Key == ClaimKeys.Password)
                    continue;

                outputClaims.Add(claim.Key, claim.Value);
            }

            // Verifico si el nombre de usuario es válido.
            var userName = inputClaims["UserName"] as string;
            if (String.IsNullOrWhiteSpace(userName))
            {
                Logger.Error("El nombre de usuario recibido es nulo o inválido. Verifique elemento \"UserName\" en el diccionario de evidencias enviadas al proveedor de seguridad de Active Directory.");
                outputClaims.Add(ClaimKeys.AuthenticationStatus, AuthenticationStatus.IncompleteData);
                return new ClaimDictionary(outputClaims);
            }


            // Verifico si la contraseña es válida
            var password = inputClaims["Password"] as string;
            if (String.IsNullOrEmpty(password))
            {
                Logger.Error("La contraseña recibida para autenticar al usuario [{0}] es nula. Verifique elemento \"Password\" en el diccionario de evidencias enviadas al proveedor de seguridad de active directory.", userName);
                outputClaims.Add(ClaimKeys.AuthenticationStatus, AuthenticationStatus.IncompleteData);
                return new ClaimDictionary(outputClaims);
            }


            // Obtengo el contexto de seguridad para AD-DS
            using (var context = new PrincipalContext(ContextType.Domain,
                        ActiveDirectoryUri.Authority,
                        ActiveDirectoryUri.AbsolutePath.Substring(1, ActiveDirectoryUri.AbsolutePath.Length - 1),
                        ContextOptions.Negotiate))
            {
                // Vaidación de credenciales en AD
                if (!context.ValidateCredentials(userName, password))
                {
                    outputClaims.Add(ClaimKeys.AuthenticationStatus, AuthenticationStatus.BadPassword);
                    return new ClaimDictionary(outputClaims);
                }

                // Obtengo datos del usuario
                using (var userPrincipal = UserPrincipal.FindByIdentity(context, IdentityType.SamAccountName, userName))
                {
                    // Usuario no existe
                    if (userPrincipal == null)
                    {
                        Logger.Verbose("No se encontró la identidad [{0}] en Active Directory-", userName);
                        outputClaims.Add(ClaimKeys.AuthenticationStatus, AuthenticationStatus.UserDoesNotExists);
                        return new ClaimDictionary(outputClaims);
                    }


                    outputClaims.Add(ClaimKeys.AuthenticationStatus, AuthenticationStatus.OK);

                    // Verifico cuenta bloqueada
                    if (userPrincipal.IsAccountLockedOut())
                        outputClaims[ClaimKeys.AuthenticationStatus] = AuthenticationStatus.AccountLocked;

                    // Verifico cuenta expirada
                    if (userPrincipal.AccountExpirationDate.HasValue)
                        if (userPrincipal.AccountExpirationDate.Value < DateTime.Now)
                            outputClaims[ClaimKeys.AuthenticationStatus] = AuthenticationStatus.AccountExpired;


                    outputClaims.Add(ClaimKeys.FirstName, userPrincipal.Name);
                    outputClaims.Add(ClaimKeys.FathersName, userPrincipal.Surname);
                    outputClaims.Add(ClaimKeys.MothersName, userPrincipal.GivenName);
                    outputClaims.Add(ClaimKeys.TelephoneNumber, userPrincipal.VoiceTelephoneNumber);
                    outputClaims.Add(ClaimKeys.Email, userPrincipal.EmailAddress);
                    outputClaims.Add(ClaimKeys.Rut, userPrincipal.EmployeeId);

                    var sRut = userPrincipal.EmployeeId.Substring(0, userPrincipal.EmployeeId.ToString().Length - 2);

                    WsnucsessionWebClient cliente = new WsnucsessionWebClient();
                    decimal cargoId = 1;
                    decimal estadoArchivo = decimal.Parse(Defaults.TipoInicio.ToString());
                    if (estadoArchivo == 2 || estadoArchivo == 3) // 2=> Contingencia Programada, 3 => Contingencia pasiva
                    {
                        cargoId = 99;//con este valor se recuperará el máximo id de sesión del usuario conectado en vez de generar un nuevo número
                    }
                    FcespGrabaSesionResult result = cliente.fcespGrabaSesion(sRut, userName, inputClaims["Mac"].ToString(), inputClaims["Ip"].ToString(), 1, ClaimKeys.VersionNuc.ToString(), "", "", 1, 1, 1, cargoId);
                    var alias = result.poUsuvaAliasNombre;
                    var sessionId = result.poSesnuSesionId;
                    var usuarioId = result.poUsunuUsuarioId;


                    Wssuser2rolesWebClient roles = new Wssuser2rolesWebClient();
                    SpsusFuncSuserXRutResult resultadoRoles = roles.spsusFuncSuserXRut(sRut);

					var rolesHolder = (resultadoRoles.roles == null || resultadoRoles.roles.Count() == 0) ? new List<RolModel>() : resultadoRoles.roles.Select(x => new RolModel { ID = x.rolId, Descripcion = x.funcionalidadDesc, grupoId = x.grupoId, grupoDescripcion = x.grupoDesc }).ToList();
					Alemana.Nucleo.Shared.DataHolder.SetValue(Nucleo.Shared.DataHolderKeys.Roles, rolesHolder);
					outputClaims.Add("Roles", rolesHolder);
					outputClaims.Add(ClaimKeys.SessionId, sessionId.ToString());
                    outputClaims.Add(ClaimKeys.Alias, alias.ToString());
                    outputClaims.Add(ClaimKeys.UsuarioId, usuarioId.ToString());

                    // GetPublishedVersion();

                    outputClaims.Add(ClaimKeys.VersionNuc, ClaimKeys.VersionNuc);


                    return new ClaimDictionary(outputClaims);
                }
            }
        }

        public string Version
        {
            get
            {
                Version myVersion = new Version();

                //ApplicationDeployment deploy =  ApplicationDeployment.CurrentDeployment;
                //UpdateCheckInfo update = deploy.CheckForDetailedUpdate();
                if (ApplicationDeployment.IsNetworkDeployed)
                    myVersion = ApplicationDeployment.CurrentDeployment.CurrentVersion;
                else
                    return "1.0.0.0";

                return string.Format("{0}.{1}.{2}.{3}", myVersion.Major, myVersion.Minor, myVersion.Build, myVersion.Revision);
                //return string.Format("V{0}", update.AvailableVersion.ToString());
            }
        }

        



        private Uri ActiveDirectoryUri { get; set; } 

        public ChangePasswordResult Change(NucleoIdentity identity, string password)
        {
            throw new NotImplementedException();
        }

    }
}
