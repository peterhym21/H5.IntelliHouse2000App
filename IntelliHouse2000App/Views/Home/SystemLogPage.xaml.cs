using IntelliHouse2000App.ViewModels.Home;

namespace IntelliHouse2000App.Views;

public partial class SystemLogPage : ContentPage
{
	private readonly LogPageViewModel _viewModel;
	public SystemLogPage(LogPageViewModel viewModel)
	{
		BindingContext = viewModel;
		_viewModel = viewModel;
		InitializeComponent();
	}
	
	protected override async void OnAppearing()
	{
		base.OnAppearing();
		await _viewModel.GetSystemLogsAsync();
	}

	private async void RefreshView_OnRefreshing(object sender, EventArgs e)
	{
		await _viewModel.GetSystemLogsAsync();
	}
}