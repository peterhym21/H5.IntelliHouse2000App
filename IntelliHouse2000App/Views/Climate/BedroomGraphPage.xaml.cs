using IntelliHouse2000App.ViewModels;

namespace IntelliHouse2000App.Views;


public partial class BedroomGraphsPage : ContentPage
{
	private ClimateGraphsPageViewModel _viewModel;
	public BedroomGraphsPage(ClimateGraphsPageViewModel viewModel)
	{
		_viewModel = viewModel;
		BindingContext = _viewModel;
		InitializeComponent();
	}

	protected override async void OnAppearing()
	{
		base.OnAppearing();
		await _viewModel.GetBedroomAsync(DateTime.Now);
	}
}

