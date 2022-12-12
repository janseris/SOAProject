namespace Microservices.IoT.Sensor.RestAPI.Models
{
    /// <summary>
    /// Information retrieved within one tick of a sensor from its REST API
    /// </summary>
    public class FridgeSensorStats
    {
        /// <summary>
        /// Degrees celsius
        /// </summary>
        public double Temperature { get; set; }
        
        /// <summary>
        /// Watts
        /// </summary>
        public int PowerConsumption { get; set; }
        
        public bool IsDoorOpen { get; set; }
        
        public List<string> CurrentWarnings { get; set; }
    }
}
