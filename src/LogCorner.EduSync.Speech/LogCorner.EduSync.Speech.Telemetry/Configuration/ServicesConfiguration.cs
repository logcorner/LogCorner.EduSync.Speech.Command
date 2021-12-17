using Microsoft.Extensions.DependencyInjection;

namespace LogCorner.EduSync.Speech.Telemetry.Configuration
{
    public static class ServicesConfiguration
    {
        public static void AddTelemetryServices(this IServiceCollection services)
        {
            services.AddSingleton<IOpenTelemetryService, OpenTelemetryService>();
        }
    }
}