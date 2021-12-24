using System;
using OpenTelemetry.Metrics;
using OpenTelemetry.Trace;
using Serilog;

namespace LogCorner.EduSync.Speech.Telemetry.Configuration
{
    public static class OpenTelemetryExporterConfiguration
    {
        public static void AddNewRelicExporter(this MeterProviderBuilder meterProviderBuilder,
            string hostName, int portNumber, string apiKey)
        {
            meterProviderBuilder
                .AddOtlpExporter(options =>
                {
                    options.Endpoint = new Uri($"{hostName}:{portNumber}");
                    options.Headers = $"api-key={apiKey}";
                    // New Relic requires the exporter to use delta aggregation temporality.
                    // The OTLP exporter defaults to using cumulative aggregation temporatlity.
                    options.AggregationTemporality = AggregationTemporality.Delta;
                });
        }

        public static void AddNewRelicExporter(this TracerProviderBuilder tracerProviderBuilder, string hostName, int portNumber, string apiKey)
        {
            tracerProviderBuilder
                .AddOtlpExporter(options =>
                {
                    options.Endpoint = new Uri($"{hostName}:{portNumber}");
                    options.Headers = $"api-key={apiKey}";
                });
        }

        public static void AddZipkinExporter(this TracerProviderBuilder tracerProviderBuilder, string zipkinHostName, string zipkinPort)
        {
            var endpoint = new Uri($"http://{zipkinHostName}:{zipkinPort}/api/v2/spans");
            Log.Debug($"OpenTelemetryExporterConfiguration::AddZipkinExporter:Endpoint {endpoint}");
                tracerProviderBuilder.AddZipkinExporter(b =>
            {
                b.Endpoint = endpoint;
            });
        }

        public static void AddJaegerExporter(this TracerProviderBuilder tracerProviderBuilder, string jaergerHostName, string jaergerPort)
        {
            tracerProviderBuilder.AddJaegerExporter(o =>
            {

                o.AgentHost = jaergerHostName;
                o.AgentPort = int.Parse(jaergerPort);
            });
        }
    }
}