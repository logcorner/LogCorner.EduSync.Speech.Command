using System.Collections.Generic;

namespace LogCorner.EduSync.Speech.Telemetry
{
    public interface IOpenTelemetryService
    {
        void DoSomeWork(string workName, IDictionary<string, object> tags);
    }
}