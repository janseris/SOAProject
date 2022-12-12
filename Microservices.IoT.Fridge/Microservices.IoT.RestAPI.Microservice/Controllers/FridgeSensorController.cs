using Microservices.IoT.API.Models.FoodItems;
using Microservices.IoT.API.Services.Fridges;
using Microservices.IoT.Sensor.RestAPI.Models;

using Microsoft.AspNetCore.Mvc;

namespace Microservices.IoT.Sensor.RestAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class FridgeSensorController : ControllerBase
    {
        private readonly IFridgeSensorService service;
        private readonly ApplicationConfig config;

        public FridgeSensorController(IFridgeSensorService service, ApplicationConfig config)
        {
            this.service = service;
            this.config = config;
        }

        /// <summary>
        /// Main function which should be periodically called
        /// </summary>
        /// <returns></returns>
        [HttpGet("sensorData", Name = "GetSensorData")]
        public FridgeSensorStats GetSensorData()
        {
            var info = service.GetInfo(config.Name);
            var warnings = service.GetCurrentEvents(config.Name);
            return new FridgeSensorStats
            {
                CurrentWarnings = warnings.Select(item => item.Type.Name).ToList(),
                IsDoorOpen = info.Sensors.IsOpen,
                PowerConsumption = info.Sensors.PowerConsumptionWatts,
                Temperature = info.Sensors.TemperatureDegrees
            };
        }

        /// <summary>
        /// Returns data for a graph which shows how many percent of food are healthy/neutral/unhealthy
        /// </summary>
        [HttpGet("foodHealthStats", Name ="GetFoodHealthStats")]
        public Dictionary<string, double> GetFoodHealthStats()
        {
            Dictionary<FoodHealthRating, double> data = service.GetFoodHealthStats(config.Name);
            List<KeyValuePair<string, double>> items = data.Select(kvp => new KeyValuePair<string, double>(kvp.Key.Name, kvp.Value)).ToList();
            return new Dictionary<string, double>(items);
        }

        [HttpGet("food", Name = "GetFood")]
        public List<FoodSensorModel> GetFood()
        {
            var data = service.GetFood(config.Name);
            var items = data.Select(item => new FoodSensorModel(item)).ToList();
            return items;
        }

        [HttpGet("occupiedVolumePercent", Name ="GetOccupiedVolumePercent")]
        public double GetOccupiedVolumePercent()
        {
            return service.GetOccupiedVolumePercent(config.Name);
        }

        [HttpGet("expiredFoodItems", Name ="GetExpiredFoodItemsCount")]
        public int GetExpiredFoodItemsCount()
        {
            return service.GetExpiredFoodItemsCount(config.Name);
        }


        [HttpGet("events", Name="GetCurrentEvents")]
        public List<EventSensorModel> GetCurrentEvents()
        {
            var data = service.GetCurrentEvents(config.Name);
            var items = data.Select(item => new EventSensorModel(item)).ToList();
            return items;
        }
    }
}
