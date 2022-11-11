using System.Collections.ObjectModel;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using IntelliHouse2000App.Models;
using IntelliHouse2000App.Repository;

namespace IntelliHouse2000App.ViewModels;

public partial class ClimateGraphsPageViewModel : ObservableObject
{
    [ObservableProperty] public List<Measurements> kitchenValues = new ();
    [ObservableProperty] public List<Measurements> bedroomValues = new ();
    [ObservableProperty] public List<Measurements> livingroomValues = new ();
    private readonly IGenericRepository _repository;

    public ClimateGraphsPageViewModel(IGenericRepository repo)
    {
        _repository = repo;
    }
    
    [RelayCommand]
    public async Task GetKitchenAsync(DateTime? ts)
    {
        DateTime timeStamp = ts ?? DateTime.Now;
        
        var logs = await _repository.GetAsync<List<Measurements>>(new Uri(Constants.ApiBaseUrl + $"kitchen?ts={timeStamp}"));
        KitchenValues = logs;
    }

    [RelayCommand]
    public async Task GetLivingroomAsync(DateTime? ts)
    {
        DateTime timeStamp = ts ?? DateTime.Now;
        
        var logs = await _repository.GetAsync<List<Measurements>>(new Uri(Constants.ApiBaseUrl + $"livingroom?ts={timeStamp}"));
        LivingroomValues = logs;
    }
    
    [RelayCommand]
    public async Task GetBedroomAsync(DateTime? ts)
    {
        DateTime timeStamp = ts ?? DateTime.Now;
        
        var logs = await _repository.GetAsync<List<Measurements>>(new Uri(Constants.ApiBaseUrl + $"bedroom?ts={timeStamp}"));
        BedroomValues = logs;
    }
}