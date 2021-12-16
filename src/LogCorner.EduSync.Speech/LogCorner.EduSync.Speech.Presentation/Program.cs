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
                .ConfigureLogging((context,loggingBuilder )=>
                {
                    var opentelemetryServiceName = context.Configuration["OpenTelemetry:ServiceName"];
                    var environment = Environment.GetEnvironmentVariable("ASPNETCORE_ENVIRONMENT");
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
                                        .AddService(opentelemetryServiceName)
                                        .AddAttributes(new Dictionary<string, object> {
                                            { "environment", environment }
                                        })
                                        .AddTelemetrySdk())
                                .AddOtlpExporter(exporterOptions =>
                                {
                                    //options.Endpoint = new Uri($"{Environment.GetEnvironmentVariable("OTEL_EXPORTER_OTLP_ENDPOINT")}");
                                    //options.Headers = Environment.GetEnvironmentVariable("OTEL_EXPORTER_OTLP_HEADERS");

                                    exporterOptions.Endpoint = new Uri("https://otlp.nr-data.net:4317");//new Uri($"{Environment.GetEnvironmentVariable("OTEL_EXPORTER_OTLP_ENDPOINT")}");
                                    exporterOptions.Headers =
                                        "api-key=bb413cc336625e6b6569a7dc4a03f858789cNRAL"; //Environment.GetEnvironmentVariable("OTEL_EXPORTER_OTLP_HEADERS");
                                });
                        });
                })
                .UseStartup<Startup>();
    }
}