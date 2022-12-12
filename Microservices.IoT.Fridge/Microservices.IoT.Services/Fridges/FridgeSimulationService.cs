using System.Transactions;

using Microservices.IoT.API.Exceptions;
using Microservices.IoT.API.Models.Events;
using Microservices.IoT.API.Services.Fridges;
using Microservices.IoT.Data.DAOs.Events;
using Microservices.IoT.Data.DAOs.Fridges;

namespace Microservices.IoT.Services.Fridges
{
    public class FridgeSimulationService : IFridgeSimulationService
    {
        private readonly FridgeSimulationDAO dao;
        private readonly EventSimulationDAO eventDAO;
        private readonly FridgeSensorDAO sensorDAO;
        private readonly FridgesManagementDAO managementDAO;

        public FridgeSimulationService(FridgeSimulationDAO dao, EventSimulationDAO eventDAO, FridgeSensorDAO sensorDAO, FridgesManagementDAO managementDAO)
        {
            this.dao = dao;
            this.eventDAO = eventDAO;
            this.sensorDAO = sensorDAO;
            this.managementDAO = managementDAO;
        }

        public void AddFood(int ID, List<int> foodIDs)
        {
            dao.AddFood(ID, foodIDs);
        }
       
        public void OpenDoor(int ID)
        {
            using (var transaction = new TransactionScope())
            {
                dao.SetDoorOpen(ID, true);
                eventDAO.Begin(ID, EventType.DOOR_OPEN);
                transaction.Complete(); //commit
            }
        }
     
        public void CloseDoor(int ID)
        {
            using (var transaction = new TransactionScope())
            {
                dao.SetDoorOpen(ID, false);
                eventDAO.End(ID, EventType.DOOR_OPEN);
                transaction.Complete(); //commit
            }
        }

        public void SetCurrentTemperature(int ID, double degrees)
        {
            var name = managementDAO.GetName(ID);
            var info = sensorDAO.GetInfo(name);

            using (var transaction = new TransactionScope())
            {
                dao.SetCurrentTemperature(ID, degrees);
                bool temperatureExceeded = degrees > info.Configurable.ConfiguredOperatingTemperatureDegrees + info.Configurable.DegreesOffsetForTemperatureWarning;
                if (temperatureExceeded)
                {
                    eventDAO.Begin(ID, EventType.TEMPERATURE_EXCEEDED);
                }
                transaction.Complete(); //commit
            }
        }

        public void SetCurrentPowerConsumptionWatts(int ID, int powerConsumption)
        {
            var name = managementDAO.GetName(ID);
            var info = sensorDAO.GetInfo(name);
            using (var transaction = new TransactionScope())
            {
                dao.SetCurrentPowerConsumption(ID, powerConsumption);
                bool powerConsumptionExceeded = powerConsumption > info.Static.DesignedMaximalPowerConsumptionWatts;
                if (powerConsumptionExceeded)
                {
                    eventDAO.Begin(ID, EventType.MAXIMUM_DESIGNER_POWER_CONSUMPTION_EXCEEDED);
                }
                transaction.Complete(); //commit
            }
        }
    }
}
