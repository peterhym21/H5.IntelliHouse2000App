using IntelliHouse2000App.ViewModels.Home;

namespace IntelliHouse2000App.Views;

public partial class CriticalLogPage : ContentPage
{
	private readonly LogPageViewModel _viewModel;
	public CriticalLogPage(LogPageViewModel viewModel)
	{
		BindingContext = viewModel;
		_viewModel = viewModel;
		InitializeComponent();
	}
	
	protected override async void OnAppearing()
	{
		base.OnAppearing();
		await _viewModel.GetCriticalLogsAsync();
	}

	private async void RefreshView_OnRefreshing(object sender, EventArgs e)
	{
		await _viewModel.GetCriticalLogsAsync();
	}
}