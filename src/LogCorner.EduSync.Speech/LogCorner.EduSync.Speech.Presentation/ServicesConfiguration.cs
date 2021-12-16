using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Identity.Web;
using Microsoft.OpenApi.Models;
using OpenTelemetry.Metrics;
using OpenTelemetry.Resources;
using OpenTelemetry.Trace;
using System;
using System.Collections.Generic;

namespace LogCorner.EduSync.Speech.Presentation
{
    public static class ServicesConfiguration
    {
        public static void AddCustomAuthentication(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
                .AddMicrosoftIdentityWebApi(options =>
                    {
                        configuration.Bind("AzureAdB2C", options);

                        options.TokenValidationParameters.NameClaimType = "name";
                    },
                    options => { configuration.Bind("AzureAdB2C", options); });
        }

        public static void AddCustomSwagger(this IServiceCollection services, IConfiguration configuration)
        {
            var tenantName = configuration["SwaggerUI:TenantName"];
            services.AddSwaggerGen(options =>
            {
                options.SwaggerDoc("v1", new OpenApiInfo
                {
                    Title = "LogCorner Micro Service Event Driven Architecture - Command HTTP API",
                    Version = "v1",
                    Description = "The Speech Micro Service Command HTTP API"
                });
                options.AddSecurityDefinition("oauth2", new OpenApiSecurityScheme
                {
                    Type = SecuritySchemeType.OAuth2,

                    Flows = new OpenApiOAuthFlows
                    {
                        AuthorizationCode = new OpenApiOAuthFlow
                        {
                            AuthorizationUrl = new Uri($"https://{tenantName}.b2clogin.com/{tenantName}.onmicrosoft.com/B2C_1_SignUpIn/oauth2/v2.0/authorize"),
                            TokenUrl = new Uri($"https://{tenantName}.b2clogin.com/{tenantName}.onmicrosoft.com/B2C_1_SignUpIn/oauth2/v2.0/token"),
                            Scopes = new Dictionary<string, string>
                            {
                                {$"https://{tenantName}.onmicrosoft.com/command/api/Speech.Create","Create a new Speech"},
                                {$"https://{tenantName}.onmicrosoft.com/command/api/Speech.Edit", "Edit and Update a  Speech" },
                                {$"https://{tenantName}.onmicrosoft.com/command/api/Speech.Delete","Delete a Speech"}
                            }
                        }
                    }
                });

                options.AddSecurityRequirement(new OpenApiSecurityRequirement
                {
                    {
                        new OpenApiSecurityScheme
                        {
                            Reference = new OpenApiReference
                            {
                                Type = ReferenceType.SecurityScheme,
                                Id = "oauth2"
                            }
                        },
                        new[] {
                                $"https://{tenantName}.onmicrosoft.com/command/api/Speech.Create",
                                $"https://{tenantName}.onmicrosoft.com/command/api/Speech.Edit",
                                $"https://{tenantName}.onmicrosoft.com/command/api/Speech.Delete"
                              }
                    }
                });
            });
        }

        public static void AddOpenTelemetry(this IServiceCollection services, IConfiguration configuration)
        {
            // Define an OpenTelemetry resource
            // A resource represents a collection of attributes describing the
            // service. This collection of attributes will be associated with all
            // telemetry generated from this service (traces, metrics, logs).
            var opentelemetryServiceName = configuration["OpenTelemetry:ServiceName"];
            var environment = Environment.GetEnvironmentVariable("ASPNETCORE_ENVIRONMENT");
            var sourceName = configuration["OpenTelemetry:SourceName"];

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

                tracerProviderBuilder.AddJaegerExporter(o =>
                {
                    o.AgentHost = "localhost";
                    o.AgentPort = 6831;
                });

                tracerProviderBuilder.AddZipkinExporter(b =>
                {
                    var zipkinHostName = "localhost";
                    b.Endpoint = new Uri($"http://{zipkinHostName}:9412/api/v2/spans");
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
            });
        }
    }
}