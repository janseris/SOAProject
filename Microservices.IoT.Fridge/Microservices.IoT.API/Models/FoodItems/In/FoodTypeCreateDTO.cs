namespace Microservices.IoT.API.Models.FoodItems.In
{
    public class FoodTypeCreateDTO
    {
        public string Name { get; set; }
        public double Density { get; set; }
        public bool ImmutableVolumeHint { get; set; }
        public int HealthRatingID { get; set; }
    }
}
