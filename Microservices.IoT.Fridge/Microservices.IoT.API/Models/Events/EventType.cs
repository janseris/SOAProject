namespace Microservices.IoT.API.Models.Events
{
    public class EventType
    {
        public int ID { get; set; }
        public string Name { get; set; }

        public const int ENGINE_OVERHEATING = 1;
        public const int POWER_OUTAGE = 2;
        public const int TEMPERATURE_EXCEEDED = 3;
        public const int LIGHT_ISSUE = 4;
        public const int SEALING_ISSUE = 5;
        public const int MAXIMUM_DESIGNER_POWER_CONSUMPTION_EXCEEDED = 6;
        public const int DOOR_OPEN_WARNING_PERIOD_SECONDS_EXCEEDED = 7;
        public const int DOOR_OPEN = 8;

    }
}
