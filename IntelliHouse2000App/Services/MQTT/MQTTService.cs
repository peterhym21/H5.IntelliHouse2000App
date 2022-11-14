using System.Diagnostics;
using System.Net.Sockets;
using System.Text;
using IntelliHouse2000App.Helpers;
using MQTTnet;
using MQTTnet.Client;
using MQTTnet.Client.Connecting;
using MQTTnet.Client.Disconnecting;
using MQTTnet.Client.Options;
using MQTTnet.Client.Receiving;
using MQTTnet.Exceptions;
using Polly;
using Polly.Retry;

namespace IntelliHouse2000App.Services;

[LifeTime(ServiceLifetime.Singleton)]
public class MqttService : IMQTTService
{
    private IMqttClient _mqttClient;
    private IMqttClientOptions _mqttClientOptions;

    private event EventHandler<MqttClientConnectedEventArgs> Connected;
    private event EventHandler<MqttClientDisconnectedEventArgs> Disconnected;
    private event EventHandler<MqttApplicationMessageReceivedEventArgs> MessageReceived;

    public MqttService()
    {
        Initialize(new MqttClientOptionsBuilder()
            .WithClientId(GenerateUniqueClientId())
            .WithCleanSession(true)
            .WithTcpServer(Constants.MqttBaseUrl)
            .WithCredentials(new MqttClientCredentials
            {
                Username = Constants.mqttUser,
                Password = Encoding.UTF8.GetBytes(Constants.mqttPass)
            })
            .Build());
        
        var _ = Task.Run(async () => await Connect()).Result;
        Connected += (sender, args) => MessagingCenter.Send(this, Constants.MqttConnectedSubject);
        Disconnected += (sender, args) => MessagingCenter.Send(this, Constants.MqttDisconnectedSubject);
        MessageReceived += (sender, args) => MessagingCenter.Send(this, Constants.MqttMessageReceivedSubject);
    }
    
    public bool IsConnected()
    {
        return _mqttClient.IsConnected;
    }

    public async Task<bool> Connect()
    {
        try
        {
            await GetMqttConnectPolicy().ExecuteAsync(async () =>
            {
                if (!_mqttClient.IsConnected) await _mqttClient.ConnectAsync(_mqttClientOptions);   
            });
        }
        catch (Exception ex)
        {
            return false;
        }

        return _mqttClient.IsConnected;
    }

    public async Task<bool> Disconnect()
    {
        try
        {
            await _mqttClient.DisconnectAsync();
        }
        catch (Exception ex)
        {
            return false;
        }

        return true;
    }

    public void Initialize(IMqttClientOptions mqttClientOptions)
    {
        try
        {
            _mqttClientOptions = mqttClientOptions;
            var factory = new MqttFactory();
            _mqttClient = factory.CreateMqttClient();
            _mqttClient.ConnectedHandler = new MqttClientConnectedHandlerDelegate(mqttClientConnectedEventArgs =>
            {
                Connected?.Invoke(this, mqttClientConnectedEventArgs);
            });
            _mqttClient.DisconnectedHandler = new MqttClientDisconnectedHandlerDelegate(disconnectEventArgs =>
            {
                Disconnected?.Invoke(this, disconnectEventArgs);
            });
            _mqttClient.ApplicationMessageReceivedHandler =
                new MqttApplicationMessageReceivedHandlerDelegate(messageReceivedArgs =>
                {
                    MessageReceived?.Invoke(this, messageReceivedArgs);
                });
        }
        catch (Exception ex)
        {
            // Blergh
        }
    }

    public async Task<bool> Subscribe(string topic)
    {
        try
        {
            await _mqttClient.SubscribeAsync(topic);
        }
        catch (Exception ex)
        {
            return false;
        }

        return true;
    }

    public async Task<bool> Publish(MqttApplicationMessage message)
    {
        try
        {
            await Connect();
            await _mqttClient.PublishAsync(message);
        }
        catch (Exception ex)
        {
            return false;
        }

        return true;
    }
    
    private static AsyncRetryPolicy GetMqttConnectPolicy()
    {
        return Policy.Handle<SocketException>()
                     .Or<MqttCommunicationTimedOutException>()
                     .WaitAndRetryAsync(retryCount: 3,
                                        sleepDurationProvider: attempt => TimeSpan.FromSeconds(3),
                                        onRetry: (exception, span) =>
                                        {
                                            Debug.WriteLine("MQTT Connection retry");
                                        });
    }
    
    private string GenerateUniqueClientId()
    {
        string guid = Guid.NewGuid().ToString();
        string randomValue = guid.Substring(0, 12).Replace("-", "");
        string clientId = $"MQTT_APP_{randomValue}";

        return clientId;
    }
}

internal class MqttConnectedHandler : IMqttClientConnectedHandler
{
    Action<MqttClientConnectedEventArgs> _connectedAction;

    public MqttConnectedHandler(Action<MqttClientConnectedEventArgs> connectedAction)
    {
        _connectedAction = connectedAction;
    }

    public async Task HandleConnectedAsync(MqttClientConnectedEventArgs eventArgs)
    {
        _connectedAction?.Invoke(eventArgs);
    }
}

internal class MqttDisconnectedHandler : IMqttClientDisconnectedHandler
{
    Action<MqttClientDisconnectedEventArgs> _disconnectedAction;

    public MqttDisconnectedHandler(Action<MqttClientDisconnectedEventArgs> disconnectAction)
    {
        _disconnectedAction = disconnectAction;
    }

    public async Task HandleDisconnectedAsync(MqttClientDisconnectedEventArgs eventArgs)
    {
        _disconnectedAction?.Invoke(eventArgs);
    }
}

internal class MqttMessageReceivedHandler : IMqttApplicationMessageReceivedHandler
{
    Action<MqttApplicationMessageReceivedEventArgs> _messageReceived;

    public MqttMessageReceivedHandler(Action<MqttApplicationMessageReceivedEventArgs> messageReceived)
    {
        _messageReceived = messageReceived;
    }

    public async Task HandleApplicationMessageReceivedAsync(MqttApplicationMessageReceivedEventArgs eventArgs)
    {
        _messageReceived?.Invoke(eventArgs);
    }
}