using System.Net.Http.Json;
using Shared.DTOs.Todos;

namespace HomeClient.Services;

public class TodoService(HttpClient httpClient) {

    public async Task<IReadOnlyCollection<TodoCategoryReadDTO>?> GetAllTodos() {
        HttpResponseMessage response = await httpClient.GetAsync("api/todos");
        return await response.Content.ReadFromJsonAsync<IReadOnlyCollection<TodoCategoryReadDTO>?>();
    }

    public async Task UpdateTodoStatus(TodoUpdateDTO todo) {
        HttpResponseMessage response = await httpClient.GetAsync("api/todos");
    }
}