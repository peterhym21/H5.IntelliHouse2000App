namespace IntelliHouse2000App;

public static class Constants
{
    private static string _baseUrl = "server.tved.it";
    private static string _schema = "http";
    private static string _port = "80";

    public static string ApiBaseUrl = $"{_schema}://{_baseUrl}:{_port}/";
}