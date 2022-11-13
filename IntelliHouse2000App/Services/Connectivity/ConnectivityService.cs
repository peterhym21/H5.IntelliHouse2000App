using IntelliHouse2000App.ViewModels;

namespace IntelliHouse2000App.Services.Connectivity;

public class ConnectivityService : IConnectivityService
{
    private readonly IConnectivity _connectivity;
    private readonly IMQTTService _mqttService;
    public ConnectivityService(IConnectivity connectivity, IMQTTService mqttService)
    {
        _connectivity = connectivity;
        _mqttService = mqttService;
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
        });
        MessagingCenter.Subscribe<MqttService>(this, Constants.MqttConnectedSubject, service =>
        {
            viewModel.HasMQTTAccess = IsConnectedToMQTT();
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