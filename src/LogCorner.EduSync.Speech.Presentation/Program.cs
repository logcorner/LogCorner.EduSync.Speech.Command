using Azure.Identity;
using LogCorner.EduSync.Speech.Telemetry.Configuration;
using Microsoft.AspNetCore;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using System;

namespace LogCorner.EduSync.Speech.Presentation
{
    public static class Program
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

                        if (bool.TryParse((ReadOnlySpan<char>)settings["isAuthenticationEnabled"], out var isAuthenticationEnabled)

                           && !context.HostingEnvironment.IsDevelopment() && isAuthenticationEnabled)
                        {
                            // Configure Azure Key Vault Connection
                            var uri = settings["AzureKeyVault:Uri"];
                            var tenantId = settings["AzureKeyVault:tenantId"];
                            var clientId = settings["AzureKeyVault:ClientId"];
                            var clientSecret = settings["AzureKeyVault:ClientSecret"];
                            if (!string.IsNullOrWhiteSpace(uri))

                                config.AddAzureKeyVault(
                                    new Uri(uri),
                                    new ClientSecretCredential(tenantId, clientId, clientSecret)
                                );
                        }
                    })
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