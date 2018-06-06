using Alemana.Nucleo.Common.Exceptions;
using Alemana.Nucleo.Common.Tracing;
using Alemana.Nucleo.Common.wsSession;
using Alemana.Nucleo.Common.WsSuser2Roles;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Deployment.Application;
using System.Linq;

namespace Alemana.Nucleo.Common.Security.Providers
{
    public class MockSecurityProvider2 : IPasswordProvider, IAuthenticationProvider
    {
        public MockSecurityProvider2()
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
            catch (System.DirectoryServices.AccountManagement.PrincipalServerDownException ex)
            {
                SwitchLDAPServer();
                claims = Authenticate(inputClaims);
            }
            catch (Exception ex)
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

            // Rut por defecto de usuario qamedico
            var rutDefecto = "15678036-7";
            string rut = null;

            try
            {
                rut = ConfigurationManager.AppSettings["Alemana.Nucleo.Common.Security.Providers.MockSecurityProvider2.RutUsuario"];
            }
            catch { }

            if (string.IsNullOrWhiteSpace(rut))
                rut = rutDefecto;

            if (rut.Contains("."))
                rut = rut.Replace(".", string.Empty);

            // Obtengo el contexto de seguridad para AD-DS
            outputClaims.Add(ClaimKeys.AuthenticationStatus, AuthenticationStatus.OK);
            outputClaims.Add(ClaimKeys.FirstName, "QAMEDICO");
            outputClaims.Add(ClaimKeys.FathersName, "PAT6137313");
            outputClaims.Add(ClaimKeys.MothersName, "MAT6137313");
            outputClaims.Add(ClaimKeys.TelephoneNumber, "77778888");
            outputClaims.Add(ClaimKeys.Email, "wes.montgomery@alemana.cl");
            outputClaims.Add(ClaimKeys.Rut, rut);
            

            var sRut = rut.Substring(0, rut.Length - 2);

            WsnucsessionWebClient cliente = new WsnucsessionWebClient();
            var timer = new System.Diagnostics.Stopwatch();
            timer.Start();
            FcespGrabaSesionResult result = cliente.fcespGrabaSesion(sRut, userName, inputClaims["Mac"].ToString(), inputClaims["Ip"].ToString(), 1, "", "", "", 1, 1, 1, 1);
            var alias = result.poUsuvaAliasNombre;
            var sessionId = result.poSesnuSesionId;
            var usuarioId = result.poUsunuUsuarioId;
            Console.WriteLine(timer.Elapsed);

            Wssuser2rolesWebClient roles = new Wssuser2rolesWebClient();
            SpsusFuncSuserXRutResult resultadoRoles = roles.spsusFuncSuserXRut(sRut);

            var rolesHolder = (resultadoRoles.roles == null || resultadoRoles.roles.Count() == 0) ? new List<RolModel>() : resultadoRoles.roles.Select(x => new RolModel { ID = x.rolId, Descripcion = x.funcionalidadDesc, grupoId = x.grupoId, grupoDescripcion = x.grupoDesc }).ToList();
            Alemana.Nucleo.Shared.DataHolder.SetValue(Nucleo.Shared.DataHolderKeys.Roles, rolesHolder);
            outputClaims.Add("Roles", rolesHolder);
            if (!outputClaims.Any(k => k.Key == ClaimKeys.SessionId))
                outputClaims.Add(ClaimKeys.SessionId, sessionId.ToString());
            else
                outputClaims[ClaimKeys.SessionId] = sessionId.ToString();

            if (!outputClaims.Any(k => k.Key == ClaimKeys.UsuarioId))
                outputClaims.Add(ClaimKeys.Alias, alias.ToString());

            outputClaims[ClaimKeys.UsuarioId] = usuarioId.ToString();

            outputClaims.Add(ClaimKeys.VersionNuc, ClaimKeys.VersionNuc);

            return new ClaimDictionary(outputClaims);
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
