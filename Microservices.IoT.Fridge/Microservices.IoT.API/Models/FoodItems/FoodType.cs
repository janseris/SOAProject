namespace Microservices.IoT.API.Models.FoodItems
{
    public class FoodType
    {
        public int ID { get; set; }
        public string Name { get; set; }

        /// <summary>
        /// Weight in grams ratio to mililiters of volume
        /// <br>E.g. 2.0 = 2000 grams in 1000 mililiters</br>
        /// </summary>
        public double Density { get; set; }

        /// <summary>
        /// Suggested default value for <see cref="Food.HasImmutableVolume"/> when creating a food item of this type
        /// </summary>
        public bool ImmutableVolumeHint { get; set; }

        public FoodHealthRating HealthRating { get; set; }
    }
}
