using HomeServer.DTOs.Todos;
using HomeServer.Models.Todos;

namespace HomeServer.Repositories;

public interface ITodoRepository {
    
    Task<List<TodoCategory>> GetAllAsync();
    
    Task<Todo?> GetByGuidAsync(Guid guid);
    
    Task CreateAsync(Todo todo);

    Task UpdateAsync(Todo todo);
    
    Task DeleteAsync(Todo todo);

    Task<TodoCategory?> GetCategoryByGuidAsync(Guid guid);
    
    Task CreateCategoryAsync(TodoCategory todoCategory);

    Task UpdateCategoryAsync(TodoCategory todoCategory);
    
    Task DeleteCategoryAsync(TodoCategory todoCategory);
}