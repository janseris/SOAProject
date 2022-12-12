using Microservices.IoT.API.Models.Events;

namespace Microservices.IoT.Sensor.RestAPI.Models
{
    public class EventSensorModel
    {
        public string Type { get; set; }

        /// <summary>
        /// Automatically filled when the event starts
        /// </summary>
        public DateTime From { get; set; }

        /// <summary>
        /// Filled when the event ends
        /// </summary>
        public DateTime? To { get; set; }

        public EventSensorModel(Event data)
        {
            Type = data.Type.Name;
            From = data.From;
            To = data.To;
        }
    }
}
