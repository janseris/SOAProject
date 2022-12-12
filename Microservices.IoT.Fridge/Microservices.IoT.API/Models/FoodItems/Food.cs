using System;

namespace Microservices.IoT.API.Models.FoodItems
{
    /// <summary>
    /// A food item. Can exist in the food repository or can be placed in a fridge.
    /// </summary>
    public class Food
    {
        public int ID { get; set; }
        public string Name { get; set; }
        public string Producer { get; set; }
        public FoodType Type { get; set; }

        /// <summary>
        /// If the food item's packaging had been open
        /// </summary>
        public bool Open { get; set; }

        public int InitialWeightGrams { get; set; }

        /// <summary>
        /// Is reduced by consuming the food
        /// </summary>
        public int CurrentWeightGrams { get; set; }

        /// <summary>
        /// True if volume is not reduced when weight is reduced by consumption of a part of the food item
        /// </summary>
        public bool HasImmutableVolume { get; set; }
        public DateTime CreatedDate { get; set; }
        public DateTime ExpirationDate { get; set; }

        public bool Expired => DateTime.Now > ExpirationDate;

        public double CurrentVolumeLiters => GetCurrentVolumeLiters();

        private double GetCurrentVolumeLiters()
        {
            if (HasImmutableVolume)
            {
                return InitialWeightGrams / Type.Density / 1000d;
            }
            else
            {
                return CurrentWeightGrams / Type.Density / 1000d;
            }
        }
    }
}
