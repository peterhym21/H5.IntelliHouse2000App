using IntelliHouse2000App.ViewModels.Home;

namespace IntelliHouse2000App.Views;

public partial class InfoLogPage : ContentPage
{
	private readonly LogPageViewModel _viewModel;
	public InfoLogPage(LogPageViewModel viewModel)
	{
		BindingContext = viewModel;
		_viewModel = viewModel;
		InitializeComponent();
	}
	
	protected override async void OnAppearing()
	{
		base.OnAppearing();
		await _viewModel.GetInfoLogsAsync();
	}

	private async void RefreshView_OnRefreshing(object sender, EventArgs e)
	{
		await _viewModel.GetInfoLogsAsync();
	}
}