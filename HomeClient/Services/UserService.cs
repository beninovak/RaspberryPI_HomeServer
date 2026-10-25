using System.Net.Http.Json;
using HomeClient.ResponseDTOs;
using Microsoft.AspNetCore.Components;

namespace HomeClient.Services;

public class UserService(HttpClient httpClient, NavigationManager navigationManager) {
    public async Task<BadLoginResponse?> Login(string email, string password) {
        FormUrlEncodedContent content = new FormUrlEncodedContent(new Dictionary<string, string> {
            { "email", email },
            { "password", password },
        });
        HttpResponseMessage response = await httpClient.PostAsync("api/login", content);

        if (response.IsSuccessStatusCode) {
            navigationManager.NavigateTo("/");
        }

        var test = await response.Content.ReadAsStringAsync();
        BadLoginResponse? responseMessage = await response.Content.ReadFromJsonAsync<BadLoginResponse>();
        
        return responseMessage;
    }
}