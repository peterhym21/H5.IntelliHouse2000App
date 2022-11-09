using MQTTnet;
using MQTTnet.Client.Connecting;
using MQTTnet.Client.Options;

namespace IntelliHouse2000App.Services;

public interface IMQTTService
{
    public Task<bool> Publish(MqttApplicationMessage message);
    public Task<bool> Subscribe(string topic);
    public void Initialize(IMqttClientOptions mqttClientOptions);
    public Task<bool> Connect();
    public bool IsConnected();
    public Task<bool> Disconnect();
    public Task<bool> Reconnect();
}