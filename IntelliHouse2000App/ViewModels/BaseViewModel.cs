using CommunityToolkit.Mvvm.ComponentModel;
using IntelliHouse2000App.Services;

namespace IntelliHouse2000App.ViewModels
{
    public partial class BaseViewModel : ObservableObject
    {
        private readonly IMQTTService _mqttService;
        private readonly IConnectivity _connectivity;
        protected BaseViewModel()
        {
            _mqttService = DependencyService.Get<IMQTTService>();
            _connectivity = DependencyService.Get<IConnectivity>(); //NH_TODO: Test if this works
            
            HasInternetAccess = HasInternetConnection();
            HasMQTTAccess = IsConnectedToMQTT();

            _connectivity.ConnectivityChanged += (sender, args) =>
            {
                HasInternetAccess = args.NetworkAccess == NetworkAccess.Internet && _mqttService.IsConnected();
                
                if (args.NetworkAccess == NetworkAccess.Internet) _mqttService.Connect();
            };
        
            MessagingCenter.Subscribe<MqttService>(this, Constants.MqttDisconnectedSubject, service =>
            {
                HasInternetAccess = HasInternetConnection();
            });
            MessagingCenter.Subscribe<MqttService>(this, Constants.MqttConnectedSubject, service =>
            {
                HasInternetAccess = HasInternetConnection();
            });
        }

        [ObservableProperty]
        private bool _hasInternetAccess;
        private bool HasInternetConnection()
        {
            return _connectivity.NetworkAccess == NetworkAccess.Internet;
        }
    
        [ObservableProperty]
        private bool _hasMQTTAccess;
        private bool IsConnectedToMQTT()
        {
            return _mqttService.IsConnected();
        }
        
        [ObservableProperty]
        [NotifyPropertyChangedFor(nameof(IsNotBusy))]
        private bool isBusy = false;

        [ObservableProperty]
        private string title;

        public bool IsNotBusy => !IsBusy;

    }
}
