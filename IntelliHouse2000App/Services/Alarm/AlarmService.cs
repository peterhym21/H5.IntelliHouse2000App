using System.Text;
using MQTTnet;
using MQTTnet.Protocol;

namespace IntelliHouse2000App.Services;

public class AlarmService : IAlarmService
{
    private readonly IMQTTService _mqttService;
    public AlarmService(IMQTTService mqttService)
    {
        _mqttService = mqttService;
    }

    public Task<bool> SetArmedAsync(ArmedState state)
    {
        string payload = state switch
        {
             ArmedState.Disarmed => "0",
             ArmedState.PartiallyArmed => "1",
             ArmedState.FullyArmed => "2",
            _ => throw new ArgumentOutOfRangeException(nameof(state), "Value was not a valid arm state")
        };
        
        return _mqttService.Publish(new MqttApplicationMessage()
        {
            Topic = "home/alarm/arm",
            Payload = Encoding.UTF8.GetBytes(payload),
            Retain = true,
            QualityOfServiceLevel = MqttQualityOfServiceLevel.AtLeastOnce
        });
    }
}
public enum ArmedState
{
    Disarmed,
    PartiallyArmed,
    FullyArmed,
}