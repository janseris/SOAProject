using Microservices.IoT.API.Models.FoodItems.In;
using Microservices.IoT.API.Models.FoodItems;
using Microservices.IoT.Data.Models;

using Microsoft.EntityFrameworkCore;
using Microservices.IoT.API.Exceptions;

namespace Microservices.IoT.Data.DAOs.FoodItems
{
    public class FoodTypeDAO : DAOBase
    {
        public FoodTypeDAO(IDbContextFactory<MicroservicesContext> factory) : base(factory)
        {
        }

        public int AddNew(FoodTypeCreateDTO data)
        {
            var item = new FOOD_TYPE
            {
                Name = data.Name,
                Density = data.Density,
                HealthRatingID = data.HealthRatingID,
                ImmutableVolumeHint = data.ImmutableVolumeHint,
            };
            using (var db = DB)
            {
                db.FOOD_TYPE.Add(item);
                db.SaveChanges();
                return item.ID;
            }
        }

        public List<FoodType> GetAll()
        {
            using (var db = DB)
            {
                var view = db.FOOD_TYPE.Include(ft => ft.HealthRating);
                var query = from item in view
                            select new FoodType
                            {
                                ID = item.ID,
                                Name = item.Name,
                                Density = item.Density,
                                ImmutableVolumeHint = item.ImmutableVolumeHint,
                                HealthRating = new FoodHealthRating
                                {
                                    ID = item.HealthRating.ID,
                                    Name = item.HealthRating.Name,
                                }
                            };
                return query.ToList();
            }
        }

        public List<FoodHealthRating> GetAllHealthRatingTypes()
        {
            using (var db = DB)
            {
                var query = from item in db.FOOD_HEALTH_RATING
                            select new FoodHealthRating
                            {
                                ID = item.ID,
                                Name = item.Name
                            };
                return query.ToList();
            }
        }

        public void Replace(int ID, FoodTypeUpdateDTO data)
        {
            using (var db = DB)
            {
                var query =
                    from item in db.FOOD_TYPE
                    where item.ID == ID
                    select item;
                var existing = query.FirstOrDefault();
                if (existing is null)
                {
                    throw new NotFoundException();
                }
                existing.Density = data.Density;
                existing.HealthRatingID = data.HealthRatingID;
                existing.ImmutableVolumeHint = data.ImmutableVolumeHint;
                db.SaveChanges();
            }
        }
    }
}
