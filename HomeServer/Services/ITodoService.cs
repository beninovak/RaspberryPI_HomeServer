using HomeServer.DTOs.Todos;
using HomeServer.Models.Todos;

namespace HomeServer.Services;

public interface ITodoService {
    
    public Task<List<TodoCategoryReadDTO>> GetAllAsync();

    public Task<TodoReadDTO> GetByGuidAsync(Guid guid);

    public Task<Guid> CreateAsync(TodoCreateDTO todoCreateDto);

    public Task UpdateAsync(Guid guid, TodoUpdateDTO todoUpdateDto);

    public Task DeleteAsync(Guid guid);
    
    public Task<TodoCategoryReadDTO> GetCategoryByGuidAsync(Guid guid);

    public Task<Guid> CreateCategoryAsync(TodoCategoryCreateDTO todoCategoryCreateDto);

    public Task UpdateCategoryAsync(Guid guid, TodoCategoryUpdateDTO todoCategoryUpdateDto);

    public Task DeleteCategoryAsync(Guid guid);
}