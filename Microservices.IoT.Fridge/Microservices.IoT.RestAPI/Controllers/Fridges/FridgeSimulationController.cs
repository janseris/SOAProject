using Microservices.IoT.API.Services.Fridges;

using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Microservices.IoT.ManagementConsole.RestAPI.Controllers.Fridges
{
    [Route("api/[controller]")]
    [ApiController]
    public class FridgeSimulationController : ControllerBase
    {
        private readonly IFridgeSimulationService service;

        public FridgeSimulationController(IFridgeSimulationService service)
        {
            this.service = service;
        }

        /// <summary>
        /// Sets open flag to true and starts a new event of type "Door open", this can generate a new event "Door open period exceeded" event if the fridge is open for too long
        /// </summary>
        [HttpPut("{ID}/openDoor", Name ="OpenFridgeDoor")]
        public void OpenDoor(int ID)
        {
            service.OpenDoor(ID);
        }

        /// <summary>
        /// Sets fridge's open flag to false.
        /// <br>If there is an active "Door open period exceeded" event active, it is stopped.</br>
        /// </summary>
        [HttpPut("{ID}/closeDoor", Name ="CloseFridgeDoor")]
        public void CloseDoor(int ID)
        {
            service.CloseDoor(ID);
        }

        /// <summary>
        /// Assigns food objects with <paramref name="foodIDs"/> to the fridge
        /// <br>A food item can belong to at most one fridge</br>
        /// <br>Only food items which do not already belong in a fridge can be placed in this fridge (these from food repository).</br>
        /// </summary>
        [HttpPost("{ID}/food", Name ="AssignFoodItemsToFridge")]
        public void AddFood(int ID, List<int> foodIDs)
        {
            service.AddFood(ID, foodIDs);
        }

        /// <summary>
        /// Changes fridge's current temperature. 
        /// <br>This is a simulation which can be then observed on sensors.</br>
        /// </summary>
        /// <param name="ID">fridge</param>
        /// <param name="degrees">new value</param>
        [HttpPut("{ID}/temperature", Name ="SetFridgeCurrentTemperature")]
        public void SetCurrentTemperature(int ID, double degrees)
        {
            service.SetCurrentTemperature(ID, degrees);
        }

        /// <summary>
        /// Changes fridge's current power consumption. 
        /// <br>This is a simulation which can be then observed on sensors.</br>
        /// </summary>
        /// <param name="ID">fridge</param>
        /// <param name="powerConsumption">new value</param>
        [HttpPut("{ID}/powerConsumption", Name = "SetFridgeCurrentPowerConsumption")]
        public void SetCurrentPowerConsumptionWatts(int ID, int powerConsumption)
        {
            service.SetCurrentPowerConsumptionWatts(ID, powerConsumption);
        }
    }
}
