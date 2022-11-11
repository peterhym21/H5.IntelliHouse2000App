using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using IntelliHouse2000App.Services;

namespace IntelliHouse2000App.ViewModels;

public partial class MainPageViewModel : ObservableObject
{
    private readonly IAlarmService _alarmService;
    private readonly IMQTTService _mqttService;
    private readonly IConnectivity _connectivity;
    public MainPageViewModel(IAlarmService alarmService, IMQTTService mqttService, IConnectivity connectivity)
    {
        _alarmService = alarmService;
        _mqttService = mqttService;
        _connectivity = connectivity;
        _internetAccess = HasInternetConnection();

        _connectivity.ConnectivityChanged += (sender, args) =>
        {
            _internetAccess = args.NetworkAccess == NetworkAccess.Internet && mqttService.IsConnected();
        };
        
        MessagingCenter.Subscribe<MqttService>(this, Constants.MqttDisconnectedSubject, service =>
        {
            _internetAccess = HasInternetConnection();
        });
        MessagingCenter.Subscribe<MqttService>(this, Constants.MqttConnectedSubject, service =>
        {
            _internetAccess = HasInternetConnection();
        });
    }

    [ObservableProperty]
    private bool _internetAccess = false;
    private bool HasInternetConnection()
    {
        return _mqttService.IsConnected() && _connectivity.NetworkAccess == NetworkAccess.Internet;
    }
    
    [RelayCommand]
    public void Disarm()
    {
        _alarmService.SetArmed(ArmedState.Disarmed);
        MessagingCenter.Send(this, Constants.AlarmArmedSubject);
    }
    
    [RelayCommand]
    public void PartiallyArm()
    {
        _alarmService.SetArmed(ArmedState.PartiallyArmed);
        MessagingCenter.Send(this, Constants.AlarmPartiallyArmedSubject);
    }
    
    [RelayCommand]
    public void FullyArm()
    {
        _alarmService.SetArmed(ArmedState.FullyArmed);
        MessagingCenter.Send(this, Constants.AlarmFullyArmedSubject);
    }
}