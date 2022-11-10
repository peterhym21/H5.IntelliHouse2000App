using System.Text;
using IntelliHouse2000App.Helpers;
using IntelliHouse2000App.Services;
using MQTTnet;
using MQTTnet.Client.Connecting;
using MQTTnet.Client.Disconnecting;
using MQTTnet.Client.Options;

namespace IntelliHouse2000App.Views;

[LifeTime(ServiceLifetime.Singleton)]
public partial class MainPage : ContentPage
{
	int count = 0;
	private IMQTTService _service;

	public MainPage(IMQTTService service)
	{
		_service = service;
		InitializeComponent();
	}

	private void MqttClient_MessageReceived(object sender, MqttApplicationMessageReceivedEventArgs e)
	{
		var topic = e.ApplicationMessage.Topic;
		var payload = e.ApplicationMessage.ConvertPayloadToString();
		DisplayAlert(topic, payload, "Ok");
	}

	private async void Publish(object sender, EventArgs e)
	{
		var message = await DisplayPromptAsync("Publish Message", "0 to disarm, 1 to partial and 2 to full arm");
		if (!string.IsNullOrWhiteSpace(message))
			await _service.Publish(new MqttApplicationMessage
			{
				Topic = "home/alarm/arm",
				Payload = Encoding.UTF8.GetBytes(message)
			});
	}
}

