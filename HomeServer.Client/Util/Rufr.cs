using System;
using System.IO;
using System.Net.Http;
using System.Net.Http.Json;
using System.Text.Json;
using System.Threading.Tasks;
using HomeServer.Core;

namespace HomeServer.Client.Util;

public static class Rufr {
    private static HttpClient? httpClient; // TODO - Dispose at some point? Probably job za GC.
    private static bool useLocalMode = true;
    
    private static readonly string appStorage = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData), "home_server");
    private static readonly string localSaveFile = Path.Combine(appStorage, "save.json");
    private static readonly string localChangesFile = Path.Combine(appStorage, "changes.json");
    
    static Rufr() {
        BuildClient(); // TODO - A sploh rabm to poj? Sam premakn to u CheckIfIveServer()?
        Directory.CreateDirectory(appStorage);
    }

    private static void BuildClient() {
        httpClient = new HttpClient {
            BaseAddress = new Uri(Constants.TEST_URL),
            Timeout = TimeSpan.FromMilliseconds(5000), // TODO - replace with 3000/5000?? Need big value for debugging...
        };
    }
    
    public static async Task CheckIfIveServer() {
        if (httpClient is null) {
            // TODO - Logger
            useLocalMode = true;
            return;
        }
        
        // TODO - Assert httpClient has BaseAddress
        try {
            var response = await httpClient.GetAsync("health");
            useLocalMode = !response.IsSuccessStatusCode;
        } catch (HttpRequestException e) {
            // TODO - Logger
            useLocalMode = true;
        }
    }

    private static void AssertHttpClient() {
        if (httpClient is null) {
            // TODO - Logger
            throw new Exception("httpClient object is null. This should never happen. Fix your shit buddy!");
        }

        if (httpClient.BaseAddress is null) {
            // TODO - Logger
            throw new Exception("httpClient base address is null. This should never happen. Fix your shit buddy!");
        }
    }
    
    public static async Task<T?> Ruf<T>(string path) {
        if (useLocalMode) {
            if (!File.Exists(localSaveFile)) {
                return default; // Don't think I want to throw here. Just don't load anything...
            }

            try {
                string json = await File.ReadAllTextAsync(localSaveFile);
                var objects = JsonSerializer.Deserialize<T>(json);
                return objects;
            } catch (JsonException e) {
                // Json was invalid
                // TODO - Logger
                return default;
            }
        }
        
        AssertHttpClient();
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
    // TODO - zakva ne bi sam returnou bool? Alpa (bool, string) tuple za (status, message)...
    // ^^^ Tega itak ne ponucam nikol...prever
    public static async Task<T?> Ruf<T>(string path, HTTP_METHOD method, JsonContent body) {
        if (useLocalMode) {
            if (!File.Exists(localChangesFile)) {
                File.Create(localChangesFile);
            }

            try {
                string json = JsonSerializer.Serialize(body); // TODO - prever čej to prava metoda...
                await File.WriteAllTextAsync(localChangesFile, json);
            } catch (NotSupportedException e) {
                // Can't Serialize body param
                // TODO - Logger
            } catch (OperationCanceledException e) {
                // File.WriteAllTextAsync was cancelled
                // TODO - Logger
            }
            return default;
        }
        
        AssertHttpClient();
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
        } catch (HttpRequestException e) {
            // TODO - Logger
            // TODO - Error message
            // TODO - Save changes locally
            throw new Exception(e.Message);
        }
    }
}