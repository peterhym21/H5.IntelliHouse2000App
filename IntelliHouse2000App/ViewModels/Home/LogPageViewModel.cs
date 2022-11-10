using System.Collections.ObjectModel;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using IntelliHouse2000App.Models;
using IntelliHouse2000App.Repository;

namespace IntelliHouse2000App.ViewModels.Home;

public class LogPageViewModel : ObservableObject
{
    public ObservableCollection<LogMessage> LogMessages { get; set; } = new ObservableCollection<LogMessage>
    {
        new LogMessage
        {
            Id = 1,
            Client = "Test",
            Message = "Test besked",
            Retain = true,
            Timestamp = DateTime.Now,
            Topic = "Something something",
            QoS = 1
        },
        new LogMessage
        {
            Id = 2,
            Client = "Tes2",
            Message = "Test besked",
            Retain = false,
            Timestamp = DateTime.Now,
            Topic = "Something something darkside",
            QoS = 0
        }
    };

    private readonly IGenericRepository _repository;

    public LogPageViewModel(IGenericRepository repository)
    {
        _repository = repository;
    }

    [RelayCommand]
    public async void GetCriticalLogsAsync()
    {
        var something = await _repository.GetAsync<List<LogMessage>>(new Uri(Constants.ApiBaseUrl));
        LogMessages = new ObservableCollection<LogMessage>(something);
    }
}