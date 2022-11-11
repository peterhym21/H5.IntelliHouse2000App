using IntelliHouse2000App.Services;
using IntelliHouse2000App.ViewModels;

namespace IntelliHouse2000App.Views;


public partial class LivingroomGraphsPage : ContentPage
{
	private ClimateGraphsPageViewModel _viewModel;
	public LivingroomGraphsPage(ClimateGraphsPageViewModel viewModel)
	{
		_viewModel = viewModel;
		BindingContext = _viewModel;
		InitializeComponent();

        MessagingCenter.Subscribe<ClimateService, string>(this, "Set-Humid",
		(sender, arg) => DisplayAlert("Info", $"{arg}!", "OK"));

        MessagingCenter.Subscribe<ClimateService, string>(this, "Set-Temp",
        (sender, arg) => DisplayAlert("Info", $"{arg}!", "OK"));

    }

	protected override async void OnAppearing()
	{
		base.OnAppearing();
		await _viewModel.GetLivingroomAsync(DateTime.Now);
	}
}

