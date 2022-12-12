namespace Microservices.IoT.API.Models.Fridges.Info
{
    public class FridgeConfigurableInfo
    {
        public double ConfiguredOperatingTemperatureDegrees { get; set; }
        public int DoorOpenPeriodSecondsForWarning { get; set; }
        public double DegreesOffsetForTemperatureWarning { get; set; }
    }
}
