namespace IntelliHouse2000App.Services;

public interface IAlarmService
{
    Task<bool> SetArmedAsync(ArmedState state);
}