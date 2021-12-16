using System.Collections.Generic;
using System.Diagnostics;
using Microsoft.Extensions.Configuration;

namespace LogCorner.EduSync.Speech.Presentation.Controllers
{
    public class OpenTelemetryService : IOpenTelemetryService
    {
        private readonly IConfiguration _configuration;

        // An ActivitySource is .NET's term for an OpenTelemetry Tracer.
        // Spans generated from this ActivitySource are associated with the ActivitySource's name and version.
        private ActivitySource _tracer;
        public OpenTelemetryService(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        public void DoSomeWork(string workName, IDictionary<string, object> tags)
        {
            var sourceName = _configuration["OpenTelemetry:SourceName"];
            _tracer = new ActivitySource(sourceName);
            // Start a span using the OpenTelemetry API
            using var span = _tracer.StartActivity(workName, ActivityKind.Internal);

            // Decorate the span with additional attributes

            foreach (var item in tags)
            {
                span?.AddTag(item.Key, item.Value);
            }
        }
    }
}