namespace Microservices.IoT.API.Models.Fridges.Info
{
    public class FridgeSensorInfo
    {
        public double TemperatureDegrees { get; set; }
        public int PowerConsumptionWatts { get; set; }
        public bool IsOpen { get; set; }
    }
}
