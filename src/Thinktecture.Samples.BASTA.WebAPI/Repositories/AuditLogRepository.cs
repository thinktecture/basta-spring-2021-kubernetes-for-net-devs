using System.Threading.Tasks;
using Thinktecture.Samples.BASTA.Entities;

namespace Thinktecture.Samples.BASTA.WebAPI.Repositories
{
    public class AuditLogRepository : IAuditLogRepository
    {
        protected BASTAContext Context { get; }

        public AuditLogRepository(BASTAContext context)
        {
            Context = context;
        }
        public async Task AuditCreatedAsync(string message)
        {
            var audit = CreateAuditLog(message, AuditLevel.Created);
            Context.AuditLogs.Add(audit);
            await Context.SaveChangesAsync();
        }

        public async Task AuditUpdatedAsync(string message)
        {
            var audit = CreateAuditLog(message, AuditLevel.Updated);
            Context.AuditLogs.Add(audit);
            await Context.SaveChangesAsync();
        }

        public async Task AuditDeletedAsync(string message)
        {
            var audit = CreateAuditLog(message, AuditLevel.Deleted);
            Context.AuditLogs.Add(audit);
            await Context.SaveChangesAsync();
        }

        private AuditLog CreateAuditLog(string message, AuditLevel level)
        {
            return new()
            {
                Message = message,
                Level = level
            };
        }
    }
}
