using Microservices.IoT.API.Exceptions;
using Microservices.IoT.API.Models.Events;
using Microservices.IoT.Data.Models;

using Microsoft.EntityFrameworkCore;

namespace Microservices.IoT.Data.DAOs.Events
{
    public class EventSimulationDAO : DAOBase
    {
        public EventSimulationDAO(IDbContextFactory<MicroservicesContext> factory) : base(factory)
        {
        }

        /// <summary>
        /// Starts a new event of given type for <paramref name="fridgeID"/>
        /// </summary>
        /// <returns>ID of newly created event</returns>
        /// <exception cref="FailedPreconditionException">When such an event is already active</exception>
        public int Begin(int fridgeID, int eventTypeID)
        {
            if (IsActive(fridgeID, eventTypeID))
            {
                throw new FailedPreconditionException();
            }
            EVENT item = new EVENT
            {
                FridgeID = fridgeID,
                From = DateTime.Now,
                TypeID = eventTypeID,
                To = null
            };
            using (var db = DB)
            {
                db.EVENT.Add(item);
                db.SaveChanges();
                return item.ID;
            }
        }

        /// <summary>
        /// Ends an event by setting its <see cref="Event.To"/> field to current date.
        /// </summary>
        /// <param name="fridgeID"></param>
        /// <param name="eventTypeID"></param>
        /// <exception cref="NotFoundException">If no such currently active event exists</exception>
        public void End(int fridgeID, int eventTypeID)
        {
            using (var db = DB)
            {
                var query =
                    from item in db.EVENT
                    where item.FridgeID == fridgeID
                    where item.TypeID == eventTypeID
                    where item.To == null //currently active
                    select item;
                var existing = query.SingleOrDefault();
                if (existing is null)
                {
                    throw new NotFoundException();
                }
                existing.To = DateTime.Now;
                db.SaveChanges();
            }
        }

        public bool IsActive(int fridgeID, int eventTypeID)
        {
            using (var db = DB)
            {
                var query =
                    from item in db.EVENT
                    where item.FridgeID == fridgeID
                    where item.TypeID == eventTypeID
                    where item.To == null //currently active
                    select 1;
                return query.Any();
            }
        }

    }
}
