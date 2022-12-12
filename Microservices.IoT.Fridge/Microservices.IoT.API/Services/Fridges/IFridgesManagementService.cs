using System.Collections.Generic;
using Microservices.IoT.API.Models.Fridges;
using Microservices.IoT.API.Models.Fridges.In;
using Microservices.IoT.API.Models.Fridges.Info;

namespace Microservices.IoT.API.Services.Fridges
{
    /// <summary>
    /// Used to set up and configure fridges
    /// </summary>
    public interface IFridgesManagementService
    {
        /// <summary>
        /// Retrieves all info about a fridge
        /// </summary>
        Fridge Get(int ID);

        /// <summary>
        /// Retrieves infos about all fridges
        /// </summary>
        List<Fridge> GetAll();

        /// <summary>
        /// Used to fill a combobox when creating a new fridge in the management UI
        /// </summary>
        List<FridgeType> GetTypes();

        /// <summary>
        /// Creates a new fridge in the system
        /// </summary>
        /// <param name="item"></param>
        /// <returns>ID of the new object</returns>
        int Create(FridgeCreateDTO item);

        /// <summary>
        /// Deletes a fridge from the system
        /// </summary>
        /// <param name="ID"></param>
        void Delete(int ID);

        /// <summary>
        /// Used to configure an existing fridge via the management UI
        /// </summary>
        void SetDoorOpenWarningPeriodSeconds(int ID, int seconds);

        /// <summary>
        /// Used to configure an existing fridge via the management UI
        /// </summary>
        void SetOffsetDegreesForTemperatureWarning(int ID, double degrees);

        /// <summary>
        /// Used to configure an existing fridge via the management UI
        /// </summary>
        void SetOperatingTemperature(int ID, double degrees);

        /// <summary>
        /// Overwrites fridge's static info with new values
        /// </summary>
        void UpdateStaticInfo(int ID, FridgeStaticInfo data);
    }
}
