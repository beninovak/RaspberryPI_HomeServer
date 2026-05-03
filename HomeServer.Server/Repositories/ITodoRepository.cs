using HomeServer.Core.Models;

namespace HomeServer.Server.Repositories;

public interface ITodoRepository {
    Task<List<TodoSection>> GetAllTodos();
    Task<Todo?> GetTodoById(uint id);

    Task CreateTodo(Todo todo);

    Task UpdateTodo(Todo todo);

    Task CreateTodoSection(TodoSection todoSection);

    Task UpdateTodoSection(TodoSection todoSection);
}