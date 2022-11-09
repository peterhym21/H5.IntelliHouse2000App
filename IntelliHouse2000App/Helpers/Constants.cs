namespace IntelliHouse2000App;

public static class Constants
{
    private static string _baseUrl = "server.tved.it";
    private static string _schema = "http";
    private static string _apiPort = "80";
    private static string _mqttPort = "1883";

    public static string ApiBaseUrl = $"{_schema}://{_baseUrl}:{_apiPort}/";
    public static string MqttBaseUrl = $"{_baseUrl}:{_mqttPort}/";
}