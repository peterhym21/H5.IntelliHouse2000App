using IntelliHouse2000App.ViewModels.Climate;

namespace IntelliHouse2000App.Views;


public partial class ClimateGraphsPage : ContentPage
{
	private ClimateGraphsPageViewModel _viewModel;
	public ClimateGraphsPage(ClimateGraphsPageViewModel viewModel)
	{
		_viewModel = viewModel;
		BindingContext = _viewModel;
		InitializeComponent();
	}

	protected override async void OnAppearing()
	{
		base.OnAppearing();
		await _viewModel.GetBedroomAsync(DateTime.Now);
		await _viewModel.GetKitchenAsync(DateTime.Now.AddDays(-2));
		await _viewModel.GetLivingroomAsync(DateTime.Now.AddHours(5));
	}
}

