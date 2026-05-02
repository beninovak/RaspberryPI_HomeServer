using System;
using System.IO;
using System.Net.Http;
using System.Net.Http.Json;
using System.Text.Json;
using System.Threading.Tasks;
using ClientConstants = HomeServer.Client.Util.Constants;

namespace HomeServer.Client.Util;

public class Rufr {
    private readonly HttpClient httpClient = new() {
        BaseAddress = new Uri(ClientConstants.TEST_URL),
        Timeout = TimeSpan.FromMilliseconds(5000),
    };
    
    public Rufr() {
        Directory.CreateDirectory(ClientConstants.appStorage);
    }
    
    private async Task<bool> IveServer() {
        try {
            var response = await httpClient.GetAsync("health");
            return response.IsSuccessStatusCode;
        } catch (HttpRequestException e) {
            // TODO - Log and display error message: "Server unavailable. Try again or use local mode."?
            return false;
        }
    }

    private static bool IsPathValid(string path) {
        return path.IndexOfAny(Path.GetInvalidPathChars()) == -1;
    }

    public static string GetSaveFileNameFromModelName(string modelName) {
        return $"{modelName.Trim().ToLower()}_save.json";
    }
    
    public async Task<T?> Ruf<T>(string path, string modelName) {
        if (!await IveServer()) {
            if (!Directory.Exists(ClientConstants.localSaveFolder)) {
                return default;
            }

            string changesPath = Path.Combine(ClientConstants.localSaveFolder, GetSaveFileNameFromModelName(modelName));
            if (!IsPathValid(changesPath) || !File.Exists(changesPath)) {
                throw new Exception("Garbage file name or file doesn't exist buddy. Fix your shit!");
            }
            
            string json = await File.ReadAllTextAsync(changesPath);
            var objects = JsonSerializer.Deserialize<T>(json);
            return objects;
        }
        
        using HttpResponseMessage response = await httpClient.GetAsync(path);
        return await response.Content.ReadFromJsonAsync<T>();
    }
    
    public async Task<(bool, string)> Ruf(string path, HttpMethod method, JsonContent body, string modelName) {
        if (!await IveServer()) {
            
            Directory.CreateDirectory(ClientConstants.localChangesFolder);
            string changesPath = Path.Combine(ClientConstants.localChangesFolder, GetSaveFileNameFromModelName(modelName));

            if (!IsPathValid(changesPath)) {
                throw new Exception("Garbage file name buddy. Fix your shit!");
            }
            
            string json = JsonSerializer.Serialize(body);
            await File.AppendAllTextAsync(changesPath, json);
            return default;
        }

        HttpResponseMessage response = method switch {
            HttpMethod.Put => await httpClient.PutAsync(path, body),
            HttpMethod.Post => await httpClient.PostAsync(path, body),
            HttpMethod.Patch => await httpClient.PatchAsync(path, body),
            _ => await httpClient.PostAsync(path, body),
        };

        return (response.IsSuccessStatusCode, await response.Content.ReadAsStringAsync());
    }
}