using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Diagnostics.Metrics;
using System.Net.Http;
using System.Threading.Tasks;

namespace LogCorner.EduSync.Speech.Telemetry
{
    public class OpenTelemetryService : IOpenTelemetryService
    {
        private readonly IConfiguration _configuration;
        private readonly ILogger<OpenTelemetryService> _logger;
        private readonly IHttpContextAccessor _httpContextAccessor;

        private string SourceName => _configuration["OpenTelemetry:SourceName"];

        // An ActivitySource is .NET's term for an OpenTelemetry Tracer.
        // Spans generated from this ActivitySource are associated with the ActivitySource's name and version.
        private readonly ActivitySource _activitySource;

        // var activitySource = new ActivitySource("SampleActivitySource");

        public OpenTelemetryService(IConfiguration configuration, ILogger<OpenTelemetryService> logger,
            IHttpContextAccessor httpContextAccessor)
        {
            _configuration = configuration;
            _logger = logger;
            _httpContextAccessor = httpContextAccessor;
            _activitySource = new ActivitySource(SourceName);
        }

        public void DoSomeWork(string workName, IDictionary<string, object> tags)
        {
            // Start a span using the OpenTelemetry API
            using var span = _activitySource.StartActivity(workName);
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
            using var span = _activitySource.StartActivity(workName)!;
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

        public async Task Trace(string workName, IDictionary<string, object> tags,
            IDictionary<string, object> baggages)
        {
            // The sampleActivity is automatically linked to the parent activity (the one from
            // ASP.NET Core in this case).
            // You can get the current activity using Activity.Current.
            using var activity = _activitySource.StartActivity(workName, ActivityKind.Server);
            // note that "sampleActivity" can be null here if nobody listen events generated
            // by the "SampleActivitySource" activity source.
            // sampleActivity?.AddTag("Name", name);
            foreach (var (key, value) in tags)
            {
                activity?.AddTag(key, value);
            }
            foreach (var (key, value) in baggages)
            {
                activity?.AddTag(key, value);
            }

            await Task.CompletedTask;
        }

        public async Task<string> Metric(Uri uri, IDictionary<string, object> tags)
        {
            var httpClient = new HttpClient();
            var meter = new Meter("MyApplicationMetrics");
            var requestCounter = meter.CreateCounter<int>("compute_requests");
            requestCounter.Add(1);

            using var activity = _activitySource.StartActivity("Get data");
            // Add data the the activity
            // You can see these data in Zipkin
            foreach (var (key, value) in tags)
            {
                activity?.AddTag(key, value);
            }

            // Http calls are tracked by AddHttpClientInstrumentation
            var result = await httpClient.GetStringAsync(uri);

            _logger.LogInformation("Response length: {Length}", result.Length);

            return result;
        }
    }
}