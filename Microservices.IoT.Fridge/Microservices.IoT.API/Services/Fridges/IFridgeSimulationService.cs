using System.Collections.Generic;

namespace Microservices.IoT.API.Services.Fridges
{
    /// <summary>
    /// Used to simulate behavior on a fridge to generate some events or change values
    /// </summary>
    public interface IFridgeSimulationService
    {
        /// <summary>
        /// Sets open flag to true and starts a new event of type "Door open", this can generate a new event "Door open period exceeded" event if the fridge is open for too long
        /// </summary>
        /// <param name="ID"></param>
        void OpenDoor(int ID);

        /// <summary>
        /// Sets fridge's open flag to false.
        /// <br>If there is an active "Door open period exceeded" event active, it is stopped.</br>
        /// </summary>
        /// <param name="ID"></param>
        void CloseDoor(int ID);

        /// <summary>
        /// Assigns food objects with <paramref name="foodIDs"/> to the fridge
        /// <br>A food item can belong to at most one fridge</br>
        /// <br>Only food items which do not already belong in a fridge can be placed in this fridge (these from food repository).</br>
        /// </summary>
        void AddFood(int ID, List<int> foodIDs);

        /// <summary>
        /// Changes fridge's current temperature. 
        /// <br>This is a simulation which can be then observed on sensors.</br>
        /// </summary>
        /// <param name="ID">fridge</param>
        /// <param name="degrees">new value</param>
        void SetCurrentTemperature(int ID, double degrees);

        /// <summary>
        /// Changes fridge's current power consumption. 
        /// <br>This is a simulation which can be then observed on sensors.</br>
        /// </summary>
        /// <param name="ID">fridge</param>
        /// <param name="powerConsumption">new value</param>
        void SetCurrentPowerConsumptionWatts(int ID, int powerConsumption);
    }
}
