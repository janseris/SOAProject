namespace Microservices.IoT.API.Models.FoodItems.In
{
    /// <summary>
    /// Used to replace info in a food type definition.
    /// </summary>
    public class FoodTypeUpdateDTO
    {
        /// <summary>
        /// Density defines the volume of the food item calculated from its weight
        /// <br><c>kg</c>/<c>liter</c> or <c>grams</c>/<c>mililiter</c></br>
        /// </summary>
        public double Density { get; set; }
        public int HealthRatingID { get; set; }

        /// <summary>
        /// Immutable volume signifies if the volume of the food item changes if the weight is reduced when a part of the item is consumed
        /// </summary>
        public bool ImmutableVolumeHint { get; set; }
    }
}
