using IntelliHouse2000App.Helpers;
using IntelliHouse2000App.ViewModels;

namespace IntelliHouse2000App.Views;

[LifeTime(ServiceLifetime.Singleton)]
public partial class MainPage : ContentPage
{
	private readonly MainPageViewModel _vm;
	public MainPage(MainPageViewModel vm)
	{
		InitializeComponent();

		_vm = vm;
        BindingContext = vm;
		
		MessagingCenter.Subscribe<MainPageViewModel, bool>(this, Constants.AlarmArmedSubject, OnAlarmArmed);
		MessagingCenter.Subscribe<MainPageViewModel, bool>(this, Constants.AlarmPartiallyArmedSubject, OnAlarmPartiallyArmed);
		MessagingCenter.Subscribe<MainPageViewModel, bool>(this, Constants.AlarmFullyArmedSubject, OnAlarmFullyArmed);
	}

	private void OnAlarmFullyArmed(MainPageViewModel sender, bool success)
	{
		if (success) DisplayAlert("Alarm", "Alarm has been fully armed", "Ok");
    }
	private void OnAlarmPartiallyArmed(MainPageViewModel sender, bool success)
	{
        if (success) DisplayAlert("Alarm", "Alarm has been partially armed", "Ok");

    }
	private void OnAlarmArmed(MainPageViewModel sender, bool success)
	{
        if (success) DisplayAlert("Alarm", "Alarm has been armed", "Ok");
    }

	protected override void OnAppearing()
	{
		base.OnAppearing();
		if (!_vm.HasInternetAccess) DisplayAlert("No internet", "No internet access", "Ok");
		else if (!_vm.HasMQTTAccess) DisplayAlert("No MQTT", "No MQTT access", "Ok");
	}
}
