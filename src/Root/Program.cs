using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using System.IO;

namespace Root
{
    public class Program
    {
        public static void Main(string[] args)
        {
            BuildWebHost(args).Run();
        }

        public static IWebHost BuildWebHost(string[] args)
        {
            return new WebHostBuilder()
                .UseKestrel()
                .UseContentRoot(Directory.GetCurrentDirectory())
                .ConfigureAppConfiguration((hostingContext, config) =>
                {
                    IHostingEnvironment hostingEnvironment = hostingContext.HostingEnvironment;

                    config.AddJsonFile("appsettings.json", true, true);
                    config.AddJsonFile($"appsettings.{hostingEnvironment.EnvironmentName}.json", true, true);
                    config.AddJsonFile("connections.json", true, true);
                    config.AddJsonFile($"connections.{hostingEnvironment.EnvironmentName}.json", true, true);
                    config.AddEnvironmentVariables();
                })
                .ConfigureLogging((hostingContext, logging) =>
                {
                    logging.AddLog4Net();
                    logging.SetMinimumLevel(LogLevel.Debug);
                })
                .UseIISIntegration()
                .UseDefaultServiceProvider((context, options) =>
                    options.ValidateScopes = context.HostingEnvironment.IsDevelopment())
                .UseStartup<Startup>()
                .Build();
        }
    }
}