using System.Collections.ObjectModel;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using IntelliHouse2000App.Models;
using IntelliHouse2000App.Repository;
using IntelliHouse2000App.Services.Connectivity;
using IntelliHouse2000App.Services;

namespace IntelliHouse2000App.ViewModels;

public partial class ClimateGraphsPageViewModel : BaseViewModel
{
    [ObservableProperty] private ObservableCollection<Measurements> kitchenValues = new();
    [ObservableProperty] private ObservableCollection<Measurements> bedroomValues = new();
    [ObservableProperty] private ObservableCollection<Measurements> livingroomValues = new();
    [ObservableProperty] private Climate climate = new();
    private readonly IGenericRepository _repository;
    private readonly ClimateService _climateService;

    public ClimateGraphsPageViewModel(IGenericRepository repo, ClimateService climateService, IConnectivityService connectivityService) : base(connectivityService)
    {
        _repository = repo;
        _climateService = climateService;
    }

    [RelayCommand]
    public async Task GetKitchenAsync(DateTime? ts)
    {
        DateTime timeStamp = ts ?? DateTime.Now;

        var logs = await _repository.GetAsync<List<Measurements>>(new Uri(Constants.ApiBaseUrl + $"kitchen?ts={timeStamp}"));
        if (logs == null) GetOldValues();
        else
        {
            KitchenValues = new ObservableCollection<Measurements>(logs.OrderBy(t => t.Timestamp).ToList());            
            for (int i = 0; i < 10; i++)
            {
                Preferences.Set($"KitchenTS{i}", logs[i].Timestamp);
                Preferences.Set($"KitchenTemp{i}", logs[i].Temperature);
                Preferences.Set($"KitchenHumidity{i}", logs[i].Humidity);
            }
        } 
    }

    [RelayCommand]
    public async Task GetLivingroomAsync(DateTime? ts)
    {
        DateTime timeStamp = ts ?? DateTime.Now;

        var logs = await _repository.GetAsync<List<Measurements>>(new Uri(Constants.ApiBaseUrl + $"livingroom?ts={timeStamp}"));
        if (logs == null) GetOldValues();
        else
        {
            LivingroomValues = new ObservableCollection<Measurements>(logs.OrderBy(t => t.Timestamp).ToList());            
            for (int i = 0; i < 10; i++)
            {
                Preferences.Set($"LivingroomTS{i}", logs[i].Timestamp);
                Preferences.Set($"LivingroomTemp{i}", logs[i].Temperature);
                Preferences.Set($"LivingroomHumidity{i}", logs[i].Humidity);
            }
        }
    }

    [RelayCommand]
    public async Task GetBedroomAsync(DateTime? ts)
    {
        DateTime timeStamp = ts ?? DateTime.Now;

        var logs = await _repository.GetAsync<List<Measurements>>(new Uri(Constants.ApiBaseUrl + $"bedroom?ts={timeStamp}"));
        if (logs == null) GetOldValues();
        else
        {
            BedroomValues = new ObservableCollection<Measurements>(logs.OrderBy(t => t.Timestamp).ToList());            
            for (int i = 0; i < 10; i++)
            {
                Preferences.Set($"BedroomTS{i}", logs[i].Timestamp);
                Preferences.Set($"BedroomTemp{i}", logs[i].Temperature);
                Preferences.Set($"BedroomHumidity{i}", logs[i].Humidity);
            }
        }
    }

    private void GetOldValues()
    {
        KitchenValues = new ObservableCollection<Measurements>();
        BedroomValues = new ObservableCollection<Measurements>();
        LivingroomValues = new ObservableCollection<Measurements>();
        int j = 9;
        for (int i = 0; i < 10; i++)
        {
            KitchenValues.Add(new Measurements()
            {
                Timestamp = Preferences.Get($"KitchenTS{i}", DateTime.Now.AddHours(-i)),
                Temperature = Preferences.Get($"KitchenTemp{i}", i * 10),
                Humidity = Preferences.Get($"KitchenHumidity{i}", j * 10)
            });
            j--;
        }

        j = 9;
        for (int i = 0; i < 10; i++)
        {
            BedroomValues.Add(new Measurements()
            {
                Timestamp = Preferences.Get($"BedroomTS{i}", DateTime.Now.AddHours(-i)),
                Temperature = Preferences.Get($"BedroomTemp{i}", i * 10),
                Humidity = Preferences.Get($"BedroomHumidity{i}", j * 10)
            });
            j--;
        }

        j = 9;
        for (int i = 0; i < 10; i++)
        {
            LivingroomValues.Add(new Measurements()
            {
                Timestamp = Preferences.Get($"LivingroomTS{i}", DateTime.Now.AddHours(-i)),
                Temperature = Preferences.Get($"LivingroomTemp{i}", i * 10),
                Humidity = Preferences.Get($"LivingroomHumidity{i}", j * 10)
            });
            j--;
        }
    }


    [RelayCommand]
    async Task SetHumidAsync(Climate climate)
    {
        _climateService.SetHumidService(climate);
    }

    [RelayCommand]
    async Task SetTempAsync(Climate climate)
    {
        _climateService.SetTempService(climate);
    }
}