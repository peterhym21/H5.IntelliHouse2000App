using IntelliHouse2000App.Helpers;

namespace IntelliHouse2000App.Views;

[LifeTime(ServiceLifetime.Singleton)]
public partial class MainPage : ContentPage
{
	int count = 0;

	public MainPage()
	{
		InitializeComponent();
	}
}

