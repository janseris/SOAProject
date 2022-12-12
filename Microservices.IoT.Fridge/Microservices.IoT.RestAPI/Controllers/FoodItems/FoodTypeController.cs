using Microservices.IoT.API.Models.FoodItems.In;
using Microservices.IoT.API.Models.FoodItems;
using Microservices.IoT.API.Services.FoodItems;
using Microsoft.AspNetCore.Mvc;

namespace Microservices.IoT.ManagementConsole.RestAPI.Controllers.FoodItems
{
    [Route("api/[controller]")]
    [ApiController]
    public class FoodTypeController : ControllerBase
    {
        private readonly IFoodTypeService service;

        public FoodTypeController(IFoodTypeService service)
        {
            this.service = service;
        }

        //<inheritdoc cref="serviceClass"> could be used for all <summary> but Swagger UI does not display it correctly

        /// <summary>
        /// Gets all food item types available in the system.
        /// <br>Used to select a type when creating a food item in the UI.</br>
        /// </summary>
        [HttpGet("", Name = "GetAllFoodTypes")]
        public List<FoodType> GetAll()
        {
            return service.GetAll();
        }

        /// <summary>
        /// Adds a new food item type to be available in the system.
        /// </summary>
        [HttpPost("", Name = "InsertFoodType")]
        public int AddNew(FoodTypeCreateDTO item) //automatically [FromBody]
        {
            return service.AddNew(item);
        }

        /// <summary>
        /// Health rating is metadata for each food type which can be used for calculating statistics about food stored in a fridge.
        /// </summary>
        [HttpGet("healthRatings", Name = "GetAllFoodHealthRatingTypes")]
        public List<FoodHealthRating> GetAllHealthRatingTypes()
        {
            return service.GetAllHealthRatingTypes();
        }

        /// <summary>
        /// Updates/replaces food type <paramref name="ID"/> definition with new definition.
        /// </summary>
        [HttpPut("{ID}", Name ="ReplaceFoodType")]
        public void Replace(int ID, FoodTypeUpdateDTO newData)
        {
            service.Replace(ID, newData);
        }
    }
}
