using System.Diagnostics;
using Microsoft.Extensions.Configuration;

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

        public void DoSomeWork(string workName, System.Collections.Generic.IDictionary<string, object> tags)
        {
            // Start a span using the OpenTelemetry API
            using var span = _tracer.StartActivity(workName);

            // Decorate the span with additional attributes

            foreach (var item in tags)
            {
                span?.AddTag(item.Key, item.Value);
            }
        }
    }
}