﻿using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using OpenTelemetry.Logs;
using OpenTelemetry.Metrics;
using OpenTelemetry.Resources;
using OpenTelemetry.Trace;
using System;
using System.Collections.Generic;

namespace LogCorner.EduSync.Speech.Telemetry.Configuration
{
    public static class OpenTelemetryConfiguration
    {
        public static void AddOpenTelemetry(this ILoggingBuilder loggingBuilder, IConfiguration configuration)
        {
            var openTelemetryServiceName = configuration["OpenTelemetry:ServiceName"];
            var environment = Environment.GetEnvironmentVariable("ASPNETCORE_ENVIRONMENT");
            var newRelicHostName = configuration["OpenTelemetry:NewRelic:Hostname"];
            var newRelicPortNumber = int.Parse(configuration["OpenTelemetry:NewRelic:PortNumber"]);
            var newRelicApiKey = configuration["OpenTelemetry:NewRelic:LicenceKey"];
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
                                .AddService(openTelemetryServiceName)
                                .AddAttributes(new Dictionary<string, object>
                                {
                                    { "environment", environment }
                                })
                                .AddTelemetrySdk())
                        .AddOtlpExporter(exporterOptions =>
                        {
                            exporterOptions.Endpoint = new Uri($"{newRelicHostName}:{newRelicPortNumber}");
                            exporterOptions.Headers = $"api-key={newRelicApiKey}";
                        });
                });
        }

        public static void AddOpenTelemetry(this IServiceCollection services, IConfiguration configuration)
        {
            // Define an OpenTelemetry resource
            // A resource represents a collection of attributes describing the
            // service. This collection of attributes will be associated with all
            // telemetry generated from this service (traces, metrics, logs).
            var openTelemetryServiceName = configuration["OpenTelemetry:ServiceName"];
            var environment = Environment.GetEnvironmentVariable("ASPNETCORE_ENVIRONMENT");
            var sourceName = configuration["OpenTelemetry:SourceName"];

            var jaergerHostName = configuration["OpenTelemetry:Jaeger:Hostname"];
            var jaergerPort = configuration["OpenTelemetry:Jaeger:PortNumber"];

            var zipkinHostName = configuration["OpenTelemetry:Zipkin:Hostname"];
            var zipkinPort = configuration["OpenTelemetry:Zipkin:PortNumber"];

            var newRelicHostName = configuration["OpenTelemetry:NewRelic:Hostname"];
            var newRelicPortNumber = int.Parse(configuration["OpenTelemetry:NewRelic:PortNumber"]);
            var newRelicApiKey = configuration["OpenTelemetry:NewRelic:LicenceKey"];

            var resourceBuilder = ResourceBuilder
                .CreateDefault()
                .AddService(openTelemetryServiceName)
                .AddAttributes(new Dictionary<string, object>
                {
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
                    .AddHttpClientInstrumentation()
                    .AddSqlClientInstrumentation(s => s.SetDbStatementForText = true);

                // Step 3. Configure the SDK to listen to custom instrumentation.
                tracerProviderBuilder
                    .AddSource(sourceName);

                // Step 4. Configure the OTLP exporter to export to New Relic
                //     The OTEL_EXPORTER_OTLP_ENDPOINT environment variable should be set to New Relic's OTLP endpoint:
                //         OTEL_EXPORTER_OTLP_ENDPOINT=https://otlp.nr-data.net:4317
                //
                //     The OTEL_EXPORTER_OTLP_HEADERS environment variable should be set to include your New Relic API key:
                //         OTEL_EXPORTER_OTLP_HEADERS=api-key=<YOUR_API_KEY_HERE>
                tracerProviderBuilder.AddNewRelicExporter(newRelicHostName, newRelicPortNumber, newRelicApiKey);

                tracerProviderBuilder.AddConsoleExporter();

                tracerProviderBuilder.AddJaegerExporter(jaergerHostName, jaergerPort);

                tracerProviderBuilder.AddZipkinExporter(zipkinHostName, zipkinPort);
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
                meterProviderBuilder.AddNewRelicExporter(newRelicHostName, newRelicPortNumber, newRelicApiKey);
                //meterProviderBuilder.AddConsoleExporter();
            });
            services.AddTelemetryServices();
        }

    }
}