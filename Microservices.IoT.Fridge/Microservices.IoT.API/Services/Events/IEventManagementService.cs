using System.Collections.Generic;
using Microservices.IoT.API.Models.Events;

namespace Microservices.IoT.API.Services.Events
{
    public interface IEventManagementService
    {
        /// <summary>
        /// Returns all events for a fridge <paramref name="fridgeID"/> (past and present)
        /// </summary>
        List<Event> GetAll(int fridgeID);

        /// <summary>
        /// Deletes event <paramref name="ID"/>
        /// </summary>
        void Delete(int ID);

        /// <summary>
        /// Deletes all events for fridge <paramref name="fridgeID"/>
        /// </summary>
        void DeleteAll(int fridgeID);

        /// <summary>
        /// Gets a list of event types which can be used to create a new event.
        /// </summary>
        List<EventType> GetEventTypes();
    }
}
