using System.Collections.Generic;
using System.Linq;
using Microservices.IoT.API.Models.Events;
using Microservices.IoT.API.Models.FoodItems;

namespace Microservices.IoT.API.Models.Fridges
{
    /// <summary>
    /// Full info about a fridge including its food items and events (current and past)
    /// </summary>
    public class Fridge
    {
        public int ID { get; set; }
        public string Name { get; set; }
        public string Brand { get; set; }
        public FridgeType Type { get; set; }
        public List<Food> Food { get; set; }
        public List<Event> Events { get; set; }
        public FridgeInfo Info { get; set; }
        public double OccupiedVolumeLiters => Food.Sum(item => item.CurrentVolumeLiters);
        public double OccupiedVolumePercent => OccupiedVolumeLiters / Info.Static.CapacityLiters * 100d;
        public double FreeVolumePercent => 100d - OccupiedVolumeLiters;
        public double FreeVolumeLiters => Info.Static.CapacityLiters - OccupiedVolumeLiters;
    }
}
