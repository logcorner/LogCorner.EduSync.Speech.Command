using Microsoft.Extensions.DependencyInjection;

namespace LogCorner.EduSync.Speech.SharedKernel.Serialyser
{
    public static class ServicesConfiguration
    {
        public static void AddSharedKernel(this IServiceCollection services) 
        {
            services.AddSingleton<IEventSerializer, JsonEventSerializer>();
            services.AddSingleton<IJsonProvider, JsonProvider>();
        }
    }
}