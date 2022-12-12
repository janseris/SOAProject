using System;

namespace Microservices.IoT.API.Models.Events
{
    /// <summary>
    /// A negative event occuring in/on a fridge.
    /// <br>Only one instance of an <see cref="Event"/> of given <see cref="EventType"/> may occur at once in one fridge.</br>
    /// <br>Some events are automatically created by the system when a property value changes.</br>
    /// </summary>
    public class Event
    {
        public int ID { get; set; }
        public EventType Type { get; set; }

        /// <summary>
        /// Automatically filled when the event starts
        /// </summary>
        public DateTime From { get; set; }

        /// <summary>
        /// Filled when the event ends
        /// </summary>
        public DateTime? To { get; set; }
    }
}
