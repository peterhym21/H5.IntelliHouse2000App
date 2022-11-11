using IntelliHouse2000App.Models;
using IntelliHouse2000App.Services;
using IntelliHouse2000App.ViewModels;

namespace IntelliHouse2000App.Views;

public partial class ClimateBedroom : ContentPage
{
    ClimateViewModel _viewModelClimate;
	public ClimateBedroom(ClimateViewModel viewModelClimate)
	{
		InitializeComponent();
		_viewModelClimate = viewModelClimate;
		BindingContext = _viewModelClimate;
        _viewModelClimate.Climate.Room = "bedroom";

        MessagingCenter.Subscribe<ClimateViewModel, string>(this, "No-Climate",
        (sender, arg) => DisplayAlert("Error", $"The error message is: {arg}!", "OK"));

        MessagingCenter.Subscribe<ClimateService, string>(this, "Set-Humid",
        (sender, arg) => DisplayAlert("Info", $"{arg}!", "OK"));

        MessagingCenter.Subscribe<ClimateService, string>(this, "Set-Temp",
        (sender, arg) => DisplayAlert("Info", $"{arg}!", "OK"));
    }

    protected override void OnAppearing()
    {
        base.OnAppearing();
        _viewModelClimate.GetClimateCommand.Execute(_viewModelClimate.Climate);
    }

}