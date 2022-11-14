using CommunityToolkit.Mvvm.ComponentModel;

namespace IntelliHouse2000App.Models
{
    public partial class Climate : ObservableObject
    {
        [ObservableProperty]
        private string room;
        [ObservableProperty]
        private int setTemp;
        [ObservableProperty]
        private int setHumid;


    }
}
