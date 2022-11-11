using System.Collections.ObjectModel;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using IntelliHouse2000App.Models;
using IntelliHouse2000App.Repository;

namespace IntelliHouse2000App.ViewModels.Climate;

public partial class ClimateGraphsPageViewModel : ObservableObject
{
    [ObservableProperty] public ObservableCollection<Measurements> kitchenValues = new ();
    [ObservableProperty] public ObservableCollection<Measurements> bedroomValues = new ();
    [ObservableProperty] public ObservableCollection<Measurements> livingroomValues = new ();
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
        KitchenValues = new ObservableCollection<Measurements>(logs);
    }

    [RelayCommand]
    public async Task GetLivingroomAsync(DateTime? ts)
    {
        DateTime timeStamp = ts ?? DateTime.Now;
        
        var logs = await _repository.GetAsync<List<Measurements>>(new Uri(Constants.ApiBaseUrl + $"livingroom?ts={timeStamp}"));
        LivingroomValues = new ObservableCollection<Measurements>(logs);
    }
    
    [RelayCommand]
    public async Task GetBedroomAsync(DateTime? ts)
    {
        DateTime timeStamp = ts ?? DateTime.Now;
        
        var logs = await _repository.GetAsync<List<Measurements>>(new Uri(Constants.ApiBaseUrl + $"bedroom?ts={timeStamp}"));
        BedroomValues = new ObservableCollection<Measurements>(logs);
    }
}