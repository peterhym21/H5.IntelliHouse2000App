using System.Collections.ObjectModel;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using IntelliHouse2000App.Models;
using IntelliHouse2000App.Repository;
using IntelliHouse2000App.Services.Connectivity;

namespace IntelliHouse2000App.ViewModels.Home;

public partial class LogPageViewModel : BaseViewModel
{
    // ReSharper disable once InconsistentNaming
    [ObservableProperty] private ObservableCollection<LogMessage> logMessages;
    private readonly IGenericRepository _repository;

    public LogPageViewModel(IGenericRepository repository, IConnectivityService connectivityService) : base(connectivityService)
    {
        _repository = repository;
    }

    [RelayCommand]
    public async Task GetCriticalLogsAsync()
    {
        var logs = await _repository.GetAsync<List<LogMessage>>(new Uri(Constants.ApiBaseUrl + "critical"));
        if (logs != null)
        {
            LogMessages = new ObservableCollection<LogMessage>(logs.Take(10));
            for (int i = 0; i < 10; i++)
            {
                Preferences.Set($"CritMes{i}", LogMessages[i].Message);
                Preferences.Set($"CritTS{i}", LogMessages[i].Timestamp);
                Preferences.Set($"CritSend{i}", LogMessages[i].Client);
            }
        }
        else
        {
            LogMessages = new ObservableCollection<LogMessage>();
            for (int i = 0; i < 10; i++)
            {
                LogMessages.Add(new LogMessage
                {
                    Message = Preferences.Get($"CritMes{i}", "No message found"),
                    Timestamp = Preferences.Get($"CritTS{i}", DateTime.Now),
                    Client = Preferences.Get($"CritSend{i}", "System")
                });
            }
        }
    }

    [RelayCommand]
    public async Task GetInfoLogsAsync()
    {
        var logs = await _repository.GetAsync<List<LogMessage>>(new Uri(Constants.ApiBaseUrl + "info"));
        if (logs != null)
        {
            LogMessages = new ObservableCollection<LogMessage>(logs.Take(10));
            for (int i = 0; i < 10; i++)
            {
                Preferences.Set($"InfoMes{i}", LogMessages[i].Message);
                Preferences.Set($"InfoTS{i}", LogMessages[i].Timestamp);
                Preferences.Set($"InfoSend{i}", LogMessages[i].Client);
            }
        }
        else
        {
            LogMessages = new ObservableCollection<LogMessage>();
            for (int i = 0; i < 10; i++)
            {
                LogMessages.Add(new LogMessage
                {
                    Message = Preferences.Get($"InfoMes{i}", "No message found"),
                    Timestamp = Preferences.Get($"InfoTS{i}", DateTime.Now),
                    Client = Preferences.Get($"InfoSend{i}", "System")
                });
            }
        }
    }

    [RelayCommand]
    public async Task GetSystemLogsAsync()
    {
        var logs = await _repository.GetAsync<List<LogMessage>>(new Uri(Constants.ApiBaseUrl + "system"));
        if (logs != null)
        {
            LogMessages = new ObservableCollection<LogMessage>(logs.Take(10));
            for (int i = 0; i < 10; i++)
            {
                Preferences.Set($"SysMes{i}", LogMessages[i].Message);
                Preferences.Set($"SysTS{i}", LogMessages[i].Timestamp);
                Preferences.Set($"SysSend{i}", LogMessages[i].Client);
            }
        }
        else
        {
            LogMessages = new ObservableCollection<LogMessage>();
            for (int i = 0; i < 10; i++)
            {
                LogMessages.Add(new LogMessage
                {
                    Message = Preferences.Get($"SysMes{i}", "No message found"),
                    Timestamp = Preferences.Get($"SysTS{i}", DateTime.Now),
                    Client = Preferences.Get($"SysSend{i}", "System")
                });
            }
        }
    }
}
