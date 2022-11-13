using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using IntelliHouse2000App.Services;
using IntelliHouse2000App.Services.Connectivity;
using Microsoft.Maui.Controls.Internals;

namespace IntelliHouse2000App.ViewModels;

public partial class MainPageViewModel : BaseViewModel
{
    private readonly IAlarmService _alarmService;
    public MainPageViewModel(IAlarmService alarmService, IConnectivityService connectivityService) : base(connectivityService)
    {
        _alarmService = alarmService;
    }
    
    [RelayCommand]
    public async Task DisarmAsync()
    {
        bool success = await _alarmService.SetArmedAsync(ArmedState.Disarmed);
        MessagingCenter.Send(this, Constants.AlarmArmedSubject, success);
    }

    [RelayCommand]
    public async Task PartiallyArmAsync()
    {
        bool success = await _alarmService.SetArmedAsync(ArmedState.PartiallyArmed);
        MessagingCenter.Send(this, Constants.AlarmPartiallyArmedSubject, success);
    }
    
    [RelayCommand]
    public async Task FullyArmAsync()
    {
        bool success = await _alarmService.SetArmedAsync(ArmedState.FullyArmed);
        MessagingCenter.Send(this, Constants.AlarmFullyArmedSubject, success);
    }
}