using Shared.DTOs.Todos;
using HomeServer.Models.Todos;

namespace HomeServer.Repositories;

public interface ITodoRepository {
    
    Task<IReadOnlyCollection<TodoCategory>> GetAllAsync(Guid userGuid);
    
    Task<Todo?> GetByGuidAsync(Guid guid, Guid userGuid);
    
    Task CreateAsync(Todo todo);

    Task UpdateAsync(Todo todo);
    
    Task DeleteAsync(Todo todo);

    Task<TodoCategory?> GetCategoryByGuidAsync(Guid guid, Guid userGuid);
    
    Task CreateCategoryAsync(TodoCategory todoCategory);

    Task UpdateCategoryAsync(TodoCategory todoCategory);
    
    Task DeleteCategoryAsync(TodoCategory todoCategory);
}