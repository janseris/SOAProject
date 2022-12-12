using Microservices.IoT.API.Models.Fridges.In;
using Microservices.IoT.API.Models.Fridges;
using Microservices.IoT.Data.Models;

using Microsoft.EntityFrameworkCore;
using Microservices.IoT.API.Models.Fridges.Info;
using Microservices.IoT.API.Models.Events;
using Microservices.IoT.API.Models.FoodItems;
using Microservices.IoT.API.Exceptions;

namespace Microservices.IoT.Data.DAOs.Fridges
{
    public class FridgesManagementDAO : DAOBase
    {
        public FridgesManagementDAO(IDbContextFactory<MicroservicesContext> factory) : base(factory)
        {
        }

        public List<FridgeType> GetTypes()
        {
            using (var db = DB)
            {
                var query = from item in db.FRIDGE_TYPE
                            select new FridgeType
                            {
                                ID = item.ID,
                                Name = item.Name,
                            };
                return query.ToList();
            }
        }

        public int Insert(FridgeCreateDTO data)
        {
            var item = new FRIDGE
            {
                Name = data.Name,
                Brand = data.Brand,
                CapacityLiters = data.CapacityLiters,
                ConfiguredOperatingTemperatureDegrees = data.ConfiguredOperatingTemperatureDegrees,
                DesignedMaximalPowerConsumptionWatts = data.DesignedMaximalPowerConsumptionWatts,
                DoorOpenPeriodSecondsForWarning = data.DoorOpenPeriodSecondsForWarning,
                IsOpen = false,
                CurrentTemperatureDegrees = data.ConfiguredOperatingTemperatureDegrees,
                CurrentPowerConsumptionWatts = data.DesignedMaximalPowerConsumptionWatts,
                DegreesOffsetForTemperatureWarning = data.DegreesOffsetForTemperatureWarning,
                TypeID = data.TypeID
            };
            using (var db = DB)
            {
                db.FRIDGE.Add(item);
                db.SaveChanges();
                return item.ID;
            }
        }

