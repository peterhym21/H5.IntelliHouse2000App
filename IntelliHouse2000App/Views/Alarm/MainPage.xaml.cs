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
	}

}

