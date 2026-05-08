using HomeServer.Core.DTOs;
using HomeServer.Core.Models;

namespace HomeServer.Server.Repositories;

public interface ITodoRepository {
    Task<List<TodoByCategoryDTO>> GetAllTodos();
    Task<TodoDTO?> GetTodoById(uint id);

    Task CreateTodo(TodoDTO todo);

    Task UpdateTodo(TodoDTO todo);

    Task CreateTodoCategory(TodoCategoryDTO todoCategory);

    Task UpdateTodoCategory(TodoCategoryDTO todoCategory);
}