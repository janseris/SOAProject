namespace Microservices.IoT.Sensor.RestAPI.Models
{
    public class FridgeSensor
    {
        public int FridgeID { get; set; }
        public List<FridgeSensorStats> Data { get; set; } = new List<FridgeSensorStats>();
        public TimeSpan UpdatePeriod { get; set; }
        public DateTime LastUpdate { get; set; }
        public TimeSpan Uptime => DateTime.Now - LastUpdate;
        public double MaxTemperature => Data.Max(item => item.Temperature);
        public double MinTemperature => Data.Min(item => item.Temperature);
        public double AverageTemperature => Data.Average(item => item.Temperature);
    }
}
