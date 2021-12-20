using System;
using Serilog.Core;
using Serilog.Events;

namespace LogCorner.EduSync.Speech.Telemetry
{
    public class SeriLogEventSink : ILogEventSink
    {
        private readonly IFormatProvider _formatProvider;

        public SeriLogEventSink(IFormatProvider formatProvider)
        {
            _formatProvider = formatProvider;
        }

        public void Emit(LogEvent logEvent)
        {
           
        }
    }
}