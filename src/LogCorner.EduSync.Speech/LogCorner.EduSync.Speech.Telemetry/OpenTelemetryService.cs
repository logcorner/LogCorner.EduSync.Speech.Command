using Microsoft.Extensions.Configuration;
using System.Collections.Generic;
using System.Diagnostics;

namespace LogCorner.EduSync.Speech.Telemetry
{
    public class OpenTelemetryService : IOpenTelemetryService
    {
        private readonly IConfiguration _configuration;
        private string SourceName => _configuration["OpenTelemetry:SourceName"];

        // An ActivitySource is .NET's term for an OpenTelemetry Tracer.
        // Spans generated from this ActivitySource are associated with the ActivitySource's name and version.
        private readonly ActivitySource _tracer;

        public OpenTelemetryService(IConfiguration configuration)
        {
            _configuration = configuration;
            _tracer = new ActivitySource(SourceName);
        }

        public void DoSomeWork(string workName, IDictionary<string, object> tags)
        {
            // Start a span using the OpenTelemetry API
            using var span = _tracer.StartActivity(workName);

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

            span?.AddEvent(new ActivityEvent(eventName, tags: new ActivityTagsCollection(tags)));
            // Decorate the span with additional attributes

            foreach (var item in additionalTags)
            {
                span?.AddTag(item.Key, item.Value);
            }
        }
    }
}