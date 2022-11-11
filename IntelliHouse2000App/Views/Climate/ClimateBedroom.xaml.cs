using IntelliHouse2000App.Models.Climate;

namespace IntelliHouse2000App.Views;

public partial class ClimateBedroom : ContentPage
{
	Climate _climate;
	public ClimateBedroom(Climate climate)
	{
		InitializeComponent();
		this._climate = climate;
		BindingContext= _climate;
	}
}