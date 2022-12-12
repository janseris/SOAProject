using Microservices.IoT.API.Models.Events;
using Microservices.IoT.API.Services.Events;
using Microservices.IoT.Data.DAOs.Events;

namespace Microservices.IoT.Services.Events
{
    public class EventManagementService : IEventManagementService
    {
        private readonly EventManagementDAO dao;

        public EventManagementService(EventManagementDAO dao)
        {
            this.dao = dao;
        }

        public void Delete(int ID)
        {
            dao.Delete(ID);
        }

        public void DeleteAll(int fridgeID)
        {
            dao.DeleteAll(fridgeID);
        }

        public List<Event> GetAll(int fridgeID)
        {
            return dao.GetAll(fridgeID);
        }

        public List<EventType> GetEventTypes()
        {
            return dao.GetEventTypes();
        }
    }
}
