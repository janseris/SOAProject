using Microservices.IoT.API.Services.Events;
using Microservices.IoT.API.Services.FoodItems;
using Microservices.IoT.API.Services.Fridges;
using Microservices.IoT.Data.DAOs.Events;
using Microservices.IoT.Data.DAOs.FoodItems;
using Microservices.IoT.Data.DAOs.Fridges;
using Microservices.IoT.Data.Models;
using Microservices.IoT.ManagementConsole.RestAPI.Services;
using Microservices.IoT.Services.Events;
using Microservices.IoT.Services.FoodItems;
using Microservices.IoT.Services.Fridges;
using Microsoft.EntityFrameworkCore;

namespace Microservices.IoT.ManagementConsole.RestAPI
{
    public class DISetup
    {
        /// <summary>
        /// Adds services to DI container
        /// </summary>
        public static void ConfigureServices(IConfiguration appSettings, IServiceCollection services)
        {
            ConfigureDataAccessLayer(appSettings, services);
            ConfigureServiceLayer(appSettings, services);

            services.AddSingleton<SensorsManagementService>();
        }

        private static void ConfigureDataAccessLayer(IConfiguration appSettings, IServiceCollection services)
        {
            string connectionString = appSettings.GetConnectionString("DefaultConnectionString");
            services.AddDbContextFactory<MicroservicesContext>(options => options.UseSqlServer(connectionString));

            services.AddSingleton<EventManagementDAO>();
            services.AddSingleton<EventSimulationDAO>();

            services.AddSingleton<FoodItemsManagementDAO>();
            services.AddSingleton<FoodItemsSimulationDAO>();
            services.AddSingleton<FoodTypeDAO>();

            services.AddSingleton<FridgeSensorDAO>();
            services.AddSingleton<FridgeSimulationDAO>();
            services.AddSingleton<FridgesManagementDAO>();
        }

        private static void ConfigureServiceLayer(IConfiguration appSettings, IServiceCollection services)
        {
            services.AddSingleton<IEventManagementService, EventManagementService>();
            services.AddSingleton<IEventSimulationService, EventSimulationService>();

            services.AddSingleton<IFoodItemsManagementService, FoodItemsManagementService>();
            services.AddSingleton<IFoodItemsSimulationService, FoodItemsSimulationService>();
            services.AddSingleton<IFoodTypeService, FoodTypeService>();

            services.AddSingleton<IFridgeSensorService, FridgeSensorService>();
            services.AddSingleton<IFridgeSimulationService, FridgeSimulationService>();
            services.AddSingleton<IFridgesManagementService, FridgesManagementService>();
        }
    }
}
