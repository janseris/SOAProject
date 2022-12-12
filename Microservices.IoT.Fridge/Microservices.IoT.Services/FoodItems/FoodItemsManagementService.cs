using Microservices.IoT.API.Models.FoodItems;
using Microservices.IoT.API.Models.FoodItems.In;
using Microservices.IoT.API.Services.FoodItems;
using Microservices.IoT.Data.DAOs.FoodItems;

namespace Microservices.IoT.Services.FoodItems
{
    public class FoodItemsManagementService : IFoodItemsManagementService
    {
        private readonly FoodItemsManagementDAO dao;

        public FoodItemsManagementService(FoodItemsManagementDAO dao)
        {
            this.dao = dao;
        }

        public Food Get(int ID)
        {
            return dao.Get(ID);
        }

        public List<Food> GetAll()
        {
            return dao.GetAll();
        }

        public void ChangeExpirationDate(int ID, DateTime newDate)
        {
            dao.ChangeExpirationDate(ID, newDate);
        }

        public int Create(FoodCreateDTO item)
        {
            return dao.Create(item);
        }

        public int Duplicate(int ID)
        {
            return dao.Duplicate(ID);
        }

        public void Remove(int ID)
        {
            dao.Delete(ID);
        }
    }
}
