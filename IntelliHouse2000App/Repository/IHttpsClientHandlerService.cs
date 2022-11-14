namespace IntelliHouse2000App.Repository;

public interface IHttpsClientHandlerService
{
    HttpMessageHandler GetPlatformMessageHandler();
}