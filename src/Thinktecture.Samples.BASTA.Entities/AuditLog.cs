using System;

namespace Thinktecture.Samples.BASTA.Entities
{
    public class AuditLog
    {
        public Guid Id { get; set; }
        public AuditLevel Level { get; set; }
        public String Message { get; set; }
        public DateTime TimeStamp { get; set; }
    }

    public enum AuditLevel
    {
        Other,
        Created,
        Updated,
        Deleted
    }
}
