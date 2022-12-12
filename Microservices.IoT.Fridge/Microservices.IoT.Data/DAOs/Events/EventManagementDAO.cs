using Microservices.IoT.API.Models.Events;
using Microservices.IoT.Data.Models;

using Microsoft.EntityFrameworkCore;

namespace Microservices.IoT.Data.DAOs.Events
{
    public class EventManagementDAO : DAOBase
    {
        public EventManagementDAO(IDbContextFactory<MicroservicesContext> factory) : base(factory)
        {
        }

        public void Delete(int ID)
        {
            using (var db = DB)
            {
                var existing =
                    from item in db.EVENT
                    where item.ID == ID
                    select item;
                if (existing is null)
                {
                    return;
                }
                db.Remove(existing);
                db.SaveChanges();
            }
        }

        public void DeleteAll(int fridgeID)
        {
            using (var db = DB)
            {
                var existing =
                    from item in db.EVENT
                    where item.FridgeID == fridgeID
                    select item;
                if (existing is null)
                {
                    return;
                }
                db.Remove(existing);
                db.SaveChanges();
            }
        }

        public List<Event> GetAll(int fridgeID)
        {
            using (var db = DB)
            {
                return (from e in db.EVENT
                        where e.FridgeID == fridgeID
                        select new Event
                        {
                            ID = e.ID,
                            From = e.From,
                            To = e.To,
                            Type = new EventType
                            {
                                ID = e.Type.ID,
                                Name = e.Type.Name
                            }
                        }).ToList();
            }
        }

        public List<EventType> GetEventTypes()
        {
            using (var db = DB)
            {
                return (from t in db.EVENT_TYPE
                        select new EventType
                        {
                            ID = t.ID,
                            Name = t.Name
                        }).ToList();
            }
        }
    }
}
