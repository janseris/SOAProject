using Microservices.IoT.API.Models.Events;
using Microservices.IoT.API.Models.FoodItems;
using Microservices.IoT.API.Models.Fridges;
using Microservices.IoT.API.Models.Fridges.Info;
using Microservices.IoT.API.Services.Fridges;
using Microservices.IoT.Data.DAOs.Fridges;

namespace Microservices.IoT.Services.Fridges
{
    public class FridgeSensorService : IFridgeSensorService
    {
        private readonly FridgeSensorDAO dao;

        public FridgeSensorService(FridgeSensorDAO dao)
        {
            this.dao = dao;
        }

        public FridgeInfo GetInfo(string fridgeName)
        {
            return dao.GetInfo(fridgeName);
        }

        public FridgeSensorInfo GetSensorInfo(string fridgeName)
        {
            return dao.GetSensorInfo(fridgeName);
        }

        public List<Event> GetCurrentEvents(string fridgeName)
        {
            return dao.GetCurrentEvents(fridgeName);
        }

        public int GetExpiredFoodItemsCount(string fridgeName)
        {
            return dao.GetExpiredFoodItemsCount(fridgeName);
        }

        public List<Food> GetFood(string fridgeName)
        {
            return dao.GetFood(fridgeName);
        }

        public Dictionary<FoodHealthRating, double> GetFoodHealthStats(string fridgeName)
        {
            var foodRatings = dao.GetFoodHealthStats(fridgeName);
            int totalCount = foodRatings.Count;
            var distinctTypes = foodRatings.DistinctBy(fr => fr.ID).ToList();
            Dictionary<FoodHealthRating, double> stats = new Dictionary<FoodHealthRating, double>();
            foreach(var foodHealthRating in distinctTypes)
            {
                var count = foodRatings.Where(fr => fr.ID == foodHealthRating.ID).ToList().Count;
                var percent = (count / (double)totalCount) * 100d;
                stats.Add(foodHealthRating, percent);
            }
            return stats;
        }

        public double GetOccupiedVolumePercent(string fridgeName)
        {
            var food = GetFood(fridgeName);
            var info = GetInfo(fridgeName);
            var occupiedVolumeLiters = food.Sum(f => f.CurrentVolumeLiters);
            var volumeCapacityLiters = info.Static.CapacityLiters;
            return (occupiedVolumeLiters / volumeCapacityLiters) * 100d;
        }

    }
}
