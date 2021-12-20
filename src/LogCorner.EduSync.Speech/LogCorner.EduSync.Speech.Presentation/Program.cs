using LogCorner.EduSync.Speech.Telemetry.Configuration;
using Microsoft.AspNetCore;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Logging;

namespace LogCorner.EduSync.Speech.Presentation
{
    public class Program
    {
        public static void Main(string[] args)
        {
            CreateWebHostBuilder(args).Build().Run();
        }

        private static IWebHostBuilder CreateWebHostBuilder(string[] args) =>
            WebHost.CreateDefaultBuilder(args)
                   .ConfigureLogging((context, loggingBuilder) =>
                    {
                        loggingBuilder.ClearProviders();
                        loggingBuilder.AddConsole();
                        loggingBuilder.AddSerilog(context.Configuration);
                        loggingBuilder.AddOpenTelemetry(context.Configuration);
                    })
                   .UseStartup<Startup>();
    }
}