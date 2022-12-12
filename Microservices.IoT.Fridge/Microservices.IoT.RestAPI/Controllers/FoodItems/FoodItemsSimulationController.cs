using Microservices.IoT.API.Services.FoodItems;

using Microsoft.AspNetCore.Mvc;

namespace Microservices.IoT.ManagementConsole.RestAPI.Controllers.FoodItems
{
    /// <summary>
    /// <inheritdoc cref="IFoodItemsSimulationService"/>
    /// </summary>
    [Route("api/[controller]")]
    [ApiController]
    public class FoodItemsSimulationController : ControllerBase
    {
        private readonly IFoodItemsSimulationService service;

        public FoodItemsSimulationController(IFoodItemsSimulationService service)
        {
            this.service = service;
        }

        //<inheritdoc cref="serviceClass"> could be used for all <summary> but Swagger UI does not display it correctly

        /// <summary>
        /// Opens the food item's packaging.
        /// <br>Does nothing if the food packaging is already open.</br>
        /// </summary>
        /// <param name="ID"></param>
        [HttpPatch("{id}/open", Name = "OpenFoodItemPackage")]
        public void OpenFoodPackage(int ID)
        {
            service.OpenFoodPackage(ID);
        }
        /// <summary>
        /// Simulates consuming of a part of a food item.
        /// <br>Decreases the weight of a particular food item by percentage <paramref name="amountPercent"/>.</br>
        /// <br>A food item has initial weight and current weight in grams.</br>
        /// <br>If the weight reaches 0, the food item disappears (is deleted).</br>
        /// <br>Opens the food item's package if this is first consumption</br>
        /// </summary>
        /// <param name="ID">food ID</param>
        /// <param name="amountPercent">must be between 1 and 100</param>
        /// <response code="200"></response>
        /// <response code="400">If <paramref name="amountPercent"/> is not a number between 1 and 100</response>
        [HttpPost("{id}", Name ="ConsumeFoodItem")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status409Conflict)]
        public IActionResult Consume(int ID, [FromQuery] int amountPercent)
        {
            try
            {
                service.Consume(ID, amountPercent);
            } catch (ArgumentException)
            {
                return Problem(detail: "Consumed amount percentage must be a positive number between 1 and 100.", statusCode: StatusCodes.Status400BadRequest);
            }
            return Ok();
        }
    }
}
