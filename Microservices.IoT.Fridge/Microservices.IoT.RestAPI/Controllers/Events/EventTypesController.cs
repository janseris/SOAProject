using Microservices.IoT.API.Models.Events;
using Microservices.IoT.API.Services.Events;

using Microsoft.AspNetCore.Mvc;

namespace Microservices.IoT.ManagementConsole.RestAPI.Controllers.Events
{
    [Route("api/[controller]")]
    [ApiController]
    public class EventTypesController : ControllerBase
    {
        private readonly IEventManagementService service;

        public EventTypesController(IEventManagementService service)
        {
            this.service = service;
        }

        /// <summary>
        /// Gets a list of event types which can be used to create a new event.
        /// </summary>
        [HttpGet("", Name ="GetEventTypes")]
        public List<EventType> GetEventTypes()
        {
            return service.GetEventTypes();
        }
    }
}
