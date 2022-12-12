using Microservices.IoT.API.Models.FoodItems;

using Microsoft.AspNetCore.Mvc;

/*
 * Using REST API controllers: https://learn.microsoft.com/en-us/aspnet/core/web-api/action-return-types?view=aspnetcore-6.0
 * IActionResult = for multiple return types
 * Task<IActionResult> for async IActionResult
 */

namespace Microservices.IoT.ManagementConsole.RestAPI.Controllers
{
    [Route("api/[controller]")] //generates api/Food path because controller class name is FoodController
    [ApiController]
    public class SampleController : ControllerBase
    {
        private static readonly List<Food> Items = new List<Food>();

        // GET: api/[controller]
        [HttpGet] //same as [HttpGet("")] (empty relative route appended to the controller's base path)
        [ProducesResponseType(StatusCodes.Status200OK)]
        public Task<IEnumerable<Food>> GetAll()
        {
            IEnumerable<Food> items = Items;
            return Task.FromResult(items);
        }

        // GET api/food/5
        /// <summary>
        /// Popis metody
        /// </summary>
        /// <param name="ID"></param>
        /// <response code="404">Documentation for status code displayed in Swagger UI: When the given item by <paramref name="ID"/> does not exist</response>
        [HttpGet("{ID}"/* relative URL for this function with ID argument being a part of the path/URL */,
            Name = "GetItemByID" /* friendly function name displayed in Swagger UI and used by the Visual Studio C# REST API client generator */)]
        [ProducesResponseType(typeof(Food), StatusCodes.Status200OK)] //displays data type for this status code in Swagger UI
        [ProducesResponseType(StatusCodes.Status404NotFound)] //automatically returns a ProblemDetails object (RFC 7807)
        public async Task<IActionResult> Get(int ID)
        {
            var item = Items.Where(item => item.ID == ID).SingleOrDefault();
            if (item is null)
            {
                //returning a non-2xx status code automatically throws an exception on the client
                //return NotFound(); //returns ProblemDetails with default values (404, "Not Found") (and no "details" property)
                //when we add our custom values, the other properties are still filled
                return Problem(
                    statusCode: StatusCodes.Status404NotFound, /* if not provided, 500 is used automatically */
                    title: $"Requested food item {ID} was not found", /* if not provided, a value is automatically provided according to status code */
                    detail: "Detailed explanation of the problem" /* not filled automatically */
                    );
            }
            return Ok(item);
        }

        // POST api/<FoodController>
        //https://learn.microsoft.com/en-us/aspnet/core/web-api/action-return-types?view=aspnetcore-6.0
        [HttpPost]
        [ProducesResponseType(StatusCodes.Status201Created)]
        public async Task<IActionResult> Create([FromBody] string value)
        {
            Food item = null; //TODO
            //CreatedAtAction is in compliance with the standard: https://www.rfc-editor.org/rfc/rfc7231#section-6.3.2
            //Returns a response with a Location header set to GET link for this newly created item.
            //The newly created object can be returned in the response body as well using an overload of CreatedAtAction
            //That is however not useful for microservices and not useful for large content (files, images).
            return CreatedAtAction(nameof(Get), new { ID = item.ID });
        }

        //put is replacing the entire object
        // PUT api/<FoodController>/5
        [HttpPut("{ID}")]
        public void Put(int ID, [FromBody] string value)
        {
        }

        // DELETE api/<FoodController>/5
        [HttpDelete("{ID}")]
        public void Delete(int ID)
        {
        }
    }
}
