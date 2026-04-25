using System;
using System.Net.Http;
using System.Net.Http.Json;
using System.Text.Json;
using System.Threading.Tasks;
using HomeServer.Core;

namespace HomeServer.Client.Util;

public static class Rufr {
    private static HttpClient httpClient;
    
    static Rufr() {
        httpClient = new HttpClient {
            BaseAddress = new Uri(Constants.TEST_URL),
            Timeout = TimeSpan.FromMilliseconds(500000), // TODO - replace with 3000/5000??
        };
    }
    
    public static async Task<T?> Ruf<T>(string path) {
        try {
            using HttpResponseMessage response = await httpClient.GetAsync(path);
            string json = await response.Content.ReadAsStringAsync();
            return JsonSerializer.Deserialize<T>(json);
        } catch (Exception e) {
            // TODO - Logger
            throw new Exception(e.Message);
        }
    }
    
    private delegate Task<HttpResponseMessage> SupportedHttpMethod(string? url, HttpContent? content);
    private static SupportedHttpMethod? HttpMethodToUse;
    public static async Task<T?> Ruf<T>(string path, HTTP_METHOD method, JsonContent body) {
        switch (method) {
            case HTTP_METHOD.PUT:
                HttpMethodToUse = httpClient.PutAsync;
                break;
            case HTTP_METHOD.POST:
                HttpMethodToUse = httpClient.PostAsync;
                break;
            case HTTP_METHOD.PATCH:
                HttpMethodToUse = httpClient.PatchAsync;
                break;
            default:
                throw new NotImplementedException("Use a supported HTTP_METHOD buddy guy...");
        }
        
        using HttpResponseMessage response = await HttpMethodToUse(path, body);
        // TODO - poglej kva dobiš nazaj če je grosse Fehler...
        return await response.Content.ReadFromJsonAsync<T>();
    }
}