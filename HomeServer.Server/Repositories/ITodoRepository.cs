using HomeServer.Core.DTOs.Todos;

namespace HomeServer.Server.Repositories;

public interface ITodoRepository {
    Task<List<TodoCategoryReadDTO>> GetAllTodosAsync();
    Task<TodoReadDTO?> GetTodoByGuidAsync(Guid guid);

    Task CreateTodoAsync(TodoCreateDTO todoCreateDto);

    Task UpdateTodoAsync(Guid guid, TodoUpdateDTO TodoUpdateDto);
    
    /// <param name="guid">The Guid of the item to delete</param>
    /// <returns>true if the deletion was successful, false if the item was not found.</returns>
    Task<bool> DeleteTodoAsync(Guid guid);

    Task CreateTodoCategoryAsync(TodoCategoryCreateDTO todoCategoryCreateDto);

    Task UpdateTodoCategoryAsync(Guid guid, TodoCategoryUpdateDTO todoCategoryUpdateDto);

    /// <param name="guid">The Guid of the category to delete</param>
    /// <returns>true if the deletion was successful, false if the item was not found.</returns>
    Task<bool> DeleteTodoCategoryAsync(Guid guid);
}