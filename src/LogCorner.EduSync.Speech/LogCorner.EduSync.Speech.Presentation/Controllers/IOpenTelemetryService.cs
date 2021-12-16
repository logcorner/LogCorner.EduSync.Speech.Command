using System.Collections.Generic;

namespace LogCorner.EduSync.Speech.Presentation.Controllers
{
    public interface IOpenTelemetryService
    {
        void DoSomeWork(string workName, IDictionary<string, object> tags);
    }
}