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
        private bool _hasInternetAccess;
    
        [ObservableProperty]
        private bool _hasMQTTAccess;
        
        [ObservableProperty]
        [NotifyPropertyChangedFor(nameof(IsNotBusy))]
        private bool isBusy = false;

        [ObservableProperty]
        private string title;

        public bool IsNotBusy => !IsBusy;
    }
}
