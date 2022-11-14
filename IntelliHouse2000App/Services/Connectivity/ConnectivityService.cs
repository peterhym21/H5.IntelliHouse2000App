using System.Diagnostics;
using IntelliHouse2000App.ViewModels;

namespace IntelliHouse2000App.Services.Connectivity;

public class ConnectivityService : IConnectivityService
{
    private readonly IConnectivity _connectivity;
    private readonly IMQTTService _mqttService;
    private static System.Timers.Timer _reconnectTimer;
    public ConnectivityService(IConnectivity connectivity, IMQTTService mqttService)
    {
        _connectivity = connectivity;
        _mqttService = mqttService;
        
        _reconnectTimer = new System.Timers.Timer(5000);
        _reconnectTimer.Elapsed += (sender, args) =>
        {
            Debug.WriteLine("MQTT reconnect timer tick");
            _mqttService.Connect();
        };
    }
    
    public void Init(BaseViewModel viewModel)
    {
        viewModel.HasInternetAccess =  HasInternetAccess();
        viewModel.HasMQTTAccess = IsConnectedToMQTT();
        
        _connectivity.ConnectivityChanged += (sender, args) =>
        {
            viewModel.HasInternetAccess = args.NetworkAccess == NetworkAccess.Internet;
            
            if (args.NetworkAccess == NetworkAccess.Internet) _mqttService.Connect();
        };
        
        MessagingCenter.Subscribe<MqttService>(this, Constants.MqttDisconnectedSubject, service =>
        {
            viewModel.HasMQTTAccess = IsConnectedToMQTT();

            _reconnectTimer.Start();
        });
        MessagingCenter.Subscribe<MqttService>(this, Constants.MqttConnectedSubject, service =>
        {
            viewModel.HasMQTTAccess = IsConnectedToMQTT();
            _reconnectTimer.Stop();
        });
    }
    
    private bool IsConnectedToMQTT()
    {
        return _mqttService.IsConnected();
    }
    private bool HasInternetAccess()
    {
        return _connectivity.NetworkAccess == NetworkAccess.Internet;
    }
}