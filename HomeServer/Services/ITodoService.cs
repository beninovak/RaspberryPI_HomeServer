using HomeServer.DTOs.Todos;
using HomeServer.Models.Todos;

namespace HomeServer.Services;

public interface ITodoService {
    
    public Task<IReadOnlyCollection<TodoCategoryReadDTO>> GetAllAsync();

    public Task<TodoReadDTO> GetByGuidAsync(Guid guid);

    public Task<Guid> CreateAsync(TodoCreateDTO createDto);

    public Task UpdateAsync(Guid guid, TodoUpdateDTO updateDto);

    public Task DeleteAsync(Guid guid);
    
    public Task<TodoCategoryReadDTO> GetCategoryByGuidAsync(Guid guid);

    public Task<Guid> CreateCategoryAsync(TodoCategoryCreateDTO todoCategoryCreateDto);

    public Task UpdateCategoryAsync(Guid guid, TodoCategoryUpdateDTO todoCategoryUpdateDto);

    public Task DeleteCategoryAsync(Guid guid);
}