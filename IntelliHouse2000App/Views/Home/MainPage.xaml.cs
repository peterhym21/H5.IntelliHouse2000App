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

	public MainPage()
	{
		InitializeComponent();
		Connect();
	}
	
	private async void Connect()
	{
		MqttService.MqttClient.Initialize(new MqttClientOptionsBuilder().WithClientId("MQTT_APP")
			.WithCleanSession(true)
			.WithTcpServer(Constants.MqttBaseUrl)
			.Build());
		MqttService.MqttClient.Connected += MqttClient_Connected;
		MqttService.MqttClient.MessageReceived += MqttClient_MessageReceived;
		MqttService.MqttClient.Disconnected += MqttClient_Disconnected;
		await MqttService.MqttClient.Subscribe($"{Constants.MqttBaseUrl}home/system/log");
		await MqttService.MqttClient.Connect();
	}
	
	private void MqttClient_MessageReceived(object sender, MqttApplicationMessageReceivedEventArgs e)
	{
		var topic = e.ApplicationMessage.Topic;
		var payload = e.ApplicationMessage.ConvertPayloadToString();
		DisplayAlert(topic, payload, "Ok");
	}
	
	private void MqttClient_Connected(object sender, MqttClientConnectedEventArgs e)
	{
		int a;
		if (e.ConnectResult.ResultCode == MqttClientConnectResultCode.Success)
			// yay!
			a = 1;
		else
			// bummer ...
			a = 0;
	}
	
	private async void MqttClient_Disconnected(object sender, MqttClientDisconnectedEventArgs e)
	{
			await MqttService.MqttClient.Reconnect();
	}

	private async void Publish(object sender, EventArgs e)
	{
		var message = await DisplayPromptAsync("Publish Message", "0 to disarm, 1 to partial and 2 to full arm");
		if (!string.IsNullOrWhiteSpace(message))
			await MqttService.MqttClient.Publish(new MqttApplicationMessage
			{
				Topic = Constants.MqttBaseUrl + "alarm/arm",
				Payload = Encoding.UTF8.GetBytes(message)
			});
	}
}

