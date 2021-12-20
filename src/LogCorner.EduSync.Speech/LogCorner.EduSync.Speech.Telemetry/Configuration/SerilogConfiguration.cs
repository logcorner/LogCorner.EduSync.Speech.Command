using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using Serilog;

namespace LogCorner.EduSync.Speech.Telemetry.Configuration
{
    public static class SerilogConfiguration
    {
        public static void AddSerilog(this ILoggingBuilder loggingBuilder, IConfiguration configuration)
        {
            Log.Logger = new LoggerConfiguration()
                .ReadFrom.Configuration(configuration)
                .CreateLogger();
            loggingBuilder.AddSerilog();
        }
    }
}