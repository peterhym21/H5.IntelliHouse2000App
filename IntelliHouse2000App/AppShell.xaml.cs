using IntelliHouse2000App.Views;

namespace IntelliHouse2000App;

public partial class AppShell : Shell
{
	public AppShell()
	{
		InitializeComponent();
        BindingContext = this;
        Routing.RegisterRoute(nameof(Alarm), typeof(Alarm));
        Routing.RegisterRoute(nameof(Climate), typeof(Climate));
    }
}
