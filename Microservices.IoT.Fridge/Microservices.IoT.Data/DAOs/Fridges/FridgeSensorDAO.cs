using Microservices.IoT.API.Exceptions;
using Microservices.IoT.API.Models.Events;
using Microservices.IoT.API.Models.FoodItems;
using Microservices.IoT.API.Models.Fridges;
using Microservices.IoT.API.Models.Fridges.Info;
using Microservices.IoT.Data.Models;

using Microsoft.EntityFrameworkCore;

namespace Microservices.IoT.Data.DAOs.Fridges
{
    public class FridgeSensorDAO : DAOBase
    {
        public FridgeSensorDAO(IDbContextFactory<MicroservicesContext> factory) : base(factory)
        {
        }

        public FridgeInfo GetInfo(string fridgeName)
        {
            using (var db = DB)
            {
                var query = from f in db.FRIDGE
                            where f.Name == fridgeName
                            select new FridgeInfo
                            {
                                Configurable = new FridgeConfigurableInfo
                                {
                                    ConfiguredOperatingTemperatureDegrees = f.ConfiguredOperatingTemperatureDegrees,
                                    DoorOpenPeriodSecondsForWarning = f.DoorOpenPeriodSecondsForWarning,
                                },
                                Static = new FridgeStaticInfo
                                {
                                    CapacityLiters = f.CapacityLiters,
                                    DesignedMaximalPowerConsumptionWatts = f.DesignedMaximalPowerConsumptionWatts
                                },
                                Sensors = new FridgeSensorInfo
                                {
                                    IsOpen = f.IsOpen,
                                    PowerConsumptionWatts = f.CurrentPowerConsumptionWatts,
                                    TemperatureDegrees = f.CurrentTemperatureDegrees
                                }
                            };
                var item = query.SingleOrDefault();
                if (item is null)
                {
                    throw new NotFoundException();
                }
                return item;
            }
        }

        public FridgeSensorInfo GetSensorInfo(string fridgeName)
        {
            using (var db = DB)
            {
                var query = from f in db.FRIDGE
                            where f.Name == fridgeName
                            select new FridgeSensorInfo
                            {
                                IsOpen = f.IsOpen,
                                PowerConsumptionWatts = f.CurrentPowerConsumptionWatts,
                                TemperatureDegrees = f.CurrentTemperatureDegrees
                            };
                var item = query.SingleOrDefault();
                if (item is null)
                {
                    throw new NotFoundException();
                }
                return item;
            }
        }

        public List<Event> GetCurrentEvents(string fridgeName)
        {
            using (var db = DB)
            {
                var view = db.EVENT.Include(e => e.Type).Include(e => e.Fridge);
                var query = from item in view
                            where item.Fridge.Name == fridgeName
                            where item.To == null
                            select new Event
                            {
                                ID = item.ID,
                                From = item.From,
                                To = item.To,
                                Type = new EventType
                                {
                                    ID = item.Type.ID,
                                    Name = item.Type.Name
                                }
                            };
                return query.ToList();
            }
        }

        public int GetExpiredFoodItemsCount(string fridgeName)
        {
            using (var db = DB)
            {
                var query = from item in db.FRIDGE
                            where item.Name == fridgeName
                            select item.FOOD.Count();
                var result = query.ToList();
                if (result.Count == 0)
                {
                    throw new NotFoundException();
                }
                return result.First();
            }
        }

        public List<Food> GetFood(string fridgeName)
        {
            using (var db = DB)
            {
                var view =
                    db.FOOD
                    .Include(f => f.Type).ThenInclude(t => t.HealthRating)
                    .Include(f => f.Fridge);
                var query = from item in view
                            where item.Fridge.Name == fridgeName
                            select new Food
                            {
                                ID = item.ID,
                                Name = item.Name,
                                Producer = item.Producer,
                                CurrentWeightGrams = item.CurrentWeightGrams,
                                CreatedDate = item.CreatedDate,
                                ExpirationDate = item.ExpirationDate,
                                HasImmutableVolume = item.ImmutableVolume,
                                Open = item.Open,
                                InitialWeightGrams = item.InitialWeightGrams,
                                Type = new FoodType
                                {
                                    ID = item.Type.ID,
                                    Name = item.Type.Name,
                                    Density = item.Type.Density,
                                    HealthRating = new FoodHealthRating
                                    {
                                        ID = item.Type.HealthRating.ID,
                                        Name = item.Type.HealthRating.Name
                                    },
                                    ImmutableVolumeHint = item.Type.ImmutableVolumeHint
                                }
                            };
                return query.ToList();
            }
        }

        public List<FoodHealthRating> GetFoodHealthStats(string fridgeName)
        {
            using (var db = DB)
            {
                var view =
                    db.FOOD
                    .Include(f => f.Type).ThenInclude(t => t.HealthRating)
                    .Include(f => f.Fridge);
                var query =
                    from item in view
                    where item.Fridge.Name == fridgeName
                    select new FoodHealthRating
                    {
                        ID = item.Type.HealthRating.ID,
                        Name = item.Type.HealthRating.Name
                    };
                return query.ToList();
            }
        }
    }
}
