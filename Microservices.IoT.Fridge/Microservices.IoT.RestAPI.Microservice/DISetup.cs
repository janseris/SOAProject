using Microservices.IoT.API.Services.Fridges;
using Microservices.IoT.Data.DAOs.Fridges;
using Microservices.IoT.Data.DAOs;
using Microservices.IoT.Data.Models;
using Microservices.IoT.Services.Fridges;
using Microsoft.EntityFrameworkCore;

namespace Microservices.IoT.Sensor.RestAPI
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
        }

        private static void ConfigureDataAccessLayer(IConfiguration appSettings, IServiceCollection services)
        {
            string connectionString = appSettings.GetConnectionString("DefaultConnectionString");
            services.AddDbContextFactory<MicroservicesContext>(options => options.UseSqlServer(connectionString));

            services.AddSingleton<WarmUpDAO>();

            services.AddSingleton<FridgeSensorDAO>();
        }

        private static void ConfigureServiceLayer(IConfiguration appSettings, IServiceCollection services)
        {
            services.AddSingleton<IFridgeSensorService, FridgeSensorService>();
        }
    }
}
