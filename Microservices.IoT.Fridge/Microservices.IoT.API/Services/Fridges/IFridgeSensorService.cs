using System.Collections.Generic;
using Microservices.IoT.API.Models.Events;
using Microservices.IoT.API.Models.FoodItems;
using Microservices.IoT.API.Models.Fridges;
using Microservices.IoT.API.Models.Fridges.Info;

namespace Microservices.IoT.API.Services.Fridges
{
    /// <summary>
    /// List of functionality which a a fridge sensor provides
    /// <br>This will be implemented by a microservice representing a fridge sensor</br>
    /// </summary>
    public interface IFridgeSensorService
    {
        /// <summary>
        /// Retrieves all info including static (non-configurable), configurable (fridge settings) and sensor (real-time values) for fridge <paramref name="fridgeName"/>
        /// </summary>
        FridgeInfo GetInfo(string fridgeName);

        /// <summary>
        /// Retrieves info from sensors (current temperature, open door, power consumption) for fridge <paramref name="fridgeName"/>
        /// </summary>
        FridgeSensorInfo GetSensorInfo(string fridgeName);

        List<Food> GetFood(string fridgeName);

        /// <summary>
        /// Computed from <see cref="GetFood(string)"/>
        /// </summary>
        /// <param name="fridgeName"></param>
        /// <returns></returns>
        double GetOccupiedVolumePercent(string fridgeName);

        /// <summary>
        /// Computed from <see cref="GetFood(string)"/>
        /// </summary>
        int GetExpiredFoodItemsCount(string fridgeName);

        /// <summary>
        /// Returns data for a graph which shows how many percent of food are healthy/neutral/unhealthy
        /// <br>Computed from <see cref="GetFood(string)"/></br>
        /// </summary>
        Dictionary<FoodHealthRating, double> GetFoodHealthStats(string fridgeName);

        List<Event> GetCurrentEvents(string fridgeName);
    }
}
