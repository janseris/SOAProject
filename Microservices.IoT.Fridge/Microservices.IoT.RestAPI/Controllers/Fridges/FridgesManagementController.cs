using Microservices.IoT.API.Models.Fridges.In;
using Microservices.IoT.API.Models.Fridges.Info;
using Microservices.IoT.API.Models.Fridges;
using Microservices.IoT.API.Services.Fridges;
using Microsoft.AspNetCore.Mvc;

namespace Microservices.IoT.ManagementConsole.RestAPI.Controllers.Fridges
{
    /// <summary>
    /// Used to set up and configure fridges
    /// </summary>
    [Route("api/[controller]")]
    [ApiController]
    public class FridgesManagementController : ControllerBase
    {
        private readonly IFridgesManagementService service;

        public FridgesManagementController(IFridgesManagementService service)
        {
            this.service = service;
        }

        /// <summary>
        /// Retrieves all info about a fridge
        /// </summary>
        [HttpGet("{ID}", Name ="GetFridge")]
        public Fridge Get(int ID)
        {
            return service.Get(ID);
        }

        /// <summary>
        /// Retrieves infos about all fridges
        /// </summary>
        [HttpGet("", Name ="GetAllFridges")]
        public List<Fridge> GetAll()
        {
            return service.GetAll();
        }

        /// <summary>
        /// Used to fill a combobox when creating a new fridge in the management UI
        /// </summary>
        [HttpGet("fridgeTypes", Name = "GetFridgeTypes")]
        public List<FridgeType> GetTypes()
        {
            return service.GetTypes();
        }

        /// <summary>
        /// Creates a new fridge in the system
        /// </summary>
        /// <param name="item"></param>
        /// <returns>ID of the new object</returns>
        [HttpPost("", Name ="InsertFridge")]
        public int Create(FridgeCreateDTO item)
        {
            return service.Create(item);
        }

        /// <summary>
        /// Deletes a fridge from the system
        /// </summary>
        /// <param name="ID"></param>
        [HttpDelete("{ID}", Name="DeleteFridge")]
        public void Delete(int ID)
        {
            service.Delete(ID);
        }

        /// <summary>
        /// Used to configure an existing fridge via the management UI
        /// </summary>
        [HttpPut("{ID}/doorOpenWarningPeriodSeconds", Name= "SetFridgeDoorOpenWarningPeriodSeconds")]
        public void SetDoorOpenWarningPeriodSeconds(int ID, int seconds)
        {
            service.SetDoorOpenWarningPeriodSeconds(ID, seconds);
        }

        /// <summary>
        /// Used to configure an existing fridge via the management UI
        /// </summary>
        [HttpPut("{ID}/offsetDegreesForTemperatureWarning", Name= "SetFridgeOffsetDegreesForTemperatureWarning")]
        public void SetOffsetDegreesForTemperatureWarning(int ID, double degrees)
        {
            service.SetOffsetDegreesForTemperatureWarning(ID, degrees);
        }

        /// <summary>
        /// Used to configure an existing fridge via the management UI
        /// </summary>
        [HttpPut("{ID}/operatingTemperature", Name= "SetFridgeOperatingTemperature")]
        public void SetOperatingTemperature(int ID, double degrees)
        {
            service.SetOperatingTemperature(ID, degrees);
        }

        /// <summary>
        /// Overwrites fridge's static info with new values
        /// </summary>
        [HttpPut("{ID}/staticInfo", Name= "SetFridgeStaticInfo")]
        public void UpdateStaticInfo(int ID, FridgeStaticInfo data)
        {
            service.UpdateStaticInfo(ID, data);
        }

    }
}
