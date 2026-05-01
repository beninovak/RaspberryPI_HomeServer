using System;
using System.IO;
using System.Net.Http;
using System.Net.Http.Json;
using System.Text.Json;
using System.Threading.Tasks;
using HomeServer.Client.Interfaces;
using HomeServer.Client.ViewModels;
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
    
    private async Task<bool> iveServer() {
        var response = await httpClient.GetAsync("health");
        return response.IsSuccessStatusCode;
    }

    public async Task<T?> Ruf<T>(string path, ViewModelBase vm) {
        if (!await iveServer()) {
            if (!Directory.Exists(ClientConstants.localSaveFolder) || vm is not IDataViewModel viewModel) {
                return default;
            }

            string changesPath = Path.Combine(ClientConstants.localSaveFolder, viewModel.SaveFileName);
            
            string json = await File.ReadAllTextAsync(changesPath);
            var objects = JsonSerializer.Deserialize<T>(json);
            return objects;
        }
        
        using HttpResponseMessage response = await httpClient.GetAsync(path);
        return await response.Content.ReadFromJsonAsync<T>();
    }
    
    public async Task<(bool, string)> Ruf(string path, HTTP_METHOD method, JsonContent body, ViewModelBase vm) {
        if (!await iveServer()) {
            if (vm is not IDataViewModel viewModel) {
                return default; // Gugugaga...to bi pomenil da sm Rufou iz nek kjer ne rabm sploh nč dobit od serverja?
                // Maybe bi rabu loggat te grosse pizdarije...
            }

            Directory.CreateDirectory(ClientConstants.localChangesFolder);
            string changesPath = Path.Combine(ClientConstants.localChangesFolder, viewModel.SaveFileName);
            
            string json = JsonSerializer.Serialize(body);
            await File.AppendAllTextAsync(changesPath, json);
            return default;
        }

        HttpResponseMessage response;
        switch (method) {
            case HTTP_METHOD.PUT:
                response = await httpClient.PutAsync(path, body);
                break;
            case HTTP_METHOD.POST:
                response = await httpClient.PostAsync(path, body);
                break;
            case HTTP_METHOD.PATCH:
                response = await httpClient.PatchAsync(path, body);
                break;
            default:
                throw new NotImplementedException("Absolutne grosse pizdarije...se neb sploh smel zgodit!");
        }

        return (response.IsSuccessStatusCode, await response.Content.ReadAsStringAsync());
    }
}