using System.Collections.ObjectModel;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using IntelliHouse2000App.Models;
using IntelliHouse2000App.Repository;

namespace IntelliHouse2000App.ViewModels.Home;

public partial class LogPageViewModel : ObservableObject
{
    [ObservableProperty] public ObservableCollection<LogMessage> logMessages;
    private readonly IGenericRepository _repository;

    public LogPageViewModel(IGenericRepository repository)
    {
        _repository = repository;
    }

    [RelayCommand]
    public async Task GetCriticalLogsAsync()
    {
        var logs = await _repository.GetAsync<List<LogMessage>>(new Uri(Constants.ApiBaseUrl + "critical"));
        if (logs != null) LogMessages = new ObservableCollection<LogMessage>(logs.Take(10));
    }

    [RelayCommand]
    public async Task GetInfoLogsAsync()
    {
        var logs = await _repository.GetAsync<List<LogMessage>>(new Uri(Constants.ApiBaseUrl + "info"));
        if (logs != null) LogMessages = new ObservableCollection<LogMessage>(logs.Take(10));
    }
    
    [RelayCommand]
    public async Task GetSystemLogsAsync()
    {
        var logs = await _repository.GetAsync<List<LogMessage>>(new Uri(Constants.ApiBaseUrl + "system"));
        if (logs != null) LogMessages = new ObservableCollection<LogMessage>(logs.Take(10));
    }
}