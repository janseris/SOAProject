using Microservices.IoT.API.Models.FoodItems;

namespace Microservices.IoT.Sensor.RestAPI.Models
{
    public class FoodSensorModel
    {
        public string Name { get; set; }
        public string Type { get; set; }

        /// <summary>
        /// If the food item's packaging had been open
        /// </summary>
        public bool Open { get; set; }

        public int InitialWeightGrams { get; set; }

        /// <summary>
        /// Is reduced by consuming the food
        /// </summary>
        public int CurrentWeightGrams { get; set; }
        public DateTime ExpirationDate { get; set; }

        public FoodSensorModel(Food data) 
        {
            this.Name = data.Name;
            this.Type = data.Type.Name;
            this.Open = data.Open;
            this.InitialWeightGrams = data.InitialWeightGrams;
            this.CurrentWeightGrams = data.CurrentWeightGrams;
            this.ExpirationDate = data.ExpirationDate;
        }

    }
}
