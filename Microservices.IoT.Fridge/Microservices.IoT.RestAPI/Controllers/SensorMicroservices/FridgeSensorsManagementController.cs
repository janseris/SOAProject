using System.ComponentModel;

using App.Metrics;

using Microservices.IoT.ManagementConsole.RestAPI.Metrics;
using Microservices.IoT.ManagementConsole.RestAPI.Services;

using Microsoft.AspNetCore.Mvc;

/*
 * Using REST API controllers: https://learn.microsoft.com/en-us/aspnet/core/web-api/action-return-types?view=aspnetcore-6.0
 * IActionResult = for multiple return types
 * Task<IActionResult> for async IActionResult
 */

namespace Microservices.IoT.ManagementConsole.RestAPI.Controllers.SensorMicroservices
{
    /// <summary>
    /// Used to control microservices which simulate sensors on running fridges
    /// </summary>
    [Route("api/[controller]")] //generates api/FridgeSensors base URL because the controller class name is FridgeSensorsController
    [ApiController]
    public class FridgeSensorsManagementController : ControllerBase
    {
        private readonly SensorsManagementService service;
        private readonly IMetrics metrics;
        public FridgeSensorsManagementController(SensorsManagementService service, IMetrics metrics)
        {
            this.service = service;
            this.metrics = metrics;
        }

        /// <summary>
        /// = microservice process for this fridge is running
        /// </summary>
        /// <param name="name"></param>
        /// <returns></returns>
        [HttpGet("{name}", Name = "IsSensorMicroserviceRunning")]
        public bool IsSensorRunning(string name)
        {
            return service.IsRunning(name);
        }

        /// <summary>
        /// Returns the port on which a sensor microservice process is running if it is tracked
        /// <br><c>null</c> if the service is not running (or is no tracked)</br>
        /// </summary>
        /// <param name="name"></param>
        [HttpGet(template: "{name}/port", Name = "GetRunningSensorMicroservicePort")]
        public int? GetPort(string name)
        {
            return service.GetPort(name);
        }

        /// <summary>
        /// Starts a new instance of a microservice with a given <paramref name="name"/> to listen on port <paramref name="port"/> and adds it to name service-
        /// </summary>
        /// <response code="200">If a service for <paramref name="name"/> was sucessfully started</response>
        /// <response code="409">If a service for <paramref name="name"/> is already running or if <paramref name="port"/> is already occupied</response>
        /// <response code="422">If a service for <paramref name="name"/> fails to start</response>
        [HttpPost(template: "{name}/start", Name = "StartSensorMicroservice")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status409Conflict)]
        [ProducesResponseType(StatusCodes.Status422UnprocessableEntity)]
        public IActionResult Start(string name, int port, bool showWindow)
        {
            if(IsSensorRunning(name))
            {
                int? currentPort = service.GetPort(name);
                return Problem(statusCode: StatusCodes.Status409Conflict, detail: $"A service for fridge {name} is already running on port {currentPort}");
            }
            if(service.IsPortFree(port) == false)
            {
                return Problem(statusCode: StatusCodes.Status409Conflict, detail: $"The port {port} is already occupied by a process.");
            }
            try
            {
                service.Start(name, port, showWindow);
            } catch (Exception ex)
            {
                return Problem(
                    statusCode: StatusCodes.Status422UnprocessableEntity,
                    title: $"The service for fridge {name} could not be started",
                    detail: $"{ex.GetType()} {ex.Message}");
            }
            metrics.Measure.Counter.Increment(MetricsCollection.StartedMicroservicesCounter);
            metrics.Measure.Counter.Increment(MetricsCollection.RunningMicroservicesCounter);
            return Ok();
        }

        /// <summary>
        /// Stops a sensor microservice running for defined <paramref name="name"/>
        /// </summary>
        /// <param name="name"></param>
        /// <returns><c>true</c> if the sensor microservice process was running and was stopped (killed) and removed from tracking</returns>
        [HttpPost(template: "{name}/stop", Name = "StopSensorMicroservice")]
        public bool Stop(string name)
        {
            metrics.Measure.Counter.Decrement(MetricsCollection.RunningMicroservicesCounter);
            return service.Stop(name);
        }

        /// <summary>
        /// Sets path to a file which will be executed as a fridge sensor microservice.
        /// </summary>
        [HttpPut("sensorProcessExecutablePath", Name ="SetSensorProcessExecutablePath")]
        public void SetSensorProcessExecutablePath(string path)
        {
            service.ProcessExecutablePath = path;
        }

        /// <summary>
        /// Gets the path to the executable which is used as a fridge sensor microservice process.
        /// </summary>
        [HttpGet("sensorProcessExecutablePath", Name ="GetSensorProcessExecutablePath")]
        public string GetSensorProcessExecutablePath()
        {
            return service.ProcessExecutablePath;
        }
    }
}
