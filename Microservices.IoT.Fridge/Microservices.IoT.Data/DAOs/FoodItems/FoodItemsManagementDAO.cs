using Microservices.IoT.API.Models.FoodItems.In;
using Microservices.IoT.API.Models.FoodItems;
using Microservices.IoT.Data.Models;

using Microsoft.EntityFrameworkCore;
using Microservices.IoT.API.Exceptions;

namespace Microservices.IoT.Data.DAOs.FoodItems
{
    public class FoodItemsManagementDAO : DAOBase
    {
        public FoodItemsManagementDAO(IDbContextFactory<MicroservicesContext> factory) : base(factory)
        {
        }

        public Food Get(int ID)
        {
            using (var db = DB)
            {
                var view = db.FOOD.Include(f => f.Type).ThenInclude(t => t.HealthRating);
                var query = from item in view
                            where item.ID == ID
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
                return query.FirstOrDefault();
            }
        }

        public List<Food> GetAll()
        {
            using (var db = DB)
            {
                var view = db.FOOD.Include(f => f.Type).ThenInclude(t => t.HealthRating);
                var query = from item in view
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

        public int Create(FoodCreateDTO data)
        {
            var item = new FOOD()
            {
                CreatedDate = DateTime.Now,
                InitialWeightGrams = data.WeightGrams,
                CurrentWeightGrams = data.WeightGrams,
                ExpirationDate = data.ExpirationDate,
                ImmutableVolume = data.ImmutableVolume,
                Name = data.Name,
                Open = data.Open,
                Producer = data.Producer,
                TypeID = data.TypeID,
                FridgeID = null
            };
            using (var db = DB)
            {
                db.Add(item);
                db.SaveChanges();
                return item.ID;
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <exception cref="NotFoundException"></exception>
        public void Delete(int ID)
        {
            using (var db = DB)
            {
                var item = db.FOOD.FirstOrDefault(x => x.ID == ID);
                if (item is null)
                {
                    throw new NotFoundException();
                }
                db.Remove(item);
                db.SaveChanges();
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <exception cref="NotFoundException"></exception>
        public int Duplicate(int ID)
        {
            using (var db = DB)
            {
                var item = db.FOOD.FirstOrDefault(x => x.ID == ID);
                if (item is null)
                {
                    throw new NotFoundException();
                }
                var copy = new FOOD
                {
                    Name = item.Name,
                    Producer = item.Producer,
                    CreatedDate = DateTime.Now,
                    ExpirationDate = item.ExpirationDate,
                    ImmutableVolume = item.ImmutableVolume,
                    TypeID = item.TypeID,
                    InitialWeightGrams = item.InitialWeightGrams,
                    CurrentWeightGrams = item.InitialWeightGrams,
                    Open = false,
                    FridgeID = null
                };
                db.Add(copy);
                db.SaveChanges();
                return copy.ID;
            }
        }

        public void ChangeExpirationDate(int ID, DateTime newDate)
        {
            using (var db = DB)
            {
                var item = db.FOOD.FirstOrDefault(item => item.ID == ID);
                if (item is null)
                {
                    return;
                }
                item.ExpirationDate = newDate;
                db.SaveChanges();
            }
        }

    }
}
