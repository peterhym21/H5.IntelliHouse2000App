using IntelliHouse2000App.Helpers;
using IntelliHouse2000App.ViewModels;

namespace IntelliHouse2000App.Views;

[LifeTime(ServiceLifetime.Singleton)]
public partial class MainPage : ContentPage
{
	public MainPage(MainPageViewModel vm)
	{
		InitializeComponent();
		BindingContext = vm;
		
		MessagingCenter.Subscribe<MainPageViewModel>(this, Constants.AlarmArmedSubject, _ => DisplayAlert("Alarm", "Alarm has been armed", "Ok", "Cancel"));
		MessagingCenter.Subscribe<MainPageViewModel>(this, Constants.AlarmPartiallyArmedSubject, _ => DisplayAlert("Alarm", "Alarm has been partially armed", "Ok", "Cancel"));
		MessagingCenter.Subscribe<MainPageViewModel>(this, Constants.AlarmFullyArmedSubject, _ => DisplayAlert("Alarm", "Alarm has been fully armed", "Ok", "Cancel"));
	}
}

