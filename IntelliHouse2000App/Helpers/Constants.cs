namespace IntelliHouse2000App;

public static class Constants
{
    private static string _baseMQTTUrl = "server.tved.it";
    private static string _baseAPIUrl = "mqtt-api.tved.it";
    private static string _schema = "http";
    private static string _apiPort = "80";
    // private static string _mqttPort = "1883";

    public static string ApiBaseUrl = $"{_schema}://{_baseAPIUrl}:{_apiPort}/";
    public static string MqttBaseUrl = $"{_baseMQTTUrl}";

    public const string mqttUser = "ardui";
    public const string mqttPass = "s1hif-xp!sT-qCuwu";
}