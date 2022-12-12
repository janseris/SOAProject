namespace Microservices.IoT.API.Services.Events
{
    /// <summary>
    /// Used to simulate occurence of events in the system.
    /// <br>Fridge sensor uses this API as well to create events based on a fridge property value change (temperature, power consumption, open period exceeded)</br>
    /// </summary>
    public interface IEventSimulationService
    {
        /// <summary>
        /// Creates a new event for the fridge starting from <see cref="DateTime.Now"/>
        /// </summary>
        /// <returns></returns>
        int Begin(int fridgeID, int eventTypeID);

        /// <summary>
        /// Sets <see cref="Event.From"/> to <see cref="DateTime.Now"/>
        /// </summary>
        void End(int fridgeID, int eventTypeID);

        bool IsActive(int fridgeID, int eventTypeID);
    }
}
