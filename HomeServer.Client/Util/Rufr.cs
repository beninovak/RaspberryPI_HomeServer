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
        httpClient = new HttpClient {
            BaseAddress = new Uri(Constants.TEST_URL),
        };
    }
    
    public static async Task<T?> Ruf<T>(string path, HTTP_METHOD method) {
        switch (method) {
            case HTTP_METHOD.GET:
                return await RufGet<T?>(path);
            
            case HTTP_METHOD.PATCH:
                return await RufPatch<T?>(path);
            
            default:
                throw new NotImplementedException("Use a supported HTTP_METHOD buddy guy...");
        }
    }
    
    public static async Task<T?> Ruf<T>(string path, HTTP_METHOD method, JsonContent body) {
        JsonContent jsonBody = JsonContent.Create(body);
        switch (method) {
            case HTTP_METHOD.POST:
                return await RufPost<T?>(path, jsonBody);
            
            case HTTP_METHOD.PATCH:
                return await RufPatch<T?>(path, jsonBody);
            
            default:
                throw new NotImplementedException("Use a supported HTTP_METHOD buddy guy...");
        }
    }
    
    private static async Task<T?> RufGet<T>(string path) {
        try {
            using HttpResponseMessage response = await httpClient.GetAsync(path);
            string json = await response.Content.ReadAsStringAsync();
            return JsonSerializer.Deserialize<T>(json);
        } catch (Exception e) {
            // TODO - Logger
            throw new Exception(e.Message);
        }
    }
    
    private static async Task<T?> RufPost<T>(string path, JsonContent body) {
        JsonContent jsonBody = JsonContent.Create(body);
        // TODO - poglej kva dobiš nazaj če je grosse Fehler...
        using HttpResponseMessage response = await httpClient.PostAsync(path, jsonBody);
        return await response.Content.ReadFromJsonAsync<T>();
    }
    
    private static async Task<T?> RufPatch<T>(string path) {
        // TODO - poglej kva dobiš nazaj če je grosse Fehler...
        using HttpResponseMessage response = await httpClient.PatchAsync(path, null);
        return await response.Content.ReadFromJsonAsync<T>();
    }
    
    private static async Task<T?> RufPatch<T>(string path, JsonContent body) {
        JsonContent jsonBody = JsonContent.Create(body);
        // TODO - poglej kva dobiš nazaj če je grosse Fehler...
        using HttpResponseMessage response = await httpClient.PatchAsync(path, jsonBody);
        return await response.Content.ReadFromJsonAsync<T>();
    }
}