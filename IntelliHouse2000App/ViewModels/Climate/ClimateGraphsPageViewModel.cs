using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using IntelliHouse2000App.Models;
using IntelliHouse2000App.Repository;
using IntelliHouse2000App.Services;

namespace IntelliHouse2000App.ViewModels;

public partial class ClimateGraphsPageViewModel : BaseViewModel
{
    [ObservableProperty] public List<Measurements> kitchenValues = new();
    [ObservableProperty] public List<Measurements> bedroomValues = new();
    [ObservableProperty] public List<Measurements> livingroomValues = new();
    [ObservableProperty] private Climate climate = new();
    private readonly IGenericRepository _repository;
    private readonly ClimateService _climateService;

    public ClimateGraphsPageViewModel(IGenericRepository repo, ClimateService climateService)
    {
        _repository = repo;
        _climateService = climateService;
    }

    [RelayCommand]
    public async Task GetKitchenAsync(DateTime? ts)
    {
        DateTime timeStamp = ts ?? DateTime.Now;

        var logs = await _repository.GetAsync<List<Measurements>>(new Uri(Constants.ApiBaseUrl + $"kitchen?ts={timeStamp}"));
        KitchenValues = logs.OrderBy(t => t.Timestamp).ToList();
    }

    [RelayCommand]
    public async Task GetLivingroomAsync(DateTime? ts)
    {
        DateTime timeStamp = ts ?? DateTime.Now;

        var logs = await _repository.GetAsync<List<Measurements>>(new Uri(Constants.ApiBaseUrl + $"livingroom?ts={timeStamp}"));
        LivingroomValues = logs.OrderBy(t => t.Timestamp).ToList();
    }

    [RelayCommand]
    public async Task GetBedroomAsync(DateTime? ts)
    {
        DateTime timeStamp = ts ?? DateTime.Now;

        var logs = await _repository.GetAsync<List<Measurements>>(new Uri(Constants.ApiBaseUrl + $"bedroom?ts={timeStamp}"));
        BedroomValues = logs.OrderBy(t => t.Timestamp).ToList();
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