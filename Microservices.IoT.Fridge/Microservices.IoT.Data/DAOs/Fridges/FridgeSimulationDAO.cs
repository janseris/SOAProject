using Microservices.IoT.API.Exceptions;
using Microservices.IoT.Data.Models;

using Microsoft.EntityFrameworkCore;

namespace Microservices.IoT.Data.DAOs.Fridges
{
    public class FridgeSimulationDAO : DAOBase
    {
        public FridgeSimulationDAO(IDbContextFactory<MicroservicesContext> factory) : base(factory)
        {
        }

        public void AddFood(int ID, List<int> foodIDs)
        {
            using (var db = DB)
            {
                var query = from f in db.FOOD
                            where foodIDs.Contains(f.ID)
                            select f;
                var items = query.ToList();
                foreach(var item in items)
                {
                    item.FridgeID = ID;
                }
                db.SaveChanges();
            }
        }

        public void SetDoorOpen(int ID, bool value)
        {
            using (var db = DB)
            {
                var query = from f in db.FRIDGE
                            where f.ID == ID
                            select f;
                var item = query.SingleOrDefault();
                if (item is null)
                {
                    throw new NotFoundException();
                }
                item.IsOpen = value;
                db.SaveChanges();
            }
        }

        public void SetCurrentTemperature(int ID, double degrees)
        {
            using (var db = DB)
            {
                var query = from f in db.FRIDGE
                            where f.ID == ID
                            select f;
                var item = query.SingleOrDefault();
                if (item is null)
                {
                    throw new NotFoundException();
                }
                item.CurrentTemperatureDegrees = degrees;
                db.SaveChanges();
            }
        }

        public void SetCurrentPowerConsumption(int ID, int powerConsumption)
        {
            using (var db = DB)
            {
                var query = from f in db.FRIDGE
                            where f.ID == ID
                            select f;
                var item = query.SingleOrDefault();
                if (item is null)
                {
                    throw new NotFoundException();
                }
                item.CurrentPowerConsumptionWatts = powerConsumption;
                db.SaveChanges();
            }
        }
    }
}
