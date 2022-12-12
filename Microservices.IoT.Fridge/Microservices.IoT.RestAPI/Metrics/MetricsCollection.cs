using App.Metrics;
using App.Metrics.Counter;

namespace Microservices.IoT.ManagementConsole.RestAPI.Metrics
{
    public class MetricsCollection
    {
        public static CounterOptions StartedMicroservicesCounter => new CounterOptions
        {
            Name = "Started sensor microservices",
            Context = "microservicesMainRestAPI",
            MeasurementUnit = Unit.Calls
        };

        public static CounterOptions RunningMicroservicesCounter => new CounterOptions
        {
            Name = "Running sensor microservices",
            Context = "microservicesMainRestAPI",
            MeasurementUnit = Unit.Calls
        };
    }
}
