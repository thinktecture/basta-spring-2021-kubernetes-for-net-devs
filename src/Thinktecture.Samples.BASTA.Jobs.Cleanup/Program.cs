using System;
using System.Linq;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Thinktecture.Samples.BASTA.Configuration.Extensions;
using Thinktecture.Samples.BASTA.Entities;

namespace Thinktecture.Samples.BASTA.Jobs.Cleanup
{
    class Program
    {
        static void Main(string[] args)
        {
            var configuration = GetConfiguration();
            try
            {
                var bastaConfig = configuration.GetBastaAPIConfig();
                var contextOptions = new DbContextOptionsBuilder<BASTAContext>()
                    .UseSqlServer(bastaConfig.DatabaseConnectionString)
                    .Options;

                using var ctx = new BASTAContext(contextOptions);
                var oldLogs = ctx
                    .AuditLogs
                    .Where(al => al.TimeStamp < DateTime.UtcNow.AddDays(bastaConfig.AuditLogRetentionDays));

                ctx.RemoveRange(oldLogs);
                ctx.SaveChanges();
                Console.WriteLine("Audit Log cleaned up.");
            }
            catch (Exception exception)
            {
                Console.WriteLine($"ERROR while removing old AuditLogs.");
                Console.WriteLine(exception.Message);
                Console.WriteLine(exception.StackTrace);
            }
        }

        private static IConfiguration GetConfiguration()
        {
            var builder = new ConfigurationBuilder();
            builder.AddEnvironmentVariables();
            return builder.Build();
        }
    }
}
