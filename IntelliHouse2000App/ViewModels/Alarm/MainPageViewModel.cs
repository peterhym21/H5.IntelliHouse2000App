﻿using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using IntelliHouse2000App.Services;

namespace IntelliHouse2000App.ViewModels;

public partial class MainPageViewModel : ObservableObject
{
    private readonly IAlarmService _alarmService;
    public MainPageViewModel(IAlarmService alarmService)
    {
        _alarmService = alarmService;
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