namespace IntelliHouse2000App.Models;

public class Measurements
{
    public decimal Temperature { get; set; }
    public double Humidity { get; set; }
    public DateTime Timestamp { get; set; } = DateTime.Now;
}