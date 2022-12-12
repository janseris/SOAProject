using System.Diagnostics;
using System.Net.NetworkInformation;
using System.Net;

using Microservices.IoT.ManagementConsole.RestAPI.Models;

namespace Microservices.IoT.ManagementConsole.RestAPI.Services
{
    /// <summary>
    /// Manages sensor microservice processes
    /// </summary>
    public class SensorsManagementService
    {
        private string _processExecutablePath;
        /// <summary>
        /// Path on which the executable for the sensor process is searched
        /// </summary>
        public string ProcessExecutablePath
        {
            get => _processExecutablePath;
            set { 
                _processExecutablePath = value;
                Console.WriteLine($"Sensor microservices executable path: {value}");
            }
        } 

        public const string DefaultProcessPath = @"C:\Users\janse\Desktop\skola\podzim 2022\PV217 Service Oriented Architecture\Microservices.IoT.Fridge\Microservices.IoT.RestAPI.Microservice\bin\Debug\net6.0\Microservices.IoT.Sensor.RestAPI.exe";

        public SensorsManagementService()
        {
            ProcessExecutablePath = DefaultProcessPath;
        }

        /// <summary>
        /// A primitive service discovery service using name
        /// </summary>
        private readonly Dictionary<string, SensorMicroserviceProcess> RunningMicroservicesByName = new Dictionary<string, SensorMicroserviceProcess>();

        /// <summary>
        /// Returns true if a microservice for given name is running and is managed.
        /// </summary>
        /// <param name="name"></param>
        public bool IsRunning(string name)
        {
            return RunningMicroservicesByName.ContainsKey(name);
        }

        /// <summary>
        /// Returns the port on which a sensor microservice process is running if it is tracked
        /// <br><c>null</c> if the service is not running (or is no tracked)</br>
        /// </summary>
        /// <param name="name"></param>
        public int? GetPort(string name)
        {
            if(RunningMicroservicesByName.ContainsKey(name) == false)
            {
                return null;
            }
            return RunningMicroservicesByName[name].Port;
        }

        /// <summary>
        /// Starts a new instance of a microservice with a given <paramref name="name"/> to listen on port <paramref name="port"/>
        /// </summary>
        /// <returns><c>true</c> if the microservice process was successfully started and added to tracking.</returns>
        public void Start(string name, int port, bool showWindow)
        {
            ProcessStartInfo info = new ProcessStartInfo();
            info.FileName = ProcessExecutablePath;
            info.Arguments = $"\"{name}\" {port}";
            info.UseShellExecute = showWindow;
            var process = Process.Start(info);
            var microserviceInfo = new SensorMicroserviceProcess {
                Port = port,
                PID = process.Id,
                Started = DateTime.Now
            };
            RunningMicroservicesByName.Add(name, microserviceInfo);
        }

        /// <summary>
        /// Stops a sensor microservice running for defined <paramref name="name"/>
        /// </summary>
        /// <param name="name"></param>
        /// <returns><c>true</c> if the sensor microservice process was running and was stopped (killed) and removed from tracking</returns>
        public bool Stop(string name) 
        {
            if(IsRunning(name) == false)
            {
                return true;
            }
            var PID = RunningMicroservicesByName[name].PID;
            Process.GetProcessById(PID).Kill();
            Console.WriteLine($"Microservice {name} stopped");
            RunningMicroservicesByName.Remove(name);
            return true;
        }

        public bool IsPortFree(int port)
        {
            bool isAvailable = true;

            // Evaluate current system tcp connections. This is the same information provided
            // by the netstat command line application, just in .Net strongly-typed object
            // form.  We will look through the list, and if our port we would like to use
            // in our TcpClient is occupied, we will set isAvailable to false.
            IPGlobalProperties ipGlobalProperties = IPGlobalProperties.GetIPGlobalProperties();
            IPEndPoint[] tcpConnInfoArray = ipGlobalProperties.GetActiveTcpListeners();

            foreach (IPEndPoint endpoint in tcpConnInfoArray)
            {
                if (endpoint.Port == port)
                {
                    isAvailable = false;
                    break;
                }
            }
            return isAvailable;
        }

        public void KillAllMicroservices()
        {
            foreach(var name in this.RunningMicroservicesByName.Keys)
            {
                Stop(name);
            }
        }
    }
}
