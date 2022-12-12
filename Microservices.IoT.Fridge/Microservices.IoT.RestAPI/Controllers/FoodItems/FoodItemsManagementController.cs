using Microservices.IoT.API.Exceptions;
using Microservices.IoT.API.Models.FoodItems;
using Microservices.IoT.API.Models.FoodItems.In;
using Microservices.IoT.API.Services.FoodItems;

using Microsoft.AspNetCore.Mvc;

namespace Microservices.IoT.ManagementConsole.RestAPI.Controllers.FoodItems
{
    [Route("api/[controller]")]
    [ApiController]
    public class FoodItemsManagementController : ControllerBase
    {
        private readonly IFoodItemsManagementService service;

        public FoodItemsManagementController(IFoodItemsManagementService service)
        {
            this.service = service;
        }

        //<inheritdoc cref="serviceClass"> could be used for all <summary> but Swagger UI does not display it correctly

        /// <summary>
        /// Gets a particular food item info by <paramref name="ID"/>.
        /// </summary>
        /// <exception cref="NotFoundException"></exception>
        [HttpGet("{ID}", Name = "GetFoodItem")]
        public Food Get(int ID)
        {
            return service.Get(ID);
        }

        /// <summary>
        /// Retrieves all food items available in the food repository.
        /// <br>Food items placed in fridges are not returned.</br>
        /// </summary>
        [HttpGet("", Name = "GetAllFoodItems")]
        public List<Food> GetAll()
        {
            return service.GetAll();
        }

        /// <summary>
        /// Creates a new food item and places it in the food repository.
        /// <br>This food item can then be placed into a fridge.</br>
        /// </summary>
        [HttpPost("", Name = "InsertFoodItem")]
        public int Create(FoodCreateDTO item)
        {
            return service.Create(item);
        }

        /// <summary>
        /// Deletes a food item from the system.
        /// <br>The food item could have been placed in a fridge but could be in the repository.</br>
        /// </summary>
        [HttpDelete("{ID}", Name = "DeleteFoodItem")]
        public void Remove(int ID)
        {
            service.Remove(ID);
        }

        /// <summary>
        /// Creates a copy of a food item with full stats and places it in the food repository.)"/>
        /// </summary>
        [HttpPost("{ID}/duplicate", Name ="DuplicateFoodItem")]
        public int Duplicate(int ID)
        {
            return service.Duplicate(ID);
        }

        /// <summary>
        /// Updates a food item's <see cref="Food.ExpirationDate"/> to help simulate expired items in the system
        /// </summary>
        [HttpPatch("{ID}", Name = "ChangeExpirationDate")]
        public void ChangeExpirationDate(int ID, [FromQuery] DateTime newDate)
        {
            service.ChangeExpirationDate(ID, newDate);
        }
    }
}
