using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using System.Collections.Generic;
using System.Diagnostics;

namespace LogCorner.EduSync.Speech.Telemetry
{
    public class OpenTelemetryService : IOpenTelemetryService
    {
        private readonly IConfiguration _configuration;
        private readonly ILogger<OpenTelemetryService> _logger;

        private string SourceName => _configuration["OpenTelemetry:SourceName"];

        // An ActivitySource is .NET's term for an OpenTelemetry Tracer.
        // Spans generated from this ActivitySource are associated with the ActivitySource's name and version.
        private readonly ActivitySource _tracer;

        public OpenTelemetryService(IConfiguration configuration, ILogger<OpenTelemetryService> logger )
        {
            _configuration = configuration;
            _logger = logger;
            _tracer = new ActivitySource(SourceName);
        }

        public void DoSomeWork(string workName, IDictionary<string, object> tags)
        {
            // Start a span using the OpenTelemetry API
            using var span = _tracer.StartActivity(workName);
            if (span == null)
            {
                _logger.LogWarning($"OpenTelemetryService::DoSomeWork:Cannot start activity :{workName} ");
            }
            // Decorate the span with additional attributes

            foreach (var item in tags)
            {
                span?.AddTag(item.Key, item.Value);
            }
        }

        public void DoSomeWork(string workName, string eventName, IDictionary<string, object> tags,
            IDictionary<string, object> additionalTags)
        {
            using var span = _tracer.StartActivity(workName)!;
            if (span == null)
            {
                _logger.LogWarning($"OpenTelemetryService::DoSomeWork:Cannot start activity :{workName} ");
            }
            span?.AddEvent(new ActivityEvent(eventName, tags: new ActivityTagsCollection(tags)));
            // Decorate the span with additional attributes

            foreach (var item in additionalTags)
            {
                span?.AddTag(item.Key, item.Value);
            }
        }
    }
}