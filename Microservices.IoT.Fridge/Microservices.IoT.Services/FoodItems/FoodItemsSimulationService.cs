using Microservices.IoT.API.Services.FoodItems;
using Microservices.IoT.Data.DAOs.FoodItems;

namespace Microservices.IoT.Services.FoodItems
{
    public class FoodItemsSimulationService : IFoodItemsSimulationService
    {
        private readonly FoodItemsManagementDAO dao;
        private readonly FoodItemsSimulationDAO simulationDAO;

        public FoodItemsSimulationService(FoodItemsManagementDAO dao, FoodItemsSimulationDAO simulationDAO)
        {
            this.dao = dao;
            this.simulationDAO = simulationDAO;
        }

        public void Consume(int ID, int amountPercent)
        {
            if(amountPercent <= 0 || amountPercent > 100)
            {
                throw new ArgumentException(nameof(amountPercent));
            }
            var item = dao.Get(ID);
            var consumptionGrams = item.InitialWeightGrams * amountPercent;
            var weightAfterConsumption = item.CurrentWeightGrams - consumptionGrams;
            if (weightAfterConsumption < 0)
            {
                dao.Delete(ID); //delete
            }
            else
            {
                simulationDAO.ChangeCurrentWeight(ID, weightAfterConsumption);
            }
        }

        public void OpenFoodPackage(int ID)
        {
            simulationDAO.OpenFoodPackage(ID);
        }
    }
}
