using System;

namespace Thinktecture.Samples.BASTA.Configuration
{
    public class BastaAPIConfig
    {
        public const String RootSectionName = "Thinktecture";
        public const String SectionName = "BastaApi";
        
        public String DatabaseConnectionString { get; set; }
        public int AuditLogRetentionDays { get; set; }
    }
}
