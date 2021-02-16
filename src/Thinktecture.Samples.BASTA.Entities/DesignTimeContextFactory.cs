using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using Microsoft.Extensions.Configuration;
using Thinktecture.Samples.BASTA.Configuration.Extensions;

namespace Thinktecture.Samples.BASTA.Entities
{
    // ReSharper disable once UnusedType.Global
    public class DesignTimeContextFactory: IDesignTimeDbContextFactory<BASTAContext>
    {
        public BASTAContext CreateDbContext(string[] args)
        {
            var builder = new ConfigurationBuilder();
            builder.AddEnvironmentVariables();
            var configuration = builder.Build();

            var bastaConfig = configuration.GetBastaAPIConfig();
            
            var optionsBuilder = new DbContextOptionsBuilder<BASTAContext>();
            optionsBuilder.UseSqlServer(bastaConfig.DatabaseConnectionString);

            return new BASTAContext(optionsBuilder.Options);
        }
    }
}
