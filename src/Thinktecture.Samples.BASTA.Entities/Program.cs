using System;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Thinktecture.Samples.BASTA.Configuration.Extensions;

namespace Thinktecture.Samples.BASTA.Entities
{
    class Program
    {
        static async Task Main(string[] args)
        {
            var configuration = GetConfiguration();
            try
            {
                var bastaConfig = configuration.GetBastaAPIConfig();
                var contextOptions = new DbContextOptionsBuilder<BASTAContext>()
                    .UseSqlServer(bastaConfig.DatabaseConnectionString)
                    .Options;

                await using var ctx = new BASTAContext(contextOptions);
                
                var migrationTags = ctx.Database.MigrateAsync();
                Console.WriteLine("Migrating Database...");
                while (!migrationTags.IsCompleted)
                {
                    Console.Write(".");
                    Thread.Sleep(50);
                }
                Console.WriteLine("Migration finished");
            }
            catch (Exception exception)
            {
                Console.WriteLine($"ERROR while executing database migrations");
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
