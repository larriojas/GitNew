using System;
using System.Collections.Generic;

namespace Alemana.Nucleo.Common.Security
{
    public interface IAuditingProvider
    {
        void Audit(AuditEvent auditEvent);
    }

    [Serializable]
    public class AuditEvent
    {
        public AuditEvent(int auditEventId, string eventText, Dictionary<string, object> contextData)
        {
            UniqueId = Guid.NewGuid();
            AuditEventId = auditEventId;
            EventText = eventText;
            Context = new ClaimDictionary(contextData ?? new Dictionary<string, object>());
        }

        public int AuditEventId { get; private set; }
        public Guid UniqueId { get; private set; }
        public string EventText { get; private set; }
        public ClaimDictionary Context { get; private set; }
    }


}
