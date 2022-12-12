using System;

namespace Microservices.IoT.API.Services.FoodItems
{
    public interface IFoodItemsSimulationService
    {
        /// <summary>
        /// Opens the food item's packaging.
        /// <br>Does nothing if the food packaging is already open.</br>
        /// </summary>
        void OpenFoodPackage(int ID);

        /// <summary>
        /// Simulates consuming of a part of a food item.
        /// <br>Decreases the weight of a particular food item by percentage <paramref name="amountPercent"/>.</br>
        /// <br>A food item has initial weight and current weight in grams.</br>
        /// <br>If the weight reaches 0, the food item disappears (is deleted).</br>
        /// <br>Opens the food item's package if this is first consumption</br>
        /// </summary>
        /// <param name="amountPercent">must be between 1 and 100</param>
        /// <exception cref="ArgumentException">If <paramref name="amountPercent"/> value is out of range</exception>
        void Consume(int ID, int amountPercent);
    }
}
