using System;
using System.Configuration;

namespace Alemana.Nucleo.Common
{
    public class Defaults
    {
        // App Settings Keys
        internal const string DefaultExceptionPolicySetting = "Alemana.Nucleo.Common.DefaultExceptionPolicy";
        internal const string ApplicationNameSetting = "Alemana.Nucleo.Common.ApplicationName";
        internal const string LightweightTraceSetting = "Alemana.Nucleo.Common.Trace.Lightweight";
        internal const string ActiveTraceSourceSetting = "Alemana.Nucleo.Common.Trace.ActiveTraceSource";
        internal const string OfflineTraceSourceSetting = "Alemana.Nucleo.Common.Trace.OfflineTraceSource";
        internal const string CacheOfflineTraceSourceSetting = "Alemana.Nucleo.Common.Trace.CacheOfflineTraceSource";
        internal const string MockAuthenticationSetting = "Alemana.Nucleo.Common.Security.Authentication.MockMode";
        internal const string MockAuthentication2Setting = "Alemana.Nucleo.Common.Security.Authentication.MockMode2";
        internal const string MockPasswordSetting = "Alemana.Nucleo.Common.Security.Password.MockMode";
        internal const string MockAuthorizationSetting = "Alemana.Nucleo.Common.Security.Authorization.MockMode";
        internal const string MockAuditingSetting = "Alemana.Nucleo.Common.Security.Auditing.MockMode";
        internal const string CryptoHelperKeySetting = "Alemana.Nucleo.Common.Utility.CryptoHelper.Key";
        internal const string PermissionsCacheSetting = "Alemana.Nucleo.Common.Security.PermissionsCache";
        internal const string ADConnectionStringSetting = "Alemana.Nucleo.Common.Security.ActiveDirectory.ConnectionString";
        internal const string TipoInicioSetting = "Alemana.Nucleo.Contingencia.TipoInicio";
        internal const string DeboReiniciarSetting = "Alemana.Nucleo.Contingencia.DeboReiniciar";
        internal const string DeboDescargarCacheSetting = "Alemana.Nucleo.Contingencia.DescargarCache";
        internal const string RutaDistribucionSetting = "Alemana.Nucleo.Contingencia.RutaDistribucion";
        internal const string RutaCacheSetting = "Alemana.Nucleo.Contingencia.RutaCache";
        internal const string RutaPolicySetting = "Alemana.Nucleo.Contingencia.RutaPolicy";
        internal const string HabilitarDistribuidorCacheSetting = "Alemana.Nucleo.Contingencia.HabilitarDistribuidorCache";


        // Este timeout no es configurable a propósito ya que es un dato que debiera proporcionarlo
        // el proveedor de autenticación
        internal const int DefaultSessionTimeout = 300;

        private static string _applicationName;
        public static string ApplicationName
        {
            get
            {
                if (_applicationName == null)
                {
                    if (!String.IsNullOrEmpty(ConfigurationManager.AppSettings[ApplicationNameSetting]))
                        _applicationName = ConfigurationManager.AppSettings[ApplicationNameSetting];
                    else
                        _applicationName = AppDomain.CurrentDomain.FriendlyName;
                }

                return _applicationName;
            }
        }


        public static string RutaPolicy
        {
            get
            {
                string sourceName = ConfigurationManager.AppSettings[Defaults.RutaPolicySetting];

                if (sourceName == null)
                    sourceName = "policy\\";

                return sourceName;
            }
        }

        public static string RutaCache
        {
            get
            {
                string sourceName = ConfigurationManager.AppSettings[Defaults.RutaCacheSetting];

                if (sourceName == null)
                    sourceName = "cache\\";

                return sourceName;
            }
        }

        public static string RutaDistribucion
        {
            get
            {
                string sourceName = ConfigurationManager.AppSettings[Defaults.RutaDistribucionSetting];

                if (sourceName == null)
                    sourceName = "\\pulox\\offline\\distribucion1";

                return sourceName;
            }
        }
        public static string HabilitarDistribuidorCache
        {
            get
            {
                string sourceName = ConfigurationManager.AppSettings[Defaults.HabilitarDistribuidorCacheSetting];

                if (sourceName == null)
                    sourceName = "0";

                return sourceName;
            }
        }

        public static int DeboDescargarCache
        {
            get
            {
                string sourceName = ConfigurationManager.AppSettings[Defaults.DeboDescargarCacheSetting];

                if (sourceName == null)
                    sourceName = "1";

                return Convert.ToInt32(sourceName);
            }
        }

        public static int DeboReiniciar
        {
            get
            {
                string sourceName = ConfigurationManager.AppSettings[Defaults.DeboReiniciarSetting];

                if (sourceName == null)
                    sourceName = "1";

                return Convert.ToInt32(sourceName);
            }
        }

        public static int TipoInicio
        {
            get
            {
                string sourceName = ConfigurationManager.AppSettings[Defaults.TipoInicioSetting];

                if (sourceName == null)
                    sourceName = "1";

                return Convert.ToInt32(sourceName);
            }
        }

        public static string DefaultTraceSource
        {
            get
            {
                string sourceName = ConfigurationManager.AppSettings[Defaults.ActiveTraceSourceSetting];

                if (sourceName == null)
                    sourceName = "Alemana.Nucleo.Common";

                return sourceName;
            }
        }

        public static string DefaultOfflineTraceSource
        {
            get
            {
                string sourceName = ConfigurationManager.AppSettings[Defaults.OfflineTraceSourceSetting];

                if (sourceName == null)
                    sourceName = "Alemana.Nucleo.Offline";

                return sourceName;
            }
        }

        public static string DefaultCacheOfflineTraceSource
        {
            get
            {
                string sourceName = ConfigurationManager.AppSettings[Defaults.CacheOfflineTraceSourceSetting];

                if (sourceName == null)
                    sourceName = "Alemana.Nucleo.Cache.Offline";

                return sourceName;
            }
        }

        public static string DefaultExceptionPolicy
        {
            get
            {
                string policyName = ConfigurationManager.AppSettings[Defaults.DefaultExceptionPolicySetting];

                if (policyName == null)
                    policyName = "Alemana.Nucleo.Common";
                
                return policyName;
            }
        }

 

    }
}
