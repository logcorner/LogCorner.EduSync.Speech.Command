using LogCorner.EduSync.Speech.Command.SharedKernel.Serialyser;
using Microsoft.Extensions.DependencyInjection;

namespace LogCorner.EduSync.Speech.Command.SharedKernel
{
    public static class ServicesConfiguration
    {
        public static void AddSharedKernel(this IServiceCollection services)
        {
            services.AddSingleton<IJsonSerializer, JsonSerializer>();
            services.AddSingleton<IEventSerializer, JsonEventSerializer>();
            services.AddSingleton<IJsonProvider, JsonProvider>();
        }
    }
}