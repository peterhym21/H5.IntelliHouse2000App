using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;

namespace IntelliHouse2000App.ViewModels;

public partial class MainPageViewModel : ObservableObject
{
    public MainPageViewModel()
    {
        
    }
    
    [RelayCommand]
    public void Disarm()
    {
        Shell.Current.DisplayAlert("Test", "Imagine the alarm was disarmed", "cancel");
        
    }
    
    [RelayCommand]
    public void PartiallyArm()
    {
        Shell.Current.DisplayAlert("Test", "Imagine the alarm was partially armed", "cancel");
    }
    
    [RelayCommand]
    public void FullyArm()
    {
        Shell.Current.DisplayAlert("Test", "Imagine the alarm was fully armed", "cancel");
    }
}