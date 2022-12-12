using System;

namespace Microservices.IoT.API.Models.FoodItems.In
{
    public class FoodCreateDTO
    {
        public string Name { get; set; }
        public string Producer { get; set; }
        public int TypeID { get; set; }
        public bool Open { get; set; }
        public int WeightGrams { get; set; }
        public bool ImmutableVolume { get; set; }
        public DateTime ExpirationDate { get; set; }
    }
}
