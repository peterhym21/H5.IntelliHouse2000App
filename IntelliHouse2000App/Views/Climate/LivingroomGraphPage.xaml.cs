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
	}

	protected override async void OnAppearing()
	{
		base.OnAppearing();
		await _viewModel.GetLivingroomAsync(DateTime.Now);
	}
}

