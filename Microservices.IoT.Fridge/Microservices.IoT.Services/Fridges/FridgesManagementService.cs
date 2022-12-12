using Microservices.IoT.API.Models.Fridges;
using Microservices.IoT.API.Models.Fridges.In;
using Microservices.IoT.API.Models.Fridges.Info;
using Microservices.IoT.API.Services.Fridges;
using Microservices.IoT.Data.DAOs.Fridges;

namespace Microservices.IoT.Services.Fridges
{
    public class FridgesManagementService : IFridgesManagementService
    {
        private readonly FridgesManagementDAO dao;

        public FridgesManagementService(FridgesManagementDAO dao)
        {
            this.dao = dao;
        }

        public int Create(FridgeCreateDTO item)
        {
            return dao.Insert(item);
        }

        public void Delete(int ID)
        {
            dao.Delete(ID);
        }

        public List<FridgeType> GetTypes()
        {
            return dao.GetTypes();
        }

        public Fridge Get(int ID)
        {
            return dao.Get(ID);
        }

        public List<Fridge> GetAll()
        {
            return dao.GetAll();
        }

        public void SetDoorOpenWarningPeriodSeconds(int ID, int seconds)
        {
            dao.SetDoorOpenWarningPeriodSeconds(ID, seconds);
        }

        public void SetOffsetDegreesForTemperatureWarning(int ID, double degrees)
        {
            dao.SetOffsetDegreesForTemperatureWarning(ID, degrees); 
        }

        public void SetOperatingTemperature(int ID, double degrees)
        {
            dao.SetOperatingTemperature(ID, degrees);
        }

        public void UpdateStaticInfo(int ID, FridgeStaticInfo data)
        {
            dao.UpdateStaticInfo(ID, data);
        }
    }
}
