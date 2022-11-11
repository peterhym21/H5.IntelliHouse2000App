using MQTTnet.Protocol;
using MQTTnet;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using IntelliHouse2000App.Models;
using IntelliHouse2000App.Repository;

namespace IntelliHouse2000App.Services
{
    public partial class ClimateService
    {
        private readonly IMQTTService _mqttService;
        private readonly IGenericRepository _repository;
        public ClimateService(IMQTTService mqttService, IGenericRepository repository)
        {
            _mqttService = mqttService;
            _repository = repository;
        }

        public async Task<Climate> GetClimateService(Climate climate)
        {
            DateTime timeStamp =  DateTime.Now;
            List<Climate> climates = await _repository.GetAsync<List<Climate>>(new Uri(Constants.ApiBaseUrl + climate.Room + $"?ts={timeStamp.ToString("yyyy-MM-dd")}"));
            climate = climates.FirstOrDefault();
            return climate;
        }

        public void SetHumidService(Climate climate)
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
                        Topic = "home/climate/livingroom/sethumid",
                        Payload = Encoding.UTF8.GetBytes(climate.SetHumid.ToString()),
                        Retain = true,
                        QualityOfServiceLevel = MqttQualityOfServiceLevel.AtLeastOnce
                    });
                    break;
                case "kitchen":
                    _mqttService.Publish(new MqttApplicationMessage()
                    {
                        Topic = "home/climate/kitchen/sethumid",
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


        public void SetTempService(Climate climate)
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
                        Topic = "home/climate/livingroom/settemp",
                        Payload = Encoding.UTF8.GetBytes(climate.SetTemp.ToString()),
                        Retain = true,
                        QualityOfServiceLevel = MqttQualityOfServiceLevel.AtLeastOnce
                    });

                    break;
                case "kitchen":
                    _mqttService.Publish(new MqttApplicationMessage()
                    {
                        Topic = "home/climate/kitchen/settemp",
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

