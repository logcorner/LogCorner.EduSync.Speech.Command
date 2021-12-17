using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using OpenTelemetry.Resources;
using System;
using System.Collections.Generic;
using OpenTelemetry.Metrics;
using OpenTelemetry.Trace;
namespace LogCorner.EduSync.Speech.Telemetry
{
    public static class ServicesConfiguration
    {
        public static void AddOpenTelemetry(this IServiceCollection services, IConfiguration configuration)
        {
            // Define an OpenTelemetry resource
            // A resource represents a collection of attributes describing the
            // service. This collection of attributes will be associated with all
            // telemetry generated from this service (traces, metrics, logs).
            var opentelemetryServiceName = configuration["OpenTelemetry:ServiceName"];
            var environment = Environment.GetEnvironmentVariable("ASPNETCORE_ENVIRONMENT");
            var sourceName = configuration["OpenTelemetry:SourceName"];

            var jaergerHostName = configuration["OpenTelemetry:Jaeger:Hostname"];
            var jaergerPort = configuration["OpenTelemetry:Jaeger:PortNumber"];

            var zipkinHostName = configuration["OpenTelemetry:Zipkin:Hostname"];

            var zipkinPort = configuration["OpenTelemetry:Zipkin:PortNumber"];

            var resourceBuilder = ResourceBuilder
                .CreateDefault()
                .AddService(opentelemetryServiceName)
                .AddAttributes(new Dictionary<string, object> {
                    { "environment", environment }
                })
                .AddTelemetrySdk();

            // Configure the OpenTelemetry SDK for tracing
            services.AddOpenTelemetryTracing(tracerProviderBuilder =>
            {
                // Step 1. Declare the resource to be used by this tracer provider.
                tracerProviderBuilder
                    .SetResourceBuilder(resourceBuilder);

                // Step 2. Configure the SDK to listen to the following auto-instrumentation
                tracerProviderBuilder
                    .AddAspNetCoreInstrumentation(options =>
                    {
                        options.RecordException = true;
                        //options.Filter = (context) =>
                        //{
                        //    return context.Request.Method == "GET";
                        //};
                    })
                    .AddHttpClientInstrumentation();

                // Step 3. Configure the SDK to listen to custom instrumentation.
                tracerProviderBuilder
                    .AddSource(sourceName);

                // Step 4. Configure the OTLP exporter to export to New Relic
                //     The OTEL_EXPORTER_OTLP_ENDPOINT environment variable should be set to New Relic's OTLP endpoint:
                //         OTEL_EXPORTER_OTLP_ENDPOINT=https://otlp.nr-data.net:4317
                //
                //     The OTEL_EXPORTER_OTLP_HEADERS environment variable should be set to include your New Relic API key:
                //         OTEL_EXPORTER_OTLP_HEADERS=api-key=<YOUR_API_KEY_HERE>
                tracerProviderBuilder
                    .AddOtlpExporter(options =>
                    {
                        options.Endpoint = new Uri("https://otlp.nr-data.net:4317");//new Uri($"{Environment.GetEnvironmentVariable("OTEL_EXPORTER_OTLP_ENDPOINT")}");
                        options.Headers =
                            "api-key=bb413cc336625e6b6569a7dc4a03f858789cNRAL"; //Environment.GetEnvironmentVariable("OTEL_EXPORTER_OTLP_HEADERS");
                    });

                tracerProviderBuilder.AddConsoleExporter();

                tracerProviderBuilder.AddJaegerExporter(o =>
                {
                    o.AgentHost = jaergerHostName;
                    o.AgentPort = int.Parse(jaergerPort);
                });

                tracerProviderBuilder.AddZipkinExporter(b =>
                {
                    b.Endpoint = new Uri($"http://{zipkinHostName}:{zipkinPort}/api/v2/spans");
                });
            });

            services.AddOpenTelemetryMetrics(meterProviderBuilder =>
            {
                // Step 1. Declare the resource to be used by this meter provider.
                meterProviderBuilder
                    .SetResourceBuilder(resourceBuilder);

                // Step 2. Configure the SDK to listen to the following auto-instrumentation
                meterProviderBuilder
                    .AddAspNetCoreInstrumentation()
                    .AddHttpClientInstrumentation();

                // Step 3. Configure the OTLP exporter to export to New Relic
                //     The OTEL_EXPORTER_OTLP_ENDPOINT environment variable should be set to New Relic's OTLP endpoint:
                //         OTEL_EXPORTER_OTLP_ENDPOINT=https://otlp.nr-data.net:4317
                //
                //     The OTEL_EXPORTER_OTLP_HEADERS environment variable should be set to include your New Relic API key:
                //         OTEL_EXPORTER_OTLP_HEADERS=api-key=<YOUR_API_KEY_HERE>
                meterProviderBuilder
                    .AddOtlpExporter(options =>
                    {
                        //options.Endpoint = new Uri($"{Environment.GetEnvironmentVariable("OTEL_EXPORTER_OTLP_ENDPOINT")}");
                        //options.Headers = Environment.GetEnvironmentVariable("OTEL_EXPORTER_OTLP_HEADERS");

                        options.Endpoint = new Uri("https://otlp.nr-data.net:4317");//new Uri($"{Environment.GetEnvironmentVariable("OTEL_EXPORTER_OTLP_ENDPOINT")}");
                        options.Headers =
                            "api-key=bb413cc336625e6b6569a7dc4a03f858789cNRAL"; //Environment.GetEnvironmentVariable("OTEL_EXPORTER_OTLP_HEADERS");

                        // New Relic requires the exporter to use delta aggregation temporality.
                        // The OTLP exporter defaults to using cumulative aggregation temporatlity.
                        options.AggregationTemporality = AggregationTemporality.Delta;
                    });
                //meterProviderBuilder.AddConsoleExporter();
            });
            services.AddSingleton<IOpenTelemetryService, OpenTelemetryService>();
        }
    }
}