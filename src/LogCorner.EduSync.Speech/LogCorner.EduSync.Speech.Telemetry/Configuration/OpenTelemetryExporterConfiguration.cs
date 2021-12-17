using System;
using OpenTelemetry.Metrics;
using OpenTelemetry.Trace;

namespace LogCorner.EduSync.Speech.Telemetry.Configuration
{
    public static class OpenTelemetryExporterConfiguration
    {
        public static void AddNewRelicExporter(this MeterProviderBuilder meterProviderBuilder)
        {
            meterProviderBuilder
                .AddOtlpExporter(options =>
                {
                    options.Endpoint = new Uri("https://otlp.nr-data.net:4317");
                    options.Headers = "api-key={key}";

                    // New Relic requires the exporter to use delta aggregation temporality.
                    // The OTLP exporter defaults to using cumulative aggregation temporatlity.
                    options.AggregationTemporality = AggregationTemporality.Delta;
                });
        }

        public static void AddNewRelicExporter(this TracerProviderBuilder tracerProviderBuilder)
        {
            tracerProviderBuilder
                .AddOtlpExporter(options =>
                {
                    options.Endpoint = new Uri("https://otlp.nr-data.net:4317");
                    options.Headers = "api-key={key}";
                });
        }

        public static void AddZipkinExporter(this TracerProviderBuilder tracerProviderBuilder, string zipkinHostName, string zipkinPort)
        {
            tracerProviderBuilder.AddZipkinExporter(b =>
            {
                b.Endpoint = new Uri($"http://{zipkinHostName}:{zipkinPort}/api/v2/spans");
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