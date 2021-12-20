using System;
using Serilog;
using Serilog.Configuration;

namespace LogCorner.EduSync.Speech.Telemetry
{
    public static class SeriLogEventSinkExtensions
    {
        public static LoggerConfiguration NewRelicLogEventSink(
            this LoggerSinkConfiguration loggerConfiguration,
            IFormatProvider formatProvider = null)
        {
            return loggerConfiguration.Sink(new SeriLogEventSink(formatProvider));
        }
    }
}