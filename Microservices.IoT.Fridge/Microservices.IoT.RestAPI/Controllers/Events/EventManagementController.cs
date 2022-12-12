using Microservices.IoT.API.Models.Events;
using Microservices.IoT.API.Services.Events;

using Microsoft.AspNetCore.Mvc;

namespace Microservices.IoT.ManagementConsole.RestAPI.Controllers.Events
{
    [Route("api/[controller]")]
    [ApiController]
    public class EventManagementController : ControllerBase
    {
        private readonly IEventManagementService service;

        public EventManagementController(IEventManagementService service)
        {
            this.service = service;
        }

        /// <summary>
        /// Returns all events for a fridge <paramref name="fridgeID"/> (past and present)
        /// </summary>
        /// <param name="fridgeID" example="1">Documentation for <paramref name="fridgeID"/> parameter</param>
        /// <returns></returns>
        [HttpGet("", Name ="GetAllEvents")] //could be GET fridges/{id}/events in fridges controller as well
        public List<Event> GetAll([FromQuery]int fridgeID /* filter */)
        {
            return service.GetAll(fridgeID);
        }

        /// <summary>
        /// Deletes event <paramref name="ID"/>
        /// </summary>
        /// <param name="ID"></param>
        [HttpDelete("{id}", Name ="DeleteEventByID")]
        public void Delete(int ID)
        {
            service.Delete(ID);
        }

        /// <summary>
        /// Deletes all events for fridge <paramref name="fridgeID"/>
        /// </summary>
        /// <param name="fridgeID"></param>
        [HttpDelete("", Name ="DeleteEvents")] //could be DELETE fridges/{id}/events in fridges controller as well
        public void DeleteAll([FromQuery]int fridgeID /* filter */)
        {
            service.DeleteAll(fridgeID); //automatically returns OK
        }
    }
}
