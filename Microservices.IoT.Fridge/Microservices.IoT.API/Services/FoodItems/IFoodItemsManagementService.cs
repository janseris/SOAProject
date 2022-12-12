using System;
using System.Collections.Generic;

using Microservices.IoT.API.Exceptions;
using Microservices.IoT.API.Models.FoodItems;
using Microservices.IoT.API.Models.FoodItems.In;

namespace Microservices.IoT.API.Services.FoodItems
{
    /// <summary>
    /// Food items live in a repository (e.g. a heap of food on the ground or in a warehouse)
    /// <br>These food items can be placed in fridges (one food item can belong to at most one fridge).</br>
    /// </summary>
    public interface IFoodItemsManagementService
    {
        /// <summary>
        /// Gets a particular food item info by <paramref name="ID"/>.
        /// </summary>
        Food Get(int ID);

        /// <summary>
        /// Retrieves all food items available in the food repository.
        /// <br>Food items placed in fridges are not returned.</br>
        /// </summary>
        List<Food> GetAll();

        /// <summary>
        /// Creates a new food item and places it in the food repository.
        /// <br>This food item can then be placed into a fridge.</br>
        /// </summary>
        int Create(FoodCreateDTO item);

        /// <summary>
        /// Deletes a food item from the system.
        /// <br>The food item could have been placed in a fridge but could be in the repository.</br>
        /// </summary>
        void Remove(int ID);

        /// <summary>
        /// Creates a copy of a food item with full stats and places it in the food repository.
        /// </summary>
        int Duplicate(int ID);

        /// <summary>
        /// Updates a food item's <see cref="Food.ExpirationDate"/> to help simulate expired items in the system
        /// </summary>
        void ChangeExpirationDate(int ID, DateTime newDate);
    }
}
