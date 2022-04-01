using System;
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
                        
                        bool.TryParse(settings["isAuthenticationEnabled"], out var isAuthenticationEnabled);
                        if (!context.HostingEnvironment.IsDevelopment() && isAuthenticationEnabled)
                        {
                            // Configure Azure Key Vault Connection
                            var uri = settings["AzureKeyVault:Uri"];
                            var clientId = settings["AzureKeyVault:ClientId"];
                            var clientSecret = settings["AzureKeyVault:ClientSecret"];

                            // Check, if Client ID and Client Secret credentials for a Service Principal
                            // have been provided. If so, use them to connect, otherwise let the connection 
                            // be done automatically in the background
                            if (!string.IsNullOrEmpty(clientId) && !string.IsNullOrEmpty(clientSecret))
                            {
                                Console.WriteLine($"******** using service principal to read secrets from keyvault : {uri} ********");
                                config.AddAzureKeyVault(uri, clientId, clientSecret);
                            }
                            else
                            {
                                Console.WriteLine($"******** using managed identity to read secrets from keyvault : {uri} ********");
                                config.AddAzureKeyVault(uri);
                            }
                        }
                    })
                    .UseStartup<Startup>();
    }
}