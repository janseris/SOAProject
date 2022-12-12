namespace Microservices.IoT.API.Models.Fridges.In
{
    public class FridgeCreateDTO
    {
        public string Name { get; set; }
        public string Brand { get; set; }
        public int TypeID { get; set; }
        public int DesignedMaximalPowerConsumptionWatts { get; set; }
        public int CapacityLiters { get; set; }
        public double ConfiguredOperatingTemperatureDegrees { get; set; }
        public int DoorOpenPeriodSecondsForWarning { get; set; }

        /// <summary>
        /// By how many degrees the <see cref="ConfiguredOperatingTemperatureDegrees"/> can be exceeded to generate a "high temperature" event
        /// </summary>
        public double DegreesOffsetForTemperatureWarning { get; set; }
    }
}
