using System.Collections.Generic;

namespace LogCorner.EduSync.Speech.Telemetry
{
    public interface IOpenTelemetryService
    {
        void DoSomeWork(string workName, IDictionary<string, object> tags);

        void DoSomeWork(string workName, string eventName, IDictionary<string, object> tags,
            IDictionary<string, object> additionalTags);
    }
}