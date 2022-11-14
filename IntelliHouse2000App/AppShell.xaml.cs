using IntelliHouse2000App.ViewModels;
using IntelliHouse2000App.Views;

namespace IntelliHouse2000App;

public partial class AppShell : Shell
{
	public AppShell()
	{
		InitializeComponent();
        BindingContext = this;
        Routing.RegisterRoute(nameof(LogMainPage), typeof(LogMainPage));
        Routing.RegisterRoute(nameof(ClimateGraphsPage), typeof(ClimateGraphsPage));
        
    }
}
