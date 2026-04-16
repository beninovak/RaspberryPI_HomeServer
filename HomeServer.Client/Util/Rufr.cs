using System;
using System.Collections.ObjectModel;
using System.Net.Http;
using System.Net.Http.Json;
using System.Text.Json;
using System.Threading.Tasks;
using HomeServer.Core;

namespace HomeServer.Client.Util;

public static class Rufr {
    private static HttpClient httpClient;
    
    static Rufr() {
        httpClient = new HttpClient() {
            BaseAddress = new Uri(Constants.TEST_URL),
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

    private delegate Task<HttpResponseMessage> RufMethod(string uri, HttpContent? body);
    public static async Task<T?> Ruf<T>(string path, HTTP_METHOD method, JsonContent body) {
        RufMethod rufMethod;

        JsonContent jsonBody = JsonContent.Create(body);
        switch (method) {
            case HTTP_METHOD.POST:
                rufMethod = httpClient.PostAsync;
                break;
            
            case HTTP_METHOD.PATCH:
                rufMethod = httpClient.PatchAsync;
                break;
            
            default:
                throw new NotImplementedException("Use a supported HTTP_METHOD buddy guy...");
        }
        // TODO - poglej kva dobiš nazaj če je grosse Fehler...
        using HttpResponseMessage response = await rufMethod(path, jsonBody);
        return await response.Content.ReadFromJsonAsync<T>();
    }
}