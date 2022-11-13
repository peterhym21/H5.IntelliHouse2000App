using CommunityToolkit.Mvvm.ComponentModel;
using IntelliHouse2000App.Services.Connectivity;

namespace IntelliHouse2000App.ViewModels
{
    public partial class BaseViewModel : ObservableObject
    {
        protected BaseViewModel(IConnectivityService connectivityService)
        {
            connectivityService.Init(this);
        }

        [ObservableProperty]
        [NotifyPropertyChangedFor(nameof(HasNoInternetAccess))]
        private bool _hasInternetAccess = false;
    
        [ObservableProperty]
        [NotifyPropertyChangedFor(nameof(HasNoMQTTAccess))]
        private bool _hasMQTTAccess = false;
        
        [ObservableProperty]
        [NotifyPropertyChangedFor(nameof(IsNotBusy))]
        private bool isBusy = false;

        [ObservableProperty]
        private string title;

        public bool IsNotBusy => !IsBusy;
        public bool HasNoMQTTAccess => !HasMQTTAccess;
        public bool HasNoInternetAccess => !HasInternetAccess;
    }
}
