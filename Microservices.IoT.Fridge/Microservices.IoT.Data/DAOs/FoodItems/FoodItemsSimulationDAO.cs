using Microservices.IoT.API.Exceptions;
using Microservices.IoT.Data.Models;

using Microsoft.EntityFrameworkCore;

namespace Microservices.IoT.Data.DAOs.FoodItems
{
    public class FoodItemsSimulationDAO : DAOBase
    {
        public FoodItemsSimulationDAO(IDbContextFactory<MicroservicesContext> factory) : base(factory)
        {
        }

        //put the consume × delete behavior into a service
        public void ChangeCurrentWeight(int ID, int newValue)
        {
            using (var db = DB)
            {
                var item = db.FOOD.Where(item => item.ID == ID).SingleOrDefault();
                if (item is null)
                {
                    throw new NotFoundException();
                }
                item.CurrentWeightGrams = newValue;
                db.SaveChanges();
            }
        }

        public void OpenFoodPackage(int ID)
        {
            using (var db = DB)
            {
                var item = db.FOOD.Where(item => item.ID == ID).SingleOrDefault();
                if (item is null)
                {
                    throw new NotFoundException();
                }
                item.Open = true;
                db.SaveChanges();
            }
        }
    }
}
