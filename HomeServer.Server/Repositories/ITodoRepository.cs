using HomeServer.Core.Models;

namespace HomeServer.Server.Repositories;

public interface ITodoRepository {
    Task<List<TodoCategory>> GetAllTodos();
    Task<Todo?> GetTodoById(uint id);

    Task CreateTodo(Todo todo);

    Task UpdateTodo(Todo todo);

    Task CreateTodoSection(TodoCategory todoCategory);

    Task UpdateTodoSection(TodoCategory todoCategory);
}