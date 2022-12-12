using Microservices.IoT.API.Models.Fridges.Info;

namespace Microservices.IoT.API.Models.Fridges
{
    public class FridgeInfo
    {
        public FridgeStaticInfo Static { get; set; }
        public FridgeConfigurableInfo Configurable { get; set; }
        public FridgeSensorInfo Sensors { get; set; }
    }
}
