using CommunityToolkit.Mvvm.ComponentModel;

namespace IntelliHouse2000App.Models;

public partial class Measurements : ObservableObject
{
    [ObservableProperty] private double temperature;

    [ObservableProperty] private double humidity;
    [ObservableProperty] private DateTime timestamp = DateTime.Now;
}