        public void Delete(int ID)
        {
            using (var db = DB)
            {
                var existing =
                    from item in db.FRIDGE
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

        public string GetName(int ID)
        {
            using (var db = DB)
            {
                var query =
                    from item in db.FRIDGE
                    where item.ID == ID
                    select item.Name;
                var f = query.SingleOrDefault();
                if (f is null)
                {
                    throw new NotFoundException();
                }
                return f;
            }
        }

        public Fridge Get(int ID)
        {
            using (var db = DB)
            {
                var view =
                    db.FRIDGE
                    .Include(f => f.FOOD).ThenInclude(food => food.Type).ThenInclude(ft => ft.HealthRating)
                    .Include(f => f.EVENT).ThenInclude(e => e.Type)
                    .Include(f => f.Type);
                var query =
                    from item in view
                    where item.ID == ID
                    select new Fridge
                    {
                        ID = item.ID,
                        Name = item.Name,
                        Brand = item.Brand,
                        Events = (from e in item.EVENT
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
                                  }).ToList(),
                        Food = (from food in item.FOOD
                                select new Food
                                {
                                    ID = food.ID,
                                    Name = food.Name,
                                    Producer = food.Producer,
                                    CurrentWeightGrams = food.CurrentWeightGrams,
                                    CreatedDate = food.CreatedDate,
                                    ExpirationDate = food.ExpirationDate,
                                    HasImmutableVolume = food.ImmutableVolume,
                                    Open = food.Open,
                                    InitialWeightGrams = food.InitialWeightGrams,
                                    Type = new FoodType
                                    {
                                        ID = food.Type.ID,
                                        Name = food.Type.Name,
                                        Density = food.Type.Density,
                                        HealthRating = new FoodHealthRating
                                        {
                                            ID = food.Type.HealthRating.ID,
                                            Name = food.Type.HealthRating.Name
                                        },
                                        ImmutableVolumeHint = food.Type.ImmutableVolumeHint
                                    }
                                }).ToList(),
                        Info = new FridgeInfo
                        {
                            Configurable = new FridgeConfigurableInfo
                            {
                                ConfiguredOperatingTemperatureDegrees = item.ConfiguredOperatingTemperatureDegrees,
                                DoorOpenPeriodSecondsForWarning = item.DoorOpenPeriodSecondsForWarning,
                            },
                            Static = new FridgeStaticInfo
                            {
                                CapacityLiters = item.CapacityLiters,
                                DesignedMaximalPowerConsumptionWatts = item.DesignedMaximalPowerConsumptionWatts
                            },
                            Sensors = new FridgeSensorInfo
                            {
                                IsOpen = item.IsOpen,
                                PowerConsumptionWatts = item.CurrentPowerConsumptionWatts,
                                TemperatureDegrees = item.CurrentTemperatureDegrees
                            }
                        },
                        Type = new FridgeType
                        {
                            ID = item.Type.ID,
                            Name = item.Type.Name,
                        }
                    };
                var f = query.SingleOrDefault();
                if (f is null)
                {
                    throw new NotFoundException();
                }
                return f;
            }
        }

        public List<Fridge> GetAll()
        {
            using (var db = DB)
            {
                var view =
                    db.FRIDGE
                    .Include(f => f.FOOD).ThenInclude(food => food.Type).ThenInclude(ft => ft.HealthRating)
                    .Include(f => f.EVENT).ThenInclude(e => e.Type)
                    .Include(f => f.Type);
                var query =
                    from item in view
                    select new Fridge
                    {
                        ID = item.ID,
                        Name = item.Name,
                        Brand = item.Brand,
                        Events = (from e in item.EVENT
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
                                  }).ToList(),
                        Food = (from food in item.FOOD
                                select new Food
                                {
                                    ID = food.ID,
                                    Name = food.Name,
                                    Producer = food.Producer,
                                    CurrentWeightGrams = food.CurrentWeightGrams,
                                    CreatedDate = food.CreatedDate,
                                    ExpirationDate = food.ExpirationDate,
                                    HasImmutableVolume = food.ImmutableVolume,
                                    Open = food.Open,
                                    InitialWeightGrams = food.InitialWeightGrams,
                                    Type = new FoodType
                                    {
                                        ID = food.Type.ID,
                                        Name = food.Type.Name,
                                        Density = food.Type.Density,
                                        HealthRating = new FoodHealthRating
                                        {
                                            ID = food.Type.HealthRating.ID,
                                            Name = food.Type.HealthRating.Name
                                        },
                                        ImmutableVolumeHint = food.Type.ImmutableVolumeHint
                                    }
                                }).ToList(),
                        Info = new FridgeInfo
                        {
                            Configurable = new FridgeConfigurableInfo
                            {
                                ConfiguredOperatingTemperatureDegrees = item.ConfiguredOperatingTemperatureDegrees,
                                DoorOpenPeriodSecondsForWarning = item.DoorOpenPeriodSecondsForWarning,
                            },
                            Static = new FridgeStaticInfo
                            {
                                CapacityLiters = item.CapacityLiters,
                                DesignedMaximalPowerConsumptionWatts = item.DesignedMaximalPowerConsumptionWatts
                            },
                            Sensors = new FridgeSensorInfo
                            {
                                IsOpen = item.IsOpen,
                                PowerConsumptionWatts = item.CurrentPowerConsumptionWatts,
                                TemperatureDegrees = item.CurrentTemperatureDegrees
                            }
                        },
                        Type = new FridgeType
                        {
                            ID = item.Type.ID,
                            Name = item.Type.Name,
                        }
                    };
                return query.ToList();
            }
        }

        public void SetDoorOpenWarningPeriodSeconds(int ID, int seconds)
        {
            using (var db = DB)
            {
                var query =
                    from f in db.FRIDGE
                    where f.ID == ID
                    select f;
                var item = query.SingleOrDefault();
                if (item is null)
                {
                    throw new NotFoundException();
                }
                item.DoorOpenPeriodSecondsForWarning = seconds;
                db.SaveChanges();
            }
        }

        public void SetOffsetDegreesForTemperatureWarning(int ID, double degrees)
        {
            using (var db = DB)
            {
                var query =
                    from f in db.FRIDGE
                    where f.ID == ID
                    select f;
                var item = query.SingleOrDefault();
                if (item is null)
                {
                    throw new NotFoundException();
                }
                item.DegreesOffsetForTemperatureWarning = degrees;
                db.SaveChanges();
            }
        }

        public void SetOperatingTemperature(int ID, double degrees)
        {
            using (var db = DB)
            {
                var query =
                    from f in db.FRIDGE
                    where f.ID == ID
                    select f;
                var item = query.SingleOrDefault();
                if (item is null)
                {
                    throw new NotFoundException();
                }
                item.ConfiguredOperatingTemperatureDegrees = degrees;
                db.SaveChanges();
            }
        }

        public void UpdateStaticInfo(int ID, FridgeStaticInfo data)
        {
            using (var db = DB)
            {
                var query =
                    from f in db.FRIDGE
                    where f.ID == ID
                    select f;
                var item = query.SingleOrDefault();
                if (item is null)
                {
                    throw new NotFoundException();
                }
                item.DesignedMaximalPowerConsumptionWatts = data.DesignedMaximalPowerConsumptionWatts;
                item.CapacityLiters = data.CapacityLiters;
                db.SaveChanges();
            }
        }

    }
}
