namespace IntelliHouse2000App.Repository;

public class GenericRepository : IGenericRepository
{
    private readonly HttpClient _client;
    private readonly IHttpsClientHandlerService _httpsClientHandlerService;
    
    
    public async Task<T> GetAsync<T>(Uri uri, string authToken = "")
    {
        throw new NotImplementedException();
    }

    public async Task<bool> PostAsync<T>(Uri uri, T data, string authToken = "")
    {
        throw new NotImplementedException();
    }

    public async Task<R> PostAsync<T, R>(Uri uri, T data, string authToken = "")
    {
        throw new NotImplementedException();
    }

    public async Task<bool> PutAsync<T>(Uri uri, T data, string authToken = "")
    {
        throw new NotImplementedException();
    }

    public async Task<bool> DeleteAsync(Uri uri, string authToken = "")
    {
        throw new NotImplementedException();
    }
}