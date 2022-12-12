using Microservices.IoT.API.Models.FoodItems;
using Microservices.IoT.API.Models.FoodItems.In;
using Microservices.IoT.API.Services.FoodItems;
using Microservices.IoT.Data.DAOs.FoodItems;

namespace Microservices.IoT.Services.FoodItems
{
    public class FoodTypeService : IFoodTypeService
    {
        private readonly FoodTypeDAO dao;

        public FoodTypeService(FoodTypeDAO dao)
        {
            this.dao = dao;
        }

        public int AddNew(FoodTypeCreateDTO item)
        {
            return dao.AddNew(item);
        }

        public List<FoodType> GetAll()
        {
            return dao.GetAll();
        }

        public List<FoodHealthRating> GetAllHealthRatingTypes()
        {
            return dao.GetAllHealthRatingTypes();
        }

        public void Replace(int ID, FoodTypeUpdateDTO newData)
        {
            dao.Replace(ID, newData);
        }
    }
}
