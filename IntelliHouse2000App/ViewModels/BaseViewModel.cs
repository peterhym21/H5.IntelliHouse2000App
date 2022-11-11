using CommunityToolkit.Mvvm.ComponentModel;

namespace IntelliHouse2000App.ViewModels
{
    public partial class BaseViewModel : ObservableObject
    {
        [ObservableProperty]
        [NotifyPropertyChangedFor(nameof(IsNotBusy))]
        private bool isBusy = false;

        [ObservableProperty]
        private string title;

        public bool IsNotBusy => !IsBusy;

    }
}
