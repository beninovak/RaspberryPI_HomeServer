using HomeServer.DTOs.Todos;

namespace HomeServer.Repositories;

public interface ITodoRepository {
    Task<List<TodoCategoryReadDTO>> GetAllTodosAsync();
    Task<TodoReadDTO?> GetTodoByGuidAsync(Guid guid);

    Task CreateTodoAsync(TodoCreateDTO todoCreateDto);

    Task<bool> UpdateTodoAsync(Guid guid, TodoUpdateDTO todoUpdateDto);
    
    /// <param name="guid">The Guid of the item to delete</param>
    /// <returns>true if the deletion was successful, false if the item was not found.</returns>
    Task<bool> DeleteTodoAsync(Guid guid);

    Task CreateTodoCategoryAsync(TodoCategoryCreateDTO todoCategoryCreateDto);

    Task<bool> UpdateTodoCategoryAsync(Guid guid, TodoCategoryUpdateDTO todoCategoryUpdateDto);

    /// <param name="guid">The Guid of the category to delete</param>
    /// <returns>true if the deletion was successful, false if the item was not found.</returns>
    Task<bool> DeleteTodoCategoryAsync(Guid guid);
}