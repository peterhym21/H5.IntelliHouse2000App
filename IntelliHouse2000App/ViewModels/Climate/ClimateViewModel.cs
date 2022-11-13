
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using IntelliHouse2000App.Models;
using IntelliHouse2000App.Services;
using System.Collections.ObjectModel;
using IntelliHouse2000App.Services.Connectivity;

namespace IntelliHouse2000App.ViewModels
{
    public partial class ClimateViewModel : BaseViewModel
    {
        private readonly ClimateService _climateService;
        public ClimateViewModel(ClimateService climateService, IConnectivityService connectivityService) : base(connectivityService)
        {
            _climateService = climateService;
        }

        [ObservableProperty]
        private Climate climate = new();

        [ObservableProperty]
        bool isRefreshing;


        [RelayCommand]
        async Task GetClimateAsync(Climate climate)
        {
            if(IsBusy) return;

            try
            {
                Climate = await _climateService.GetClimateService(climate);;
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
}
