using System;
using System.Net.Http;
using System.Net.Http.Json;
using System.Net.NetworkInformation;
using System.Threading.Tasks;
using HomeServer.Core;

namespace HomeServer.Client.Util;

public static class Rufr {
    private static HttpClient httpClient;
    private static bool useLocalMode = true;
    
    static Rufr() {
        BuildClient();
        
        if (IveServerQuestionMark()) {
            useLocalMode = false;
        }
    }

    private static bool IveServerQuestionMark() {
        try {
            return Task.Run(async () => {
                var response = await httpClient.GetAsync("api/health");
                return response.IsSuccessStatusCode;
            }).GetAwaiter().GetResult();
        } catch (HttpRequestException) {
            return false;
        }
    }

    private static void BuildClient() {
        httpClient = new HttpClient {
            BaseAddress = new Uri(Constants.TEST_URL),
            Timeout = TimeSpan.FromMilliseconds(5000), // TODO - replace with 3000/5000?? Need big value for debugging...
        };
    }
    
    public static async Task<T?> Ruf<T>(string path) {
        try {
            using HttpResponseMessage response = await httpClient.GetAsync(path);
            return await response.Content.ReadFromJsonAsync<T>();
        } catch (Exception e) {
            // TODO - Logger
            // TODO - Error message
            // TODO - Load local changes
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

        try {
            using HttpResponseMessage response = await HttpMethodToUse(path, body);
            // TODO - poglej kva dobiš nazaj če je grosse Fehler...
            return await response.Content.ReadFromJsonAsync<T>();
        } catch (Exception e) {
            // TODO - Logger
            // TODO - Error message
            // TODO - Save changes locally
            throw new Exception(e.Message);
        }
    }
}