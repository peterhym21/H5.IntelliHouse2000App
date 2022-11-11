using IntelliHouse2000App.ViewModels.Home;

namespace IntelliHouse2000App.Views;

public partial class LogPage : ContentPage
{
	private readonly LogPageViewModel _viewModel;
	public LogPage(LogPageViewModel viewModel)
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
}