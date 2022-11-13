using SQLite;

namespace IntelliHouse2000App;

public static class Constants
{
    private const string BaseMqttUrl = "server.tved.it";
    private const string BaseApiUrl = "mqtt-api.tved.it";
    private const string Schema = "https";
    private const string ApiPort = "443";

    public const string ApiBaseUrl = $"{Schema}://{BaseApiUrl}:{ApiPort}/";
    public const string MqttBaseUrl = $"{BaseMqttUrl}";

    public const string mqttUser = "ardui";
    public const string mqttPass = "s1hif-xp!sT-qCuwu";

    public const string AlarmArmedSubject = "AlarmArmed";
    public const string AlarmPartiallyArmedSubject = "AlarmPartiallyArmed";
    public const string AlarmFullyArmedSubject = "AlarmFullyArmed";

    public const string MqttConnectedSubject = "MqttConnected";
    public const string MqttDisconnectedSubject = "MqttDisconnected";
    public const string MqttMessageReceivedSubject = "MqttMessageReceived";
    
    private const string DatabaseFilename = "Intelli2k.db3";
    public const SQLiteOpenFlags Flags =
        SQLiteOpenFlags.ReadWrite |
        SQLiteOpenFlags.Create |
        SQLiteOpenFlags.SharedCache;

    public static string DatabasePath => Path.Combine(FileSystem.AppDataDirectory, DatabaseFilename);
}