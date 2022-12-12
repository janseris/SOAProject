using Microservices.IoT.API.Services.Events;
using Microservices.IoT.Data.DAOs.Events;

namespace Microservices.IoT.Services.Events
{
    public class EventSimulationService : IEventSimulationService
    {
        private readonly EventSimulationDAO dao;

        public EventSimulationService(EventSimulationDAO dao)
        {
            this.dao = dao;
        }

        public int Begin(int fridgeID, int eventTypeID)
        {
            return dao.Begin(fridgeID, eventTypeID);
        }

        public void End(int fridgeID, int eventTypeID)
        {
            dao.End(fridgeID, eventTypeID);
        }

        public bool IsActive(int fridgeID, int eventTypeID)
        {
            return dao.IsActive(fridgeID, eventTypeID);
        }
    }
}
