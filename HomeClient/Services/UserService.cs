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
            navigationManager.NavigateTo("/todos");
        }

        BadLoginResponse? responseMessage = await response.Content.ReadFromJsonAsync<BadLoginResponse>();
        
        return responseMessage;
    }
    
    public async Task<BadRegisterResponse?> Register(string firstName, string lastName, string email, string password, string passwordConfirm) {
        FormUrlEncodedContent content = new FormUrlEncodedContent(new Dictionary<string, string> {
            { "firstName", firstName },
            { "lastName", lastName },
            { "email", email },
            { "password", password },
            { "passwordConfirm", passwordConfirm },
        });
        HttpResponseMessage response = await httpClient.PostAsync("api/register", content);

        if (response.IsSuccessStatusCode) {
            navigationManager.NavigateTo("/login");
        }

        BadRegisterResponse? responseMessage = await response.Content.ReadFromJsonAsync<BadRegisterResponse>();
        
        return responseMessage;
    }
}