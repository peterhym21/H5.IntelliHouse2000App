
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using IntelliHouse2000App.Models;
using IntelliHouse2000App.Services;
using System.Collections.ObjectModel;

namespace IntelliHouse2000App.ViewModels
{
    public partial class ClimateViewModel : BaseViewModel
    {
        private readonly ClimateService _climateService;
        public ClimateViewModel(ClimateService climateService)
        {
            _climateService = climateService;
        }

        [ObservableProperty]
        private Climate climate = new();

        [ObservableProperty]
        bool isRefreshing;


        [RelayCommand]
        async Task GetClimateAsynce(string room)
        {
            if(IsBusy) return;

            try
            {
                climate = _climateService.GetClimate(room);
            }
            catch (Exception ex)
            {
                MessagingCenter.Send(this, "No-Climate", ex.Message.ToString());
            }
            finally
            {
                IsBusy = false;
                IsRefreshing = false;
            }
        }

        [RelayCommand]
        async Task SetHumidAsync(Climate setclimate)
        {
            _climateService.SetHumid(setclimate);
        }

        [RelayCommand]
        async Task SetTempAsync(Climate setclimate)
        {
            _climateService.SetTemp(setclimate);
        }



    }
}
