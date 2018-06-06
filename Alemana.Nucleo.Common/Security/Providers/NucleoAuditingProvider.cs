using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Alemana.Nucleo.Common.WsAuditoriaGestionServiceReference;
using Alemana.Nucleo.Common.Tracing;
using Alemana.Nucleo.Shared.Helpers;

namespace Alemana.Nucleo.Common.Security.Providers
{
    public class NucleoAuditingProvider : IAuditingProvider
    {
        public void Audit(AuditEvent auditEvent)
        {
            decimal sessionID = Convert.ToDecimal(Nucleo.Common.Security.SecurityManager.CurrentUser.NucleoIdentity.SessionId);//(decimal)(Nucleo.Shared.DataHolder.GetValue(ClaimNames.SessionID.ToString()) ?? -1m);
            decimal fichaID = (decimal)(Nucleo.Shared.DataHolder.GetValue(ClaimNames.FichaID.ToString()) ?? -1m);
            decimal episodioID = (decimal)(Nucleo.Shared.DataHolder.GetValue(ClaimNames.EpisodioID.ToString()) ?? -1m);
            decimal encuentroID = (decimal)(Nucleo.Shared.DataHolder.GetValue(ClaimNames.EncuentroID.ToString()) ?? -1m);


            if (auditEvent.Context.ContainsKey(ClaimEventType.ModuleUsageStartAuditing.ToString()))
            {
                if (auditEvent.EventText.Contains("Container"))
                    return;

                if (auditEvent.EventText.Contains("ReportViewer"))
                    return;

                var client = new WsauditoriagestionWebClient();

                using (ServiceChannel.AsDisposable(client))
                {
                    var res = client.fcespIniciaAuditoriaAsync(auditEvent.EventText, sessionID, fichaID, episodioID, encuentroID);
                }
            }
        }
    }





    
}
