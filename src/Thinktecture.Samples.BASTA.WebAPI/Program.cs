using System.IO;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;

namespace Thinktecture.Samples.BASTA.WebAPI
{
    public class Program
    {
        public static void Main(string[] args)
        {
            CreateHostBuilder(args).Build().Run();
        }

        public static IHostBuilder CreateHostBuilder(string[] args) =>
            Host.CreateDefaultBuilder(args)
                .ConfigureLogging(logging =>
                {
                    // logs should go straight to STDOUT and STDERR
                    logging.ClearProviders();
                    logging
                        // Debug for integration with IDE when executing locally
                        .AddDebug()
                        .AddConsole();
                })
                .ConfigureAppConfiguration(builder =>
                {
                    // if you rely on ConfigMaps and Secrets in Kubernetes, you can add Key-Pre-File Provider
                    builder.AddKeyPerFile("/etc/thinktecture", true);
                    // consider mounting /etc/thinktecture straight from a Kubernetes ConfigMap
                    // see https://thns.io/configmap-vol
                    
                    // Ensure that configuration is applied using environment variables
                    builder.AddEnvironmentVariables();
                })
                .ConfigureWebHostDefaults(webBuilder => { webBuilder.UseStartup<Startup>(); });
    }
}
