using System;
using System.Collections.Generic;
using Microsoft.AspNetCore;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Logging;
using OpenTelemetry.Logs;
using OpenTelemetry.Resources;

namespace LogCorner.EduSync.Speech.Presentation
{
    public class Program
    {
        public static void Main(string[] args)
        {
            CreateWebHostBuilder(args).Build().Run();
        }

        public static IWebHostBuilder CreateWebHostBuilder(string[] args) =>
            WebHost.CreateDefaultBuilder(args)
                .ConfigureLogging(loggingBuilder =>
                {
                    loggingBuilder.ClearProviders();
                    loggingBuilder.AddConsole();

                    loggingBuilder
                        .AddOpenTelemetry(options =>
                        {
                            options.IncludeFormattedMessage = true;
                            options.IncludeScopes = true;
                            options.ParseStateValues = true;

                            options
                                .SetResourceBuilder(
                                    ResourceBuilder
                                        .CreateDefault()
                                        .AddService("OpenTelemetry-Dotnet-Example")
                                        .AddAttributes(new Dictionary<string, object> {
                                            { "environment", "production" }
                                        })
                                        .AddTelemetrySdk())
                                .AddOtlpExporter(options =>
                                {
                                    //options.Endpoint = new Uri($"{Environment.GetEnvironmentVariable("OTEL_EXPORTER_OTLP_ENDPOINT")}");
                                    //options.Headers = Environment.GetEnvironmentVariable("OTEL_EXPORTER_OTLP_HEADERS");

                                    options.Endpoint = new Uri("https://otlp.nr-data.net:4317");//new Uri($"{Environment.GetEnvironmentVariable("OTEL_EXPORTER_OTLP_ENDPOINT")}");
                                    options.Headers =
                                        "api-key=bb413cc336625e6b6569a7dc4a03f858789cNRAL"; //Environment.GetEnvironmentVariable("OTEL_EXPORTER_OTLP_HEADERS");
                                });
                        });
                })
                .UseStartup<Startup>();
    }
}