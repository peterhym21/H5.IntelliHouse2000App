using IntelliHouse2000App.Views;

namespace IntelliHouse2000App;

public partial class AppShell : Shell
{
	public AppShell()
	{
		InitializeComponent();
        BindingContext = this;
        Routing.RegisterRoute(nameof(LogPage), typeof(LogPage));
        Routing.RegisterRoute(nameof(ClimateKitchen), typeof(ClimateKitchen));
        Routing.RegisterRoute(nameof(ClimateLivingroom), typeof(ClimateLivingroom));
        Routing.RegisterRoute(nameof(ClimateBedroom), typeof(ClimateBedroom));
        
    }
}
