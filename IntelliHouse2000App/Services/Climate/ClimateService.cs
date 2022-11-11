using MQTTnet.Protocol;
using MQTTnet;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using IntelliHouse2000App.Models;

namespace IntelliHouse2000App.Services
{
    public partial class ClimateService
    {
        private readonly IMQTTService _mqttService;
        public ClimateService(IMQTTService mqttService)
        {
            _mqttService = mqttService;
        }

        public Climate GetClimate(string room)
        {
            Climate climate = new();
            switch (room)
            {
                case "bedroom":
                    // call api
                    break;
                case "livingroom":
                    // call api
                    break;
                case "kitchen":
                    // call api
                    break;
                default:
                    break;
            }

            return climate;

        }

        public void SetHumid(Climate climate)
        {
            switch (climate.Room)
            {
                case "bedroom":
                    _mqttService.Publish(new MqttApplicationMessage()
                    {
                        Topic = "home/climate/bedroom/sethumid",
                        Payload = Encoding.UTF8.GetBytes(climate.SetHumid.ToString()),
                        Retain = true,
                        QualityOfServiceLevel = MqttQualityOfServiceLevel.AtLeastOnce
                    });
                    break;
                case "livingroom":
                    _mqttService.Publish(new MqttApplicationMessage()
                    {
                        Topic = "home/climate/bedroom/sethumid",
                        Payload = Encoding.UTF8.GetBytes(climate.SetHumid.ToString()),
                        Retain = true,
                        QualityOfServiceLevel = MqttQualityOfServiceLevel.AtLeastOnce
                    });
                    break;
                case "kitchen":
                    _mqttService.Publish(new MqttApplicationMessage()
                    {
                        Topic = "home/climate/bedroom/sethumid",
                        Payload = Encoding.UTF8.GetBytes(climate.SetHumid.ToString()),
                        Retain = true,
                        QualityOfServiceLevel = MqttQualityOfServiceLevel.AtLeastOnce
                    });
                    break;
                default:
                    MessagingCenter.Send(this, "Set-Humid", "Humidity has been NOT set");
                    break;
            }
            MessagingCenter.Send(this, "Set-Humid", "Humidity has been set");
        }


        public void SetTemp(Climate climate)
        {
            switch (climate.Room)
            {
                case "bedroom":

                    _mqttService.Publish(new MqttApplicationMessage()
                    {
                        Topic = "home/climate/bedroom/settemp",
                        Payload = Encoding.UTF8.GetBytes(climate.SetTemp.ToString()),
                        Retain = true,
                        QualityOfServiceLevel = MqttQualityOfServiceLevel.AtLeastOnce
                    });
                    break;
                case "livingroom":
                    _mqttService.Publish(new MqttApplicationMessage()
                    {
                        Topic = "home/climate/bedroom/settemp",
                        Payload = Encoding.UTF8.GetBytes(climate.SetTemp.ToString()),
                        Retain = true,
                        QualityOfServiceLevel = MqttQualityOfServiceLevel.AtLeastOnce
                    });

                    break;
                case "kitchen":
                    _mqttService.Publish(new MqttApplicationMessage()
                    {
                        Topic = "home/climate/bedroom/settemp",
                        Payload = Encoding.UTF8.GetBytes(climate.SetTemp.ToString()),
                        Retain = true,
                        QualityOfServiceLevel = MqttQualityOfServiceLevel.AtLeastOnce
                    });
                    break;
                default:
                    MessagingCenter.Send(this, "Set-Temp", "Tempreture has been NOT set");
                    break;
            }

            MessagingCenter.Send(this, "Set-Temp", "Tempreture has been set");
        }

    }
}

