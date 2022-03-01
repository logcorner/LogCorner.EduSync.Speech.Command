using Microsoft.AspNetCore;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Hosting;

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
                .ConfigureAppConfiguration((context, config) =>
                    {
                        var settings = config.Build();

                        if (!context.HostingEnvironment.IsDevelopment())
                        {
                            // Configure Azure Key Vault Connection
                            var uri = settings["AzureKeyVault:Uri"];
                            var clientId = settings["AzureKeyVault:ClientId"];
                            var clientSecret = settings["AzureKeyVault:ClientSecret"];

                            // Check, if Client ID and Client Secret credentials for a Service Principal
                            // have been provided. If so, use them to connect, otherwise let the connection 
                            // be done automatically in the background
                            if (!string.IsNullOrEmpty(clientId) && !string.IsNullOrEmpty(clientSecret))
                                config.AddAzureKeyVault(uri, clientId, clientSecret);
                            else
                                config
                                    .AddAzureKeyVault(uri);
                        }
                    })
                    .UseStartup<Startup>();
    }
}