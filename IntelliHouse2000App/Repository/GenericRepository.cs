using System.Diagnostics;
using System.Net;
using System.Net.Http.Headers;
using System.Text;
using System.Text.Json;
using Polly;
using Polly.Retry;

namespace IntelliHouse2000App.Repository;

public class GenericRepository : IGenericRepository
{
    private readonly HttpClient _client;
    private readonly JsonSerializerOptions _serializerOptions;
    private readonly IHttpsClientHandlerService _httpsClientHandlerService;

    public GenericRepository(IHttpsClientHandlerService service)
    {
#if DEBUG
        _httpsClientHandlerService = service;
        HttpMessageHandler handler = _httpsClientHandlerService.GetPlatformMessageHandler();
        if (handler != null)
            _client = new HttpClient(handler);
        else
            _client = new HttpClient();
#else
            _client = new HttpClient();
#endif        
        _serializerOptions = new JsonSerializerOptions
        {
            PropertyNamingPolicy = JsonNamingPolicy.CamelCase,
            WriteIndented = true
        };
    }


    #region GET

    public async Task<T> GetAsync<T>(Uri uri, string authToken = "")
    {
        ConfigureHttpClient(authToken);

        T result = default;

        try
        {
            HttpResponseMessage response = await ApiRetryPolicy().ExecuteAsync(async () => await _client.GetAsync(uri));
            if (response.IsSuccessStatusCode)
            {
                string content = await response.Content.ReadAsStringAsync();
                result = JsonSerializer.Deserialize<T>(content, _serializerOptions);
                // result = JsonSerializer.Deserialize<T>(content);
                Debug.WriteLine(@"+++++ Item(s) successfully received.");
            }
        }
        catch (Exception ex)
        {
            Debug.WriteLine(@"----- ERROR {0}", ex.Message);
        }

        return result;
    }
    #endregion

    #region POST

    public async Task<bool> PostAsync<T>(Uri uri, T data, string authToken = "")
    {
        ConfigureHttpClient(authToken);

        try
        {
            string json = JsonSerializer.Serialize<T>(data, _serializerOptions);
            StringContent content = new(json, Encoding.UTF8, "application/json");

            HttpResponseMessage response = await ApiRetryPolicy().ExecuteAsync(async () => await _client.PostAsync(uri, content));
            if (response.IsSuccessStatusCode)
            {
                Debug.WriteLine(@"+++++ Item successfully created.");
                return true;
            }
        }
        catch (Exception ex)
        {
            Debug.WriteLine(@"----- ERROR {0}", ex.Message);
        }

        Debug.WriteLine(@"----- Item NOT created!");
        return false;
    }

    public async Task<R> PostAsync<T, R>(Uri uri, T data, string authToken = "")
    {
        ConfigureHttpClient(authToken);

        R result = default;

        try
        {
            string json = JsonSerializer.Serialize<T>(data, _serializerOptions);
            StringContent content = new(json, Encoding.UTF8, "application/json");

            HttpResponseMessage response = await ApiRetryPolicy().ExecuteAsync(async () => await _client.PostAsync(uri, content));
            if (response.IsSuccessStatusCode)
            {
                Debug.WriteLine(@"+++++ Item successfully created.");
                string jsonResult = await response.Content.ReadAsStringAsync();
                result = JsonSerializer.Deserialize<R>(jsonResult);
            }
        }
        catch (Exception ex)
        {
            Debug.WriteLine(@"----- ERROR {0}", ex.Message);
        }

        return result;
    }

    #endregion

    #region PUT

    public async Task<bool> PutAsync<T>(Uri uri, T data, string authToken = "")
    {
        ConfigureHttpClient(authToken);

        try
        {
            string json = JsonSerializer.Serialize<T>(data, _serializerOptions);
            StringContent content = new(json, Encoding.UTF8, "application/json");

            HttpResponseMessage response = await ApiRetryPolicy().ExecuteAsync(async () => await _client.PutAsync(uri, content));
            if (response.IsSuccessStatusCode)
            {
                Debug.WriteLine(@"+++++ Item successfully updated.");
                return true;
            }
        }
        catch (Exception ex)
        {
            Debug.WriteLine(@"----- ERROR {0}", ex.Message);
        }

        Debug.WriteLine(@"----- Item NOT updated!");
        return false;
    }

    #endregion

    #region DELETE

    public async Task<bool> DeleteAsync(Uri uri, string authToken = "")
    {
        try
        {
            HttpResponseMessage response = await ApiRetryPolicy().ExecuteAsync(async () => await _client.DeleteAsync(uri));
            if (response.IsSuccessStatusCode)
            {
                Debug.WriteLine(@"+++++ TodoItem successfully deleted.");
                return true;
            }
        }
        catch (Exception ex)
        {
            Debug.WriteLine(@"----- ERROR {0}", ex.Message);
        }

        Debug.WriteLine(@"----- Item NOT deleted!");
        return false;
    }

    #endregion

    #region HELPER

    private void ConfigureHttpClient(string authToken)
    {
        _client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

        _client.DefaultRequestHeaders.Authorization = !string.IsNullOrEmpty(authToken) ? new AuthenticationHeaderValue("Bearer", authToken) : null;
    }
    private static AsyncRetryPolicy ApiRetryPolicy()
    {
        return Policy.Handle<WebException>(ex => ex.Status == WebExceptionStatus.Timeout)
                     .WaitAndRetryAsync(retryCount: 3, 
                                        sleepDurationProvider: attempt => TimeSpan.FromSeconds(3));
    }

    #endregion
}