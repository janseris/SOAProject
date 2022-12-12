using System.Collections.Generic;
using Microservices.IoT.API.Models.FoodItems;
using Microservices.IoT.API.Models.FoodItems.In;

namespace Microservices.IoT.API.Services.FoodItems
{
    /// <summary>
    /// List of food types helps sorting and adding metadata to food items
    /// <br>This list can be extended by the user and is used in a selection list in the UI when creating a new food item.</br>
    /// </summary>
    public interface IFoodTypeService
    {
        /// <summary>
        /// Gets all food item types available in the system.
        /// <br>Used to select a type when creating a food item in the UI.</br>
        /// </summary>
        List<FoodType> GetAll();

        /// <summary>
        /// Adds a new food item type to be available in the system.
        /// </summary>
        int AddNew(FoodTypeCreateDTO item);

        /// <summary>
        /// Health rating is metadata for each food type which can be used for calculating statistics about food stored in a fridge.
        /// </summary>
        List<FoodHealthRating> GetAllHealthRatingTypes();

        /// <summary>
        /// Updates/replaces food type <paramref name="ID"/> definition with new definition.
        /// </summary>
        void Replace(int ID, FoodTypeUpdateDTO newData);
    }
}
