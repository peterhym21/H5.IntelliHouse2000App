using IntelliHouse2000App.ViewModels;

namespace IntelliHouse2000App.Views;

public partial class ClimateKitchen : ContentPage
{
    ClimateViewModel _viewModelClimate;
    public ClimateKitchen(ClimateViewModel viewModelClimate)
    {
        InitializeComponent();
        this._viewModelClimate = viewModelClimate;
        BindingContext = _viewModelClimate;
        _viewModelClimate.Climate.Room = "kitchen";

        MessagingCenter.Subscribe<ClimateViewModel, string>(this, "No-Climate",
        (sender, arg) => DisplayAlert("Error", $"The error message is: {arg}!", "OK"));
    }

    protected override void OnAppearing()
    {
        base.OnAppearing();
        _viewModelClimate.GetClimateAsynceCommand.Execute(_viewModelClimate.Climate.Room);
    }
